using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Main : MonoBehaviour
{
    public GameObject ThisUnit;

    public GameObject UnitFrame;
    public Transform UnitCentre;

    // --- Sensors ---

    // Drag and drop slots in Unity Inspector for each position a sensor can be placed at
    public GameObject NoseTipSensor;            // A: Slot for Sensor right on nose of the unit
    public GameObject UnderNoseSensor;          // B: Slot for Sensor hanging under unit nose
    public GameObject LPodSensor;               // C: Slot for Left Sensor pod, under wing or on side
    public GameObject RPodSensor;               // D: Slot for Right Sensor pod, under wing or on side
    public GameObject TailSensor;               // E: Slot for Sensor on the back of the unit
    public GameObject TopSensor;                // F: Slot for Sensor on top of the unit body

    // Transforms describing the position and rotation of the Sensor slots relative to the Unit's Transform
    public Transform NoseTipSlot;               // XYZ 0        0       1.5
    public Transform UnderNoseSlot;             // XYZ 0        -0.5    1
    public Transform LPodSlot;                  // XYZ -0.5     -0.5    0
    public Transform RPodSlot;                  // XYZ 0.5      -0.5    0
    public Transform TailSlot;                  // XYZ 0        0       -1.5
    public Transform TopSlot;                   // XYZ 0        0.75    0 

    // --- Setup Scripts --- 

    // public Navigation Script;
    // public Motion Script;

    // --- sensor instantiation and setup function ---
    // etc

    void ComponentSetup(GameObject gameObject, Transform transform)
    {
        Instantiate(gameObject, ThisUnit.transform, false);
        gameObject.transform.localPosition = transform.position;
        gameObject.transform.localRotation = transform.rotation;
    }


    // Start is called before the first frame update
    void Start()
    {
        // instantiate each component object, at the right relative location
        ComponentSetup(UnitFrame, UnitCentre);
        ComponentSetup(NoseTipSensor, NoseTipSlot);
        ComponentSetup(UnderNoseSensor, UnderNoseSlot);
        ComponentSetup(LPodSensor,LPodSlot);
        ComponentSetup(RPodSensor,RPodSlot);
        ComponentSetup(TailSensor,TailSlot);
        ComponentSetup(TopSensor,TopSlot);

        // replace with a system where components are added in Unity inspector instead of being instantiated at run time. 
        // because this is making the spawner shit the bed
                               
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
