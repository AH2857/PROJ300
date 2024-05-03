using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker2 : MonoBehaviour
{
    LayerMask RaycastIgnores;

    SphereCollider SearchSphere;
    SwarmManager ControlRef;
    public string CurrentController;

    void sphereSetup()
    {
        SearchSphere = gameObject.AddComponent<SphereCollider>();                   // Add sphere collider component
        SearchSphere.radius = ControlRef.Range;                                     // Scale sphere collider to represent scan range    
        SearchSphere.isTrigger = true;

        // prevent other unit's raycasts from detecting this unit's sphere collider
        // this is achieved by putting it on a new layer and ignoring that layer in raycasts
        int ObjectTrackerLayer = 6;
        RaycastIgnores = ~LayerMask.GetMask("ObjectTracker");
        Physics.IgnoreLayerCollision(ObjectTrackerLayer, ObjectTrackerLayer);
    }

    void sphereUpdate()
    {
        SearchSphere.radius = ControlRef.Range;
    }

    // --- Detecting Objects ---
    public const int MaxDetectObj = 20;                     // Max number of detectable objects     //10 originally
    int NumDetected = 0;                                    // Number of objects detected and saved this frame, reset each frame
    Vector3[] DetectedObjPos = new Vector3[MaxDetectObj];   // Array storing all the detected objects this frame

    private void OnTriggerStay(Collider other)
    {
        if(NumDetected < MaxDetectObj)                               // checks if space to save new detcted object's position
        {
            DetectedObjPos[NumDetected] = other.transform.position;     // Saves detected object's position to array
            NumDetected++;                                              // increment number of saved detected objects
        }
    }

    // --- searching for and saving data of objects ---

    public const int MaxSavedObj = 10;
    int NumSavedObj = 0;

    public struct ObjData                                   // storing data returned from raycast in struct
    {
        public bool Locked;
        public float objDistance;
        public Vector3 objPosition;
        public Vector3 objRotVector;
        public string objTag;
        public Vector3 objVel;
    }

    ObjData[] savedObj = new ObjData[MaxSavedObj];          // Array for storing data of objects that have been searched for
    public ObjData[] navObj = new ObjData[MaxSavedObj];     // Output Array, updated at end of frame that Navigation scripts use

    void saveObj(RaycastHit data)
    {
        savedObj[NumSavedObj].Locked = false;    // set that part of array as unlocked so data can be accessed
        savedObj[NumSavedObj].objDistance = Vector3.Distance(data.point, transform.position);  // obj Distance
        savedObj[NumSavedObj].objPosition = data.point;                                        // Obj Position
        savedObj[NumSavedObj].objRotVector = data.transform.eulerAngles;                       // obj Rotation
        savedObj[NumSavedObj].objTag = data.collider.tag;                                      // obj Tag
        savedObj[NumSavedObj].objVel = data.rigidbody.velocity;

        NumSavedObj++;  // increment number of saved objects
    }

    // Start is called before the first frame update
    void Start()
    {
        ControlRef = GameObject.Find(CurrentController).GetComponent<SwarmManager>();           // get reference to Unit manager
        sphereSetup();
    }

    // Update is called once per frame
    void Update()
    {
        ControlRef = GameObject.Find(CurrentController).GetComponent<SwarmManager>();           // get reference to Unit manager
        sphereUpdate();
        

        for (int j = 0; j < MaxSavedObj; j++) { savedObj[j].Locked = true; }            // Set each part of saved obj array as Locked, until it is overwritten
        NumSavedObj = 0;

        for(int i = 0; i < NumDetected; i++)                                            // starting at beginning of detected objects array, searches for all the detected objects
        {

            Vector3 VecTowObj = DetectedObjPos[i] - transform.position;                 // vector towards object transform
            RaycastHit hit;                                                             // raycast hit data
            
            Vector3 RaycastDir = Vector3.Normalize(VecTowObj) * ControlRef.Range;       // Create vector in direction of target, with length equal detection range
            //if (ControlRef.ObjTrackRays) { Debug.DrawRay(transform.position, RaycastDir, Color.blue); }
            
            if (Physics.Raycast(transform.position, VecTowObj, out hit, ControlRef.Range, RaycastIgnores) && (NumSavedObj < MaxSavedObj))   // if detects the object, and there is room in SavedObjects array
            {
                saveObj(hit);   // save object data
            }
        }

        // Display all stored contacts
        for (int h = 0; h < NumSavedObj; h++)
        {
            if (savedObj[h].Locked == false) {
                Vector3 objraydir = savedObj[h].objPosition - transform.position;
                if (ControlRef.ObjTrackRays){ Debug.DrawRay(transform.position, objraydir, Color.red); }                
            }           
        }

        navObj = savedObj;      // Publish Array of data for Navigation to use
        NumDetected = 0;        // reset number of objects detected this frame       
    }
}
