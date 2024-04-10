using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sensors : MonoBehaviour
{
    /* spaces for references to sensor scripts, with init variables for each (on/off, direction, range etc)
       * 1
       * 2
       * 3
       * 4
       * 5
       * etc
       */
    public Sensors Sensor1;
    public Sensors Sensor2;
    public Sensors Sensor3;
    public Sensors Sensor4;
    public Sensors Sensor5;

    // Start is called before the first frame update
    void Start()
    {
      



    }   

    // Update is called once per frame
    void Update()
    {
        // run sensor update scripts

        // run sensor data compile scripts? (run these less often, use stored data from a certain amount of time)
    }
}


/*
 * Sensors occupy slots
 *  these slots are flexible (can have any sensor or sensor package in them) 
 *  and how many of these slots each unit has is established at start up
 * 
 * Establishing slots
 *  pass number of slots (the slot manager will need to know this to know how many to look for, remember to make it so that too many or too few slots wont create an error, but instead will just ignore empty slots and ignore sensors that dont have a slot)
 *  
 * What is a sensor script?
 */



