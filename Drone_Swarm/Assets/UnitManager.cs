using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameObject spawnedUnit;                  // Reference to Unit Prefab
    public GameObject Environment;                  // Reference to Simulation environment
    
    // --- Simulation Setup ---
    // No need for centre vector, as this will always be 0,0,0? 
    public Vector3 SimCentre = new Vector3(0,0,0);  // Centre of the Simulation environment
    //public Vector3 xyzMax;                          // Coordinates of the bottom corner of the simulation environment
    //public Vector3 xyzMin;                          // Coordinates of the top corner of the simulation environment

    // --- Unit Spawning ---
    // Unit formation spawned 
    public int width = 10;
    public int height = 1;
    public int depth = 5;
    public float seperation = 1;

    public Vector3 minRot;
    public Vector3 maxRot;
    Vector3 randRot;

    // Start is called before the first frame update
    void Start()
    {
        
        //Instantiate(Environment, SimCentre, Quaternion.identity); // Instantiate the Environment object
        
        // Calculate scaling in each direction based on the xyzmin and xyzmax
        // Scale to match the xyz min max coordinates 
        
        for (int y=0; y<height; ++y)
        {
            for (int x=0; x<width; ++x)
            {
                for (int z=0; z<depth; ++z)
                {
                    //Instantiate(spawnedUnit, new Vector3(x, y, z), Quaternion.identity); //Instantiate a cube of units, x by y by z units in each axis

                    randRot = new Vector3(Random.Range(minRot.x, maxRot.x), Random.Range(minRot.y, maxRot.y), Random.Range(minRot.z, maxRot.z));
                    Instantiate(spawnedUnit, new Vector3(x * seperation, y * seperation, z * seperation), Quaternion.FromToRotation(Vector3.up, randRot));
                    // Instantiate(spawnedUnit, new Vector3(x, y, z), Random.rotation); //Instantiate a cube of units, x by y by z units in each axis         
                }
            }
        }
        
        // Instantiate(spawnedUnit, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
