using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aero_Motion : MonoBehaviour
{
    // Implement turning and acceleration
    // Max speed (higher than speed at which drag equals acceleration)
    // Max Acceleration force
    // Implement Drag (this applies a force that opposes the direction of travel, proportional to the final speed after acceleration)
    // Implent Lift (this applies a lift force in the unit's local y axis proportional to the unit's speed in the it's local forward direction

    BOIDSNav NavRef;
    Vector3 headingVector;

    // default motion functions are used in the unit specific manouevre
    // the Aeroplane uses the banking turn manouevre, which consists of a sequence of roll, pitch up, roll back to level
    int bankStage = 0;               // stage of the banking turn manouevre that is being executed, with with 0 being no bank being executed currently
    public Vector3 targAng;                 // Target angle in each axis
    Vector3 pilotTorqSum = Vector3.zero;    // Stores torques to be applied at end of frame by the unit's "piloting" (rolling the unit to turn, etc)
    Vector3 physTorqSum = Vector3.zero;     // Stores torques to be applied at end of frame due to unit's physical properties (stabilisation of wings etc)


    
    float rotateTorqCalc(int locTargAng, float locCurAng, int rollStr, float AngTol) // local Target Angle, local Current Angle, local roll strength (1), Angle accuracy tolerance
    {
        int RollDir = 0;
        //if ((locCurAng - locTargAng) == 0) 
        if ((locCurAng - locTargAng) > (-AngTol) && (locCurAng - locTargAng) < (AngTol))
        { 
            RollDir = 0; 
        } 
        else 
        { 
            RollDir = (int)Mathf.Sign((locCurAng - locTargAng)); // roll direction (-1, 1)
        }       

        if(RollDir != 0)                       
        {
            return (RollDir * rollStr);     // return set torque in that direction 
        }
        else
        {
            return 0;                       // or return 0 torque if dir = 0
        }  
            
    }

    // Angle tolerance, can be this many degrees out and still be considered to be at the correct angle position
    public int zAngleTol = 1, xAngleTol = 5, yAngleTol = 5;           // axis angle tolerances
    int targAngZ, targAngX, targAngY;

    // roll function
    bool roll(int rollStr, int tAng, float cAng, float Tol)     // returns bool representing whether roll torque is required
    {
        float reqTorq;                                          // Required torque
        reqTorq = rotateTorqCalc(tAng, cAng, rollStr, Tol);     // Calculate required torque to apply to execute intended 
        if (reqTorq == 0)
        {
            // pilot torque remains unmodified
            return false;
        }
        else
        {
            pilotTorqSum.z += reqTorq;
            return true;
        }
    }

    // pitch function 
    bool pitch(int pitchStr, int tAng, float cAng, float Tol)
    {
        float reqTorq;                                          // Required torque
        reqTorq = rotateTorqCalc(tAng, cAng, pitchStr, Tol);    // Calculate required torque to apply to execute intended 
        if (reqTorq == 0)
        {
            // pilot torque remains unmodified
            return false;
        }
        else
        {
            pilotTorqSum.x += reqTorq;
            return true;
        }
    }

    // yaw function (ignore)

    void TargetAngleUpdate()
    {
        targAng = 
        targAngZ = (int)targAng.z;
        targAngX = (int)targAng.x;
        targAngY = (int)targAng.y;
    }

    bool banking()
    {
        // turn towards heading vector from current vector
        // roll
        // pitch up
        // roll back to level

        // initiate banking turn (enter switch case)
        // case 0: break, no banking turn being executed
        // case 1: check if at intended z angle, if true: set case = 2,     if false: roll towards angle
        // case 2: check if at intended x angle, if true: set case = 3,     if false: pitch up towards angle
        // case 3: check if at intended z angle (0), if true: break,        if false: roll towards angle (0)

        switch (bankStage)
        {
            case 0:
                break;

            case 1:

                if (roll(10, targAngZ, transform.rotation.z, zAngleTol))    // check if at intended z angle, Yes: move to next stage of banking turn on next update, NO: calculate required torque to apply at end of frame
                {
                    bankStage = 2;                                          // if true: set case = 2
                }
                break;

            case 2:
                if (pitch(5, targAngX, transform.rotation.x, xAngleTol))    // check if at intended x angle, Yes: move to next stage of banking turn on next update, NO: calculate required torque to apply at end of frame
                {
                    bankStage = 3;                                          // if true: set case = 3
                }
                break;

            case 3:
                if (roll(10, 0, transform.rotation.z, zAngleTol))           // check if at intended z angle(0),
                {
                    bankStage = 0;                                          // if true: bankStage = 0
                    return true;
                }
                break;
        }
        return false;
    }

    //Vector3 StartForce = Vector3.forward;   // Starting force
    public int ForThrustStr = 1;            // Forward thrust strength
    Vector3 dragForce       = Vector3.zero;
    Vector3 dragCoeffProfile;               // used in drag force calculations for each axis
    int thrustDirCoeff      = 1;            // drag coefficient in direction of main coaxial thrust
    int nonThrustDirCoeff   = 5;            // drag coefficient in other axes
    Vector3 Liftdir         = Vector3.up;   // set direction of lift force created due to forward motion in z axis
    float LiftCoeff         = 2.0f;          // Lift coefficent recreating "FL prop LiftCoeff * v^2"
    int LDRatio             = 25;           // Lift to drag ratio. lift drag = lift / LDratio
    float LiftForce         = 0;            // lift force created by "wings"
    float LiftDrag          = 0;            // drag created by wings creating lift

    Vector3 DragForceCalc()
    {
        // calculate drag in each axis, using speed in that axis and coeff of drag in that axis
        Vector3 drag = Vector3.zero;
        Vector3 worldVel = GetComponentInParent<Rigidbody>().velocity;      // calculate velocity of unit in worldspace
        Vector3 locVel = transform.InverseTransformDirection(worldVel);     // calculate velocity in local axis of unit
        // use drag equation to update that axis of drag force vector
        drag.x = dragCoeffProfile.x * Mathf.Pow(locVel.x, 2);   // Simplified version of the drag equation, that still represents "Fd prop to v^2" 
        drag.y = dragCoeffProfile.y * Mathf.Pow(locVel.y, 2);
        drag.z = dragCoeffProfile.z * Mathf.Pow(locVel.z, 2);
        return drag;
    }

    void LiftForceCalc()
    {
        // calculate Lift force and drag from lift generation
        Vector3 Lift = Vector3.zero;
        Vector3 worldVel = GetComponentInParent<Rigidbody>().velocity;      // calculate velocity of unit in worldspace
        Vector3 locVel = transform.InverseTransformDirection(worldVel);     // calculate velocity in local axis of unit
        LiftForce = LiftCoeff * (Mathf.Pow(locVel.z, 2)/2);                 // use lift equation to update lift axis force vector
        LiftDrag = LiftForce / LDRatio;                                     // use lift and LDratio to calculate lift drag
    }

    // Apply final summed torque
    // Apply final summed acceleration
    void SummedPhysics(int forwardThrust)
    {
        GetComponentInParent<Rigidbody>().AddTorque((pilotTorqSum + physTorqSum)* Time.deltaTime);                                                                                    // Apply rotation torques
        dragForce = DragForceCalc();
        LiftForceCalc();
        GetComponentInParent<Rigidbody>().AddForce(((Vector3.forward * forwardThrust) + (- dragForce) + (Liftdir * LiftForce) + (- Vector3.forward * LiftDrag)) * Time.deltaTime);     // Apply forces to unit
    }

    int navCountdown = 10;          // Countdown of 10 seconds
    float navCheckCountdown; 

    // Start is called before the first frame update
    void Start()
    {
        GameObject navigation = GameObject.Find("Navigation");
        NavRef = navigation.GetComponent<BOIDSNav>();

        // drag setup
        dragCoeffProfile = Vector3.one * nonThrustDirCoeff;     // set drag coefficient in x and y axes (and z axis)
        dragCoeffProfile.z = thrustDirCoeff;                    // lower drag coefficient in axis of thrust (z axis)
        
        // start timer
        navCheckCountdown = navCountdown;
    }

    // Update is called once per frame
    void Update()
    {
        // reset all variables that need to reset to 0

        navCheckCountdown -= Time.deltaTime;                    // decrement countdown
        if (navCheckCountdown <= 0)
        {
            // if timer runs out, get new nav data and restart banking turn switchcase
            
            headingVector = NavRef.outputHeadingVector; // get heading vector from nav script
            TargetAngleUpdate();                        // Update target angles
            bankStage = 1;                              // start banking turn
            navCheckCountdown = navCountdown;           // restart countdown
        }

        // functions that always run on update
        banking();
        SummedPhysics(ForThrustStr);        // Apply final summed torque and Apply final summed acceleration
    }    
}

