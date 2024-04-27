using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOIDSNav : MonoBehaviour
{
    Vector3 headingVector = Vector3.zero;

    ObjectTracker2 TrackerRef;
    SwarmManager ControlRef;
    public string CurControl;   // Current Controller
    Rigidbody UnitRigidbody;

    int NumFuncs;
    string UnitTypeTag;         //Used as AllyTag

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
            SepVector = SepVector * SepStrength / (ObjCount * 100);                                     // scale the seperation vector (once for better performance, rather than on all individual elements
            if (ControlRef.DisplayRays) { Debug.DrawRay(transform.position, SepVector, Color.red); }    // Display Seperation vector as a ray
            NumFuncs++;
        }
        return SepVector;
    }


    // Ally Alignment ("Velocity matching")
    // attempt to match velocity (direction and speed in each axis) with other "ally" units within alignment range, ignoring obstacles or other objects
    // the resultant vector of all units within range is then scaled by alignStrength and returned

    //string AllyTag;            // Tag of type of unit to match velocity with, set to its own unit type whenever Alignment is called
    //int AlignMaxRange = 200;   // Alignment Range
    //int AlignStrength = 10;    // Alignment Strength, % out of 100

    Vector3 Alignment(string AllyTag, int AlignMaxRange, int AlignStrength)
    {
        Vector3 AlignVector = Vector3.zero;
        int AllyCount = 0;

        for(int i = 0; i < TrackerRef.navObj.Length; i++)
        {
            // if (within alignmnet range) && (object data represents an ally unit) && (array position has data from this frame)
            if ((TrackerRef.navObj[i].objDistance < AlignMaxRange) && (TrackerRef.navObj[i].objTag == AllyTag) && (!TrackerRef.navObj[i].Locked))
            {
                Vector3 AllyVelVec = TrackerRef.navObj[i].objVel;       // Get velocity vector of ally unit
                AlignVector += AllyVelVec;                              // update summed Align vector
                AllyCount++;                                            // increment ally count
            }
        }

        if (AllyCount > 0)
        {
            AlignVector = AlignVector * AlignStrength / (AllyCount * 100);                                      // Scale Alignment vector 
            if (ControlRef.DisplayRays) { Debug.DrawRay(transform.position, AlignVector, Color.blue); }         // Display Alignment vector as ray
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
            }
        }

        if (AllyCount > 0)
        {
            CohereVector = CohereVector / AllyCount;
            CohereVector = CohereVector  * CohereStrength / (AllyCount * 100);     // divide total position vector by the number of units
            if (ControlRef.DisplayRays) { Debug.DrawRay(transform.position, CohereVector, Color.green); }
            NumFuncs++;
        }
        return CohereVector;
    }

    Vector3 TargetPosition(Vector3 AttractPos, int AttractStrength)
    {   
        Vector3 AttractVector = Vector3.zero;
        AttractVector = ((AttractPos - transform.position) * AttractStrength) / 100;
        if (ControlRef.DisplayRays) { Debug.DrawRay(transform.position, (Vector3.Normalize(AttractVector) * 5), Color.yellow); }
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
        
        UnitRigidbody = GetComponentInParent<Rigidbody>();                              // get reference to unit's rigidibody that interprets physics interactions
        ControlRef = GameObject.Find(CurControl).GetComponent<SwarmManager>();          // get reference to Unit manager that allows updating of Unit's heading function parameters
        UnitTypeTag = ControlRef.ManagedUnits;                                          // Set ally tag for units to be the same as that of its controller       

        Debug.DrawRay(transform.position, transform.forward);

        // Call each boids algorithm function, which checks for specific conditions, then updates the "heading" vector accordingly  
        NumFuncs = 0;
        headingVector = Vector3.zero;
        headingVector += Seperation(ControlRef.SeperationRange, ControlRef.SeperationStr);                      // 50 75
        headingVector += Alignment(UnitTypeTag, ControlRef.AlignmentRange, ControlRef.AlignmentStr);            // 150 75
        headingVector += Cohesion(UnitTypeTag, ControlRef.CohesionRange, ControlRef.CohesionStr);               // 200 100
        headingVector += TargetPosition(ControlRef.CentrePos, ControlRef.TargetStr);                            // 150

        // divide heading vector by number of functions called to create an average 
        if (NumFuncs > 0)
        {
            headingVector = headingVector / NumFuncs;         
        }


        if (ControlRef.ContMotion) { headingVector = Vector3.Normalize(headingVector); }
        Debug.Log(headingVector);
        Debug.DrawRay(transform.position, headingVector, Color.white);
        UnitRigidbody.AddForce(headingVector * ControlRef.ForwardThrust * Time.deltaTime);
    }
}
