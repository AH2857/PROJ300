using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit : MonoBehaviour
{
    // public int Velocity = 0;
    // Orientation 
    // ApproximateLocation 

    public Vector3 posCur; // Position in 3d space, relative to global xyz
    public Vector3 attCur; // Orientation in 3d space, relative to the global xyz
    public Vector3 velLocal; // Velocity relative to the Unit's orientation, attCur 

    // 2: Navigation

    //Vector3 posTar = Unit.posCur; // 



    // Start is called before the first frame update
    void Start()
    {
        //

        

    }

    // Update is called once per frame
    void Update()
    {
        // -- 1: Sensors --
        // (make default vision function to be called here, firing a ray to search for a target)
        
        // -- 2: Navigation --
        // (make target location = current location, but a bit infront)


        // -- 3a: Motion, piloting --
        // (make perfect boid, based off of "forces" calculate the direction and speed the unit will execute)

        // -- 3b: Motion, movement
        // (code boid motion, it executes exactly what it is told to)

        // --- based off of the units velocity, update position ---
        // convert local velocity into global velocity, by summing elements of each to work out speed in each
        // update the unit's position based off the velocity in each axis


    }   
}
