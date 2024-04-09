using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RadarSensor : MonoBehaviour
{
    // if object detected inside Radar Sensor collider, update that "wedge" of RadarBinary to equal 1.
    // on update, scan a "wedge" by firing that raycast and returning null or the Out hit data

 
    public GameObject thisUnit;
    BitArray RadarActiveSegments;

    public float maxScanRange   = 50;
    public const int scanResolution   = 24;

    // Timer, between segment scans
    public float scanPeriod     = 1;        // How often a segment is scanned, in seconds
    float scanCountdown;

    int curSegment;                         // Current Segment being scanned
    int ScanBeamRadius          = 5;        

    struct castData_t
    {
        float Distance;
        Vector3 objPosition;
        Vector3 objRotation;
        string objTag;
    }

    castData_t[] ScanArray = new castData_t[scanResolution];
    //List<castData_t> ScanList = new List<castData_t>();

    private void OnTriggerStay(Collider other)
    {
        // re-init hitdata as null
        // raycast towards Other transform
        // get Quaternion facing Other object from the Unit Transform.Position
        // initialise raycast with position (.position of parent object (the Unit)) and direction (Quaternion towards the Other object's .position)
        // if hitdata out == null, then do nothing,
        // if hitdata.collider.tag == tag of Other, then update ScanArray with its data

        // find the wedge that Collider other.transform.position is in,
        // set that wedge's bit to 1

        Vector2 otherXZpos = new Vector2(other.transform.position.x, other.transform.position.z);       // Acquire other's xz coordinates
        otherXZpos = otherXZpos.normalized;
        Vector2 SensorForward = new Vector2(transform.position.x, transform.position.z);
        int SegmentToActivate = (int)(Vector2.SignedAngle(otherXZpos, SensorForward)/scanResolution);   // check which wedge it is in (divide angle by number of "segments" the array has)
        RadarActiveSegments[SegmentToActivate] = true;                                                  // Activate segment of the Radar that the Other object is in

    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < scanResolution; i++) { ScanList.Add(new castData_t()); }

        thisUnit.AddComponent(                                                                      // create sphere collider component attached to object
        sphereName.transform.localscale = new Vector3(maxScanRange, maxScanRange, maxScanRange);    // scale sphere to be equal to maxScanRange in radius

        RadarActiveSegments = new BitArray(scanResolution);                                         // Set number of radar segments equal to ScanResolution and init all to 0

        Array.Resize(ref ScanArray, scanResolution);
        curSegment = 0;                                                                             // set segment to scan to first segment, 0
        scanCountdown = scanPeriod;                                                                 // start segment scan countdown

    }

    // Update is called once per frame
    void Update()
    {
        scanCountdown -= Time.deltaTime;        // increment timer
        if (scanCountdown <= 0)                 // if timer has elapsed:
        {
            Physics.SphereCast(transform.position, )        // scan "segment to scan" by firing spherecast
            if (Raycasthit == null) 
            {

            } 
        }
        
        // return null or hitdata to array
        // set RadarActiveSegments[segment to scan] = false
        // increment "segment to scan", 
        // if segment to scan == scanResolution, reset segment to scan = 0;
    }
}
