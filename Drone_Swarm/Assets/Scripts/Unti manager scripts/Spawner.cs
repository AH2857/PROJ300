using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnUnit;            // Unit being spawned
    SwarmManager ControlRef;

    public bool EnableSpawn = true;         // Allow Unit to be spawned by default every frame
    int unitCount = 0;

    public Vector3 minRot;
    public Vector3 maxRot;
    Vector3 randRot;

    // Timer, between spawns
    public float spawnPeriod;
    float spawnCountdown;
    bool SpawnComplete = false;

    bool SingleSpawn(bool spawnEnabled)
    {
        if (spawnEnabled)                          
        {
            randRot = new Vector3(Random.Range(minRot.x, maxRot.x), Random.Range(minRot.y, maxRot.y), Random.Range(minRot.z, maxRot.z));
            Instantiate(spawnUnit, transform.position, Quaternion.FromToRotation(Vector3.up, randRot));   // spawn unit with random roataion within limits
            return true;                                // check succeeded, return true;            
        }
        else
        {
            return false;                               // spawn disabled, return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(spawnPeriod < 0.5) { spawnPeriod = 0.5f; }
        spawnCountdown = spawnPeriod;
    }

    // Update is called once per frame
    void Update()
    {
        ControlRef = GetComponent<SwarmManager>();

        // create timer that spawns units only after time elapsed, increases time each frame by time.deltatime, then checks if this is over the wait time
        
        spawnCountdown -= Time.deltaTime;
        if ((spawnCountdown <= 0) && (unitCount < ControlRef.MaxUnits))
        {
            SingleSpawn(EnableSpawn);
            spawnCountdown = spawnPeriod;
            unitCount++;
            Debug.Log(unitCount);
        }
        else if((spawnCountdown <= 0)&&(!SpawnComplete))
        {
            Debug.Log("Spawning Complete");
            SpawnComplete = true;
        } 
    }
}
