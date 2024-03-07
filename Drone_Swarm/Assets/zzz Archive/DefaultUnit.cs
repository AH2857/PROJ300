using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUnit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // int speed = 0;
        // int accel = 0;
        // approxCoords
        // coords at start (rand range in xyz inside the environment cube

    }

    // Update is called once per frame
    void Update()
    {
        // --- A) Sensor main ---
        // Inputs: Current coords, facing direction (orientation), 
        // Outputs: 
        // + GPS/Altimeter module: Drone's exact coords in the simulation
        // + Basic Ray: obstacle ahead, type, speed and orientation
        // + Minimum angle unobstructed direction
        
        // --- A1) GPS/Altimter module ---
        // approxCoords = global coordinate values from simulation
        
        // --- A2) Basic Ray ---
                        
        // --- A3) 
        // default scan, fire rays ahead

        // --- B) Navigation ---
        // Inputs: Sensor data (current coords, 
        // Outputs: TargetCoords
        /* Pseudocode:
         * if current trajectory unobstructed, target coords straight ahead
         * else random left or right but turn to avoid
         */

        // --- C) Motion ---
        // Inputs: Target Coords
        // BOIDs: 
        // 
        // Outputs: New current coords
        // 

    }
}
