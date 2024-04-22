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
    public int bankStage = 0;      // stage of the banking turn manouevre that is being executed, with with 0 being no bank being executed currently
    public Vector3 TargAng;        // Target angle in each axis

    // roll function
    // apply rotation force to unit rigidbody

    // pitch function 

    // yaw function (ignore)

    // Angle tolerance, can be this many degrees out and still be considered to be at the correct angle position
    public float zAngleTol = 1;     // z axis angle tolerance

    // Start is called before the first frame update
    void Start()
    {
        GameObject navigation = GameObject.Find("Navigation");
        NavRef = navigation.GetComponent<BOIDSNav>();
    }

    // Update is called once per frame
    void Update()
    {
        // get heading vector from nav script
        headingVector = NavRef.outputHeadingVector;

        bankStage = 1;
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
                if(TargAng.z <= transform.position.z <= )       // check if at intended z angle,
                {
                    // if true: set case = 2,
                    // if false: roll towards angle

                }
                else
                {

                }
                break;

            case 2:
                // check if at intended x angle,
                // if true: set case = 3,
                // if false: pitch up towards angle
                break;
                
            case 3:
                // check if at intended z angle(0),
                // if true: bankStage = 0
                // if false: roll towards angle(0)
                break;
        }
        
    }    
}

