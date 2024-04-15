using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOIDSNav : MonoBehaviour
{
    Vector3 headingVector = Vector3.zero;

    ObjectTracker2 TrackerRef;





    // Functions updating navVector
    // functions are called to modify the heading vector 

    // Seperation ("collision Avoidance")
    // Sum the distance vectors of all other objects within "Seperation range", relative to the Unit.
    // The seperation vector faces in the opposite direction to the sum vector, and is scaled by the SepStrength

    int SepRange    = 20;   // Seperation Range
    int SepStrength = 50;   // Seperation Strength, percentage out of 100%
    Vector3 Seperation()
    {
        Vector3 SepVector = Vector3.zero;       // Initialise Seperation vector as 0,0,0
        
        for (int i = 0; i < TrackerRef.navObj.Length; i++)
        {
            if ((TrackerRef.navObj[i].objDistance < SepRange) && !(TrackerRef.navObj[i].Locked))  // if unit within Seperation Range and has data
            {
                Vector3 distVec = TrackerRef.navObj[i].objPosition - transform.position;    // get distance vector of this object using scan position vector, from the unit 
                SepVector -= distVec;                                                       // add inverse of distance vector to overrall Sep Vector
            }
        }

        SepVector = SepVector * SepStrength / 100;                            // scale the seperation vector (once for better performance, rather than on all individual elements
        Debug.DrawRay(transform.position, SepVector, Color.red);     // Display Seperation vector as a ray
        return SepVector;
    }


    // Ally Alignment ("Velocity matching")
    // attempt to match velocity (direction and speed in each axis) with other "ally" units within alignment range, ignoring obstacles or other objects
    // the resultant vector of all units within range is then scaled by alignStrength and returned

    string AllyTag;             // Tag of type of unit to match velocity with, set to its own unit type whenever Alignment is called
    int AlignRange = 50;        // Alignment Range
    int AlignStrength = 1;      // Alignment Strength

    Vector3 Alignment()
    {
        //AllyTag = gameObject.tag;
        AllyTag = "RedUnit";
        //Debug.Log(AllyTag);
        Vector3 AlignVector = Vector3.zero;
        int AllyCount = 0;

        for(int i = 0; i < TrackerRef.navObj.Length; i++)
        {
            if ((TrackerRef.navObj[i].objDistance < AlignRange) && (TrackerRef.navObj[i].objTag == AllyTag) && (!TrackerRef.navObj[i].Locked))
            {
                //Debug.Log("yeehaw");
                Vector3 AllyVelVec = TrackerRef.navObj[i].objVel;       // Get velocity vector of ally unit
                AlignVector += AllyVelVec;                              // update summed Align vector
                AllyCount++;
                Debug.Log(AllyVelVec);
            }
        }

        if (AllyCount > 0) { AlignVector = AlignVector * AlignStrength / AllyCount; }   // Scale Alignment vector 
        Debug.DrawRay(transform.position, AlignVector, Color.blue);             // Display Alignment vector as ray
        //Debug.Log(AlignVector);
        return AlignVector;
    }


    // Cohesion





    // Start is called before the first frame update
    void Start()
    {
        // Get reference to object tracking data array
        GameObject objectTracking = GameObject.Find("ObjectTracking");
        TrackerRef = objectTracking.GetComponent<ObjectTracker2>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);
        // nav recalculation timer
        // add recalculation timer later

        // Call each boids algorithm function, which checks for specific conditions, then updates the "heading" vector accordingly  
        // headingVector += Seperation()

        headingVector = Vector3.zero;
        //headingVector += Seperation();
        headingVector += Alignment();


        Debug.DrawRay(transform.position, headingVector, Color.white);
    }
}
