using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker2 : MonoBehaviour
{
    public bool Terminal = false;
    public float range;
    LayerMask RaycastIgnores;

    void spheresetup()
    {
        SphereCollider SearchSphere = gameObject.AddComponent<SphereCollider>();    // Add sphere collider component
        SearchSphere.radius = range;                                                // Scale sphere collider to represent scan range    
        SearchSphere.isTrigger = true;

        // prevent other unit's raycasts from detecting this unit's sphere collider
        // this is achieved by putting it ona new layer and ignoring that layer in raycasts
        int ObjectTrackerLayer = 6;
        RaycastIgnores = ~LayerMask.GetMask("ObjectTracker");
        //SearchSphere.excludeLayers(RaycastIgnores);       // Unity version 23 feature
        Physics.IgnoreLayerCollision(ObjectTrackerLayer, ObjectTrackerLayer);

        
        
        //RaycastIgnores = (1 << ObjectTrackerLayer); // put sphere on Object Tracker layer, layer 6 
        //RaycastIgnores = ~(LayerMask.GetMask("ObjectTracker"));

    }

    // --- Detecting Objects ---
    public const int MaxDetectObj = 10;                     // Max number of detectable objects
    int NumDetected = 0;                                    // Number of objects detected and saved this frame, reset each frame
    Vector3[] DetectedObjPos = new Vector3[MaxDetectObj];   // Array storing all the detected objects this frame

    private void OnTriggerStay(Collider other)
    {
        if(NumDetected < MaxDetectObj)                               // checks if space to save new detcted object's position
        {
            //Debug.Log("Detected object");
            //Debug.Log(NumDetected);

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
        public Vector3 objDirVector;
        public string objTag;
    }

    ObjData[] savedObj = new ObjData[MaxSavedObj];          // Array for storing data of objects that have been searched for
    public ObjData[] navObj = new ObjData[MaxSavedObj];     // Output Array, updated at end of frame that Navigation scripts use

    void saveObj(RaycastHit data)
    {
        savedObj[NumSavedObj].Locked = false;    // set that part of array as unlocked so data can be accessed
        savedObj[NumSavedObj].objDistance = Vector3.Distance(data.point, transform.position);  // obj Distance
        savedObj[NumSavedObj].objPosition = data.point;                                        // Obj Position
        savedObj[NumSavedObj].objDirVector = data.transform.eulerAngles;                       // obj Rotation
        savedObj[NumSavedObj].objTag = data.collider.tag;                                      // obj Tag
        
        NumSavedObj++;  // increment number of saved objects
        //Debug.Log(NumSavedObj);
    }

    // Start is called before the first frame update
    void Start()
    {
        spheresetup();
    }

    // Update is called once per frame
    void Update()
    {
        //SearchSphere.radius = range;

        for(int j = 0; j < MaxSavedObj; j++) { savedObj[j].Locked = true; }  // Set each part of saved obj array as Locked, until it is overwritten
        NumSavedObj = 0;
        if (Terminal){ Debug.Log("1"); }

        // update with a timer later for performance, only searching for objects after a certain delay

        for(int i = 0; i < NumDetected; i++)    // starting at beginning of detected objects array, searches for all the detected objects
        {
            if (Terminal) { Debug.Log("2"); }
            Vector3 VecTowObj = DetectedObjPos[i] - transform.position;                  // vector towards object transform
            RaycastHit hit;                                                                         // raycast hit data
            
            Vector3 RaycastDir = Vector3.Normalize(VecTowObj) * range;                              // Create vector in direction of target, with length equal detection range
            Debug.DrawRay(transform.position, RaycastDir, Color.blue);
            
            if (Physics.Raycast(transform.position, VecTowObj, out hit, range, RaycastIgnores) && (NumSavedObj < MaxSavedObj))   // if detects the object, and there is room in SavedObjects array
            {
                Debug.Log("hit");
                saveObj(hit);   // save object data
                if (Terminal) { Debug.Log("3"); }
            }
        }

        //Debug.Log("NumSavedObj:");
        //Debug.Log(NumSavedObj);
        
        // Display all stored contacts
        for (int h = 0; h < NumSavedObj; h++)
        {
            Debug.Log(h);
            Debug.Log(savedObj[h].Locked);
            if (savedObj[h].Locked == false) {
                if (Terminal) { Debug.Log("4"); }

                Vector3 objraydir = savedObj[h].objPosition - transform.position;
                //Vector3 objraydir = savedObj[h].objDirVector * savedObj[h].objDistance;
                //Debug.Log(savedObj[h].objDistance);
                //Ray objray = new Ray(transform.position, objraydir);                    // create ray described by stored data
                //Debug.DrawRay(transform.position, objray.direction, Color.red);
                Debug.DrawRay(transform.position, objraydir, Color.red);
            }
            
            
        }

        navObj = savedObj; // Publish Array of data for Navigation to use





        //Debug.Log("Num Detected: ");
        //Debug.Log(NumDetected);
        NumDetected = 0;        // reset number of objects detected this frame
        
    }
}
