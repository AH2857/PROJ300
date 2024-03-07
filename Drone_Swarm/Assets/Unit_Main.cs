using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Main : MonoBehaviour
{
    public GameObject ThisUnit;
    
    // --- Sensors ---

    // Drag and drop slots in Unity Inspector for each position a sensor can be placed at
    public GameObject NoseTipSensor;            // A: Slot for Sensor right on nose of the unit
    //public GameObject UnderNoseSensor;     // B: Slot for Sensor hanging under unit nose
    //public GameObject LPodSensor;               // C: Slot for Left Sensor pod, under wing or on side
    //public GameObject RPodSensor;               // D: Slot for Right Sensor pod, under wing or on side
    //public GameObject TailSensor;               // E: Slot for Sensor on the back of the unit
    //public GameObject TopSensor;                // F: Slot for Sensor on top of the unit body

    // Transforms describing the position and rotation of the Sensor slots relative to the Unit's Transform
    public Transform NoseTipSlot;        // XYZ 0 0 +1.5
    // Transform UnderNoseTransform;      // XYZ 0 -0.5 1
    // Transform LPodTransform;           // XYZ -0.5 -0.5 0
    // Transform RPodTransform;           // XYZ 0.5 -0.5 0
    // Transform TailTransform;           // XYZ 0 0 -1.5
    // Transform TopTransform;            // XYZ 0 0.75 0 

    // --- Setup Scripts --- 

    // public Navigation Script;
    // public Motion Script;

    // --- sensor instantiation and setup function ---
    // etc

    void SensorSetup(GameObject gameObject, Transform transform)
    {
        Instantiate(gameObject, ThisUnit.transform, false);
        gameObject.transform.localPosition = transform.position;
        gameObject.transform.localRotation = transform.rotation;
    }


    // Start is called before the first frame update
    void Start()
    {
        // instantiate each Sensor object, at the right relative location

        //turn this into a function
        Instantiate(NoseTipSensor, ThisUnit.transform, false);
        NoseTipSensor.transform.localPosition = NoseTipSlot.position;
        NoseTipSensor.transform.localRotation = NoseTipSlot.rotation;

        SensorSetup(NoseTipSensor, NoseTipSlot);

        // currently just instantiating at 000 of parent hmmmm
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
