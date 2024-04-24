using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOIDSNav : MonoBehaviour
{
    Vector3 headingVector = Vector3.zero;

    ObjectTracker2 TrackerRef;

    int NumFuncs;
    string UnitTypeTag; //Used as AllyTag


    // Functions updating navVector
    // functions are called to modify the heading vector 

    // Seperation ("collision Avoidance")
    // Sum the distance vectors of all other objects within "Seperation range", relative to the Unit.
    // The seperation vector faces in the opposite direction to the sum vector, and is scaled by the SepStrength

    //int SepMaxRange = 50;   // Seperation Range
    //int SepStrength = 100;   // Seperation Strength, percentage out of 100%
    Vector3 Seperation(int SepMaxRange, int SepStrength)
    {
        Vector3 SepVector = Vector3.zero;       // Initialise Seperation vector as 0,0,0
        int ObjCount = 0;
        
        for (int i = 0; i < TrackerRef.navObj.Length; i++)
        {
            if ((TrackerRef.navObj[i].objDistance < SepMaxRange) && !(TrackerRef.navObj[i].Locked))  // if unit within Seperation Range and has data
            {
                Vector3 distVec = TrackerRef.navObj[i].objPosition - transform.position;    // get distance vector of this object using scan position vector, from the unit 
                SepVector -= distVec;                                                       // add inverse of distance vector to overrall Sep Vector
                ObjCount++;
            }
        }

        if (ObjCount > 0)
        {
            SepVector = SepVector * SepStrength / (ObjCount * 100);                  // scale the seperation vector (once for better performance, rather than on all individual elements
            Debug.DrawRay(transform.position, SepVector, Color.red);    // Display Seperation vector as a ray
            NumFuncs++;
        }
        return SepVector;
    }


    // Ally Alignment ("Velocity matching")
    // attempt to match velocity (direction and speed in each axis) with other "ally" units within alignment range, ignoring obstacles or other objects
    // the resultant vector of all units within range is then scaled by alignStrength and returned

    //string AllyTag;             // Tag of type of unit to match velocity with, set to its own unit type whenever Alignment is called
    //int AlignMaxRange = 200;     // Alignment Range
    //int AlignStrength = 10;    // Alignment Strength, % out of 100

    Vector3 Alignment(string AllyTag, int AlignMaxRange, int AlignStrength)
    {
        //AllyTag = gameObject.tag;
        //AllyTag = "RedUnit";
        //Debug.Log(AllyTag);
        Vector3 AlignVector = Vector3.zero;
        int AllyCount = 0;

        for(int i = 0; i < TrackerRef.navObj.Length; i++)
        {
            // if (within alignmnet range) && (object data represents an ally unit) && (array position has data from this frame)
            if ((TrackerRef.navObj[i].objDistance < AlignMaxRange) && (TrackerRef.navObj[i].objTag == AllyTag) && (!TrackerRef.navObj[i].Locked))
            {
                //Debug.Log("yeehaw");
                Vector3 AllyVelVec = TrackerRef.navObj[i].objVel;       // Get velocity vector of ally unit
                AlignVector += AllyVelVec;                              // update summed Align vector
                AllyCount++;                                            // increment ally count
                //Debug.Log(AllyVelVec);
            }
        }

        if (AllyCount > 0)
        {
            AlignVector = AlignVector * AlignStrength / (AllyCount * 100);      // Scale Alignment vector 
            Debug.DrawRay(transform.position, AlignVector, Color.blue);         // Display Alignment vector as ray
            //Debug.Log(AlignVector);
            NumFuncs++;
        }
        return AlignVector;
    }


    // Cohesion
    // using the sensor's position data of Ally units within a set range, calculate the centroid of all those units
    // the cohesion vector should point towards this position

    //string AllyTag;
    //int CohereMaxRange = 200;    // Max Cohesion range
    //int CohereStrength = 10;   // Cohesion strength, % out of 100

    Vector3 Cohesion(string AllyTag, int CohereMaxRange, int CohereStrength)
    {
        Vector3 CohereVector = Vector3.zero;
        int AllyCount = 0;
        
        for(int i = 0; i < TrackerRef.navObj.Length; i++)
        {
            if((TrackerRef.navObj[i].objDistance < CohereMaxRange) && (TrackerRef.navObj[i].objTag == AllyTag) && (!TrackerRef.navObj[i].Locked))
            {
                Vector3 AllyPosVec = TrackerRef.navObj[i].objPosition - transform.position;  // Get realtive position data of nearby unit
                CohereVector += AllyPosVec;                             // update summed unit position
                AllyCount++;                                            // increment ally count
                //Debug.Log(CohereVector);
            }
        }

        if (AllyCount > 0)
        {
            CohereVector = CohereVector / AllyCount;
            CohereVector = CohereVector  * CohereStrength / (AllyCount * 100);     // divide total position vector by the number of units
            Debug.DrawRay(transform.position, CohereVector, Color.green);
            NumFuncs++;
        }
        return CohereVector;
    }

    Vector3 Target = Vector3.zero; 
    Vector3 TargetPosition(Vector3 AttractPos, int AttractStrength)
    {   
        Vector3 AttractVector = Vector3.zero;
        AttractVector = ((AttractPos - transform.position) * AttractStrength) / 100;
        Debug.DrawRay(transform.position, AttractVector, Color.yellow);
        NumFuncs++;
        return AttractVector;
    }


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
        UnitTypeTag = "RedUnit";
        Debug.DrawRay(transform.position, transform.forward);
        // nav recalculation timer
        // add recalculation timer later

        // Call each boids algorithm function, which checks for specific conditions, then updates the "heading" vector accordingly  
        // headingVector += Seperation()
        NumFuncs = 0;
        headingVector = Vector3.zero;
        headingVector += Seperation(10, 90);                    // 50 75
        headingVector += Alignment(UnitTypeTag, 20, 250);        // 150 75
        headingVector += Cohesion(UnitTypeTag, 20, 100);       // 200 100
        headingVector += TargetPosition(Target, 80);            // 150
        

        // modify so each function modifies the vector passed to it instead of returning, (so if it does nothing it doesnt advocate for moving to 0,0,0????


        // divide heading vector by number of fucntions called to create an average 
        if (NumFuncs > 0)
        {
            headingVector = headingVector / NumFuncs;
            
        }
        
        
        int forceCap = 500;     // works at 100
        /*
        if (headingVector.x > forceCap) { headingVector.x = forceCap; }
        if (headingVector.y > forceCap) { headingVector.y = forceCap; }
        if (headingVector.z > forceCap) { headingVector.z = forceCap; }
        */
        //headingVector = Vector3.Normalize(headingVector * forceCap);
        headingVector = Vector3.Normalize(headingVector);
        Debug.DrawRay(transform.position, headingVector, Color.white);
        headingVector = headingVector * forceCap;

        GetComponentInParent<Rigidbody>().AddForce(headingVector * Time.deltaTime);
        //GetComponentInParent<Rigidbody>().velocity)
    }
}
