using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnUnit;        // Unit being spawned
    public GameObject spawnPoint;       // Object thats transform describes the location units will attempt to spawn

    public bool TestSpawn = false;           // Allow Single unit to be instantaiated on startup as a test
    public int EnableSpawn = 1;         // Allow Unit to be spawned by default every frame

    public int checkRadius = 5;
    public Vector3 minRot;
    public Vector3 maxRot;
    Vector3 randRot;

    // Timer, between spawns
    public float spawnPeriod = 1;
    float spawnCountdown;

    bool SingleSpawn(int spawnEnabled)
    {
        if (spawnEnabled == 1)                          
        {
            if (Physics.CheckSphere(spawnPoint.transform.position, checkRadius)){                                     //check if Unit can spawn unobstructed
                checkRadius = (int)spawnPoint.transform.localScale.x;
                randRot = new Vector3(Random.Range(minRot.x, maxRot.x), Random.Range(minRot.y, maxRot.y), Random.Range(minRot.z, maxRot.z));
                Instantiate(spawnUnit, spawnPoint.transform.position, Quaternion.FromToRotation(Vector3.up, randRot));   // spawn unit with random roataion within limits
                //Debug.Log(randRot);                   // Error check
                return true;                            // check succeeded, return true;
            }
            else
            {
                return false;                           //  check failed, return false;
            }                 
        }
        else if (spawnEnabled == 0)
        {
            return false;                               // spawn disabled, return false;
        }
        else
        {
            Debug.LogError("Error, unknown input of spawnEnabled to func Single Spawn: ");
            Debug.Log(spawnEnabled);
            return false; // ERROR
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (TestSpawn) { SingleSpawn(EnableSpawn); }         // Test Spawn

        if(spawnPeriod < 1) { spawnPeriod = 1; }
        spawnCountdown = spawnPeriod;
    }

    // Update is called once per frame
    void Update()
    {
        // create timer that spawns units only after time elapsed, increases time each frame by time.deltatime, then checks if this is over the wait time
        spawnCountdown -= Time.deltaTime;
        if (spawnCountdown <= 0)
        {
            SingleSpawn(EnableSpawn);
            spawnCountdown = spawnPeriod;
        }

    }

    void FixedUpdate()
    {
        //SingleSpawn(EnableSpawn);
    }
}
