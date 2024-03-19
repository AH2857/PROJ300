using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionFunctions : MonoBehaviour
{
    public Rigidbody Unit;
    
    
    
    // Rotate Function (default, modfy to make roll pitch and yaw)
    // Pass: Unit's odometry "ghost" Transform, Unit's Transform, Target Angle, Maximum turn rate in positive and negative directions
    bool TurnBasic(Transform UnitOdometry, Transform Unit, float TarAngle, float MaxPosTurn, float MaxNegTurn) 
    {
        // Check if target achieved by Calc difference between current believed angle
        // - if no output 0, and rotate as much as possible 
        // - if yes output 1

        float AngleDif = TarAngle - UnitOdometry.eulerAngles.x;         // Calc difference between current believed angle 
        if ((AngleDif < 1) && (AngleDif > -1))                          // check if outside acceptable range
        {
            return true;
        }
        else
        {
            float TurnAngle = AngleDif;                                                 // Calculate TurnAngle
            if (TurnAngle > MaxPosTurn) { TurnAngle = MaxPosTurn; }                     // Cap turn at Max positive turn
            if (TurnAngle < MaxNegTurn) { TurnAngle = MaxNegTurn; }                     // Cap turn at max negative turn
            Unit.RotateAround(Unit.position, Unit.right, TurnAngle * Time.deltaTime);   // Execute turn at value required (cap at max rates of turn)
            return false;
        }
    }


    /*
    // Change main thrust 
    // Pass: Rigidbody of Engine, Target Force, Maximum Force, Minimum Force)
    bool UpForceBasic(Rigidbody Engine, float TarForce, float MaxForce, float MinForce)
    {
        float Difference = TarForce - c
        
    }
    */

    //float MaxForce, float MinForce,

    // apply thrust
    // Pass: Rigidbody of Engine or Unit, Force to apply, direction (vector3,up / vector3.forward)
    void Force(Rigidbody Engine, float Force, Vector3 direction)
    {
        Engine.AddForce(direction * Force);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        Force(Unit, 10, Vector3.forward); // Must run this function every update
        Force(Unit, 1, Vector3.up);
    }
}
