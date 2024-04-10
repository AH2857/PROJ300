using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionFunctions : MonoBehaviour
{
    public Rigidbody Unit;

    // Unit "ghost" setup
    Vector3 UnitApproxVelocity;                             // Represents the "measured" Velocity of the Unit, relative to its own axis

    // Rot setup
    Vector2 xMaxTurn        = new Vector2(45,45);           // X axis TurnRate (Max Positive, Max Negative)
    Vector2 yMaxTurn        = new Vector2(45,45);           // Y axis TurnRate (Max Positive, Max Negative)
    Vector2 zMaxTurn        = new Vector2(45,45);           // Z axis TurnRate (Max Positive, Max Negative)

    // Thrust setup
    Vector3 ThrustCurrent   = new Vector3(0,0,0);           // Thrust value in each direction (that will be applied per second using * Time.Deltatime)
    Vector3 MaxThrustChange = new Vector3(50, 50, 50);      // Max value the change of thrust will be capped at (that will be applied per second using "* Time.Deltatime")
    Vector3 MaxThrust       = new Vector3(200, 200, 200);   // Max value the thrust will be capped at (that will be applied per second using "* Time.Deltatime")

    // Rotate Function (default, modify to make roll pitch and yaw)
    // Pass: Unit's odometry "ghost" Transform or actual transform, Unit's Transform, Target Angle, Maximum turn rate in positive and negative directions
    bool Rot(Transform UnitPosition, Transform UnitTrans, float TargAngle, float MaxPosTurn, float MaxNegTurn) 
    {
        // Check if at already at target angle by calculating difference between current (approx) angle and target angle
        // - if no output 0, and rotate as much as possible*  
        // - if yes output 1

        // *Replace with more intelligent method later?

        float AngleDif = TargAngle - UnitPosition.eulerAngles.x;        // Calc difference between current and believed angle 
        if ((AngleDif < 1) && (AngleDif > -1))                          // check if outside acceptable range
        {
            return true;
        }
        else
        {
            float TurnAngle = AngleDif;                                                 // Calculate TurnAngle
            if (TurnAngle > MaxPosTurn) { TurnAngle = MaxPosTurn; }                     // Cap turn at Max positive turn
            if (TurnAngle < MaxNegTurn) { TurnAngle = MaxNegTurn; }                     // Cap turn at max negative turn
            UnitTrans.RotateAround(UnitTrans.position, UnitTrans.right, TurnAngle * Time.deltaTime);   // Execute turn at value required (cap at max rates of turn)
            return false;
        }
    }
    bool ThrustToSpeed(float TargSpeed, float ApproxVel, float MaxThrustChange, float MaxThrust, ref float ThrustCurrent)
    {
        // Check if already at target speed by calculating difference between current speed and target speed
        // - if no output 0 and increase thrust as much as possible*
        // - if yes output 1 and keep thrust the same

        float SpeedDif = TargSpeed - ApproxVel;
        if ((SpeedDif < 1) && (SpeedDif > -1))
        {
            // Dont modify the current thrust
            return true;
        }
        else
        {
            float ThrustAdjust = (SpeedDif * 10);                                       // calculate thrust adjustment, adjust the 
            if (ThrustAdjust > MaxThrustChange) { ThrustAdjust = MaxThrustChange; };    // Cap max thrust increase
            if (ThrustAdjust < MaxThrustChange) { ThrustAdjust = -MaxThrustChange; };   // Cap max thrust increase
            ThrustCurrent += (ThrustAdjust);                                            // adjust current thrust
            if (ThrustCurrent > MaxThrust) { ThrustCurrent = MaxThrust; };              // Cap max thrust
            return false;
        }
    }

    bool Roll(Transform A, Transform B, float C){
        return Rot(A, B, C, zMaxTurn.x, zMaxTurn.y);
    }

    bool Pitch(Transform A, Transform B, float C)
    {
        return Rot(A, B, C, xMaxTurn.x, xMaxTurn.y);
    }

    bool Yaw(Transform A, Transform B, float C)
    {
        return Rot(A, B, C, yMaxTurn.x, yMaxTurn.y);
    }
    
    // Adjust thrust to achieve target speed for Z axis 
    // Pass: Target Speed
    bool ZThrustToSpeed(float zSpeed)
    {
        return ThrustToSpeed(zSpeed, UnitApproxVelocity.z, MaxThrustChange.z, MaxThrust.z, ref ThrustCurrent.z);
    }

    // Adjust thrust to achieve target speed for Y axis 
    // Pass: Target Speed
    bool YThrustToSpeed(float ySpeed)
    {
        return ThrustToSpeed(ySpeed, UnitApproxVelocity.y, MaxThrustChange.y, MaxThrust.z, ref ThrustCurrent.z);
    }

    // Adjust thrust to achieve target speed for X axis 
    // Pass: Target Speed
    bool XThrustToSpeed(float xSpeed)
    {
        return ThrustToSpeed(xSpeed, UnitApproxVelocity.x, MaxThrustChange.x, MaxThrust.x, ref ThrustCurrent.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update "ghost" unit values by measureing them (position, orientation and speed)
        UnitApproxVelocity = Unit.velocity; // replace with method of working out velocity relative to own axes


        
        
        // Thrust Forces are applied
        Unit.AddRelativeForce(Vector3.forward * ThrustCurrent.z * Time.deltaTime);
    }
}
