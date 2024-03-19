using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simple_quad : MonoBehaviour
{
    // given a target direction, the unit will execute a standard movement to 


    // quadThrust // constant thrust upwards relative to quadcopter, must be large enough to overcome gravity
    // max pitch angle // dependant on thrust
    // max roll angle  // dependant on thrust
    // 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // piloting:
        // receive vec3 target coords
        // difference between current coords and target coords in each x and z axis. 
        // gain height or lose height or stay the same
        
        // current vec3 coords
        // 


        // use trig to calculate pitch and roll angles (capped at maximums) 
        // 

        // craft:
        // 
    }
}
