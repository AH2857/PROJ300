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
    int SepStrength = 1;    // Seperation Strength
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

        SepVector = SepVector * SepStrength;            // scale the seperation vector (once for better performance, rather than on all individual elements
        Debug.DrawRay(transform.position, SepVector, Color.yellow);   // Display Seperation vector as a ray
        return SepVector;
    }

    // Ally Alignment ("Velocity matching")
    // attempt to match velocity (direction and speed in each axis) with other "ally" units within alignment range, ignoring obstacles or other objects
    // implementing a simplifed version that just matches direction
    // the resultant vector of all units within range is then scaled by alignStrength and returned

     

    string AllyTag;             // Tag of type of unit to match velocity with, set to its own unit type whenever Alignment is called
    int AlignRange      = 50;   // Alignment Range
    int AlignStrength   = 1;    // Alignment Strength

    Vector3 SimplifiedAlignment()
    {
        AllyTag = gameObject.tag;
        Vector3 AlignVector = Vector3.zero;     // Initialise Alignment vector as 0,0,0

        for (int i = 0; i < TrackerRef.navObj.Length; i++)
        {
            if ((TrackerRef.navObj[i].objDistance < AlignRange) && (TrackerRef.navObj[i].objTag == AllyTag) && (!TrackerRef.navObj[i].Locked) )
            {
                Debug.Log("yeehaw");
                // !!! Simplified version of original algorithm, doesnt have ally's speed, only direction
                Vector3 AllyDirVec = Vector3.Normalize(TrackerRef.navObj[i].objRotVector);     // get the Direction vector of the ally unit
                AlignVector += AllyDirVec;                                  // add the Direction vector to Alignment Vector
            }
        }

        AlignVector = AlignVector * AlignStrength;
        Debug.DrawRay(transform.position, AlignVector, Color.cyan);

        return AlignVector;
    }

    // Alignment Version 2:
    // Doesnt provide a force until it has calculated a non zero Align vector afte the first call of alignment.
    // it then uses the difference between the two summed vectors and the time difference to convert the two sum direction vectors into a velocity



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
        // nav recalculation timer
        // add recalculation timer later

        // Call each boids algorithm function, which checks for specific conditions, then updates the "heading" vector accordingly  
        // headingVector += Seperation()

        headingVector = Vector3.zero;
        headingVector += Seperation();
        headingVector += SimplifiedAlignment();


        Debug.DrawRay(transform.position, headingVector, Color.white);
    }
}
