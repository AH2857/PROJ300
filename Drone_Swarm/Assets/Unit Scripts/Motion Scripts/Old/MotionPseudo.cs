using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionPseudo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // --- "Plane" ---
        
        // pull UnitType
        // - this defines it motion by specifying all the limits and methods of motion for this particular unit type
        // - This includes pitch, yaw and roll speeds, and whether it can do these particular manouevres, and 
        // - ie planeMotion1
        //      (pitch A per second, yaw 0 per second, roll B per second)
        //      (pitch, yaw, roll functions)
        //      (

        // pull UnitPilot
        // - this is the part that works out the specifications of the manouvres taht the unit will take to execute the navigational instructions
        // - ie SimplePlanePilot
        //      follows roll, pitch, roll routine
        //      rolls to change heading to match 
       
    }

    // Update is called once per frame
    void Update()
    {
        // --- "Brain" ---
        // inside of UnitPilot script: (this will be elsewhere)

        // acquire:
        //  att_intended
        //  att_cur
        //  vel_intended
        //  vel_cur

        // roll, pitch, roll routine

        // A_Roll
            // calculate required A_Roll (trig, dif in x and dif in y between cur and intended att)
            // execute (to the limits of the unity type, ie maxes out at maximum roll the unit can do), updating the internal_att_cur

        // B_Pitch
            // calculate req B_Pitch 
            // execute to limits, update internal_att_cur

        // check if internal_att_cur != att_intended 
        // yes --> C_Roll
            // calculate req C_Roll
            // execute to limits
        // no 
            // nothing happens

        // update att_cur = internal_att_cur




    }
}
