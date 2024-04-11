using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    // if object detected, raycast towards it. if detect object, add it to array of objects

    void spheresetup()
    {
        SphereCollider SearchSphere = gameObject.AddComponent<SphereCollider>();    // Add sphere collider component
        SearchSphere.radius = SearchRange;                                          // Scale sphere collider to represent scan range    
        SearchSphere.isTrigger = true;
    }

    // --- Detecting search objects ---
    public float SearchRange;
    public const int SearchObj = 10;                        // Size of buffer of objects to search for
    Vector3[] SearchBuf = new Vector3[SearchObj];           // Array of Search objects positions in global space 
    int SearchBufIndex = 0;                                 // Number of objects stored in buffer

    // --- Detected objects ---
    struct castData                                         // Struct storing data about detected objects
    {
        public bool StoredData;                             // Is there data stored from the current frame?
        public float objDistance;
        public Vector3 objPosition;                         // Object hit position in world space
        public Vector3 objDirVector;                        // Object direction vector
        public string objTag;                               // Objects "tag"
    }

    public float DetectRange;
    public const int DetectObj  = 10;                       // Number of detected objects data to store
    castData[] DetectObjects    = new castData[DetectObj];  // Array of detected objects data 
    int StoredDetectObj             = 0;                        // Number of detected objects stored so far

    private void castDataClear()
    {
        for (int j = 0; j < StoredDetectObj; j++) 
        {
            DetectObjects[j].StoredData = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Contact");
        if (SearchBufIndex < SearchObj)                     // if space in buffer:
        {
            SearchBuf[SearchBufIndex] = other.transform.position;   // add objects position to object array to be searched for later
            Debug.Log(other.transform.position);
            SearchBufIndex++;                                       // Increment number of seacrh objects in buffer-
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spheresetup();
    }

    // Update is called once per frame
    void Update()
    {
        






        // ----- old code

        // Clear Detected objects array
        castDataClear();
        StoredDetectObj = 0;                                    // set number of detected objects to zero

        for (int i = 0; i < SearchBufIndex; i++)                 // for loop running through buffer of targets
        {
            if (SearchBufIndex > 0)
            {
                //Debug.Log(i);
                Vector3 VecTowObj = SearchBuf[i] - gameObject.transform.position;                       // vector towards object transform
                RaycastHit hit;                                                                         // raycast hit data
                Debug.DrawRay(transform.position, VecTowObj, Color.blue);
                Vector3 RaycastDir = Vector3.Normalize(VecTowObj) * DetectRange;                // Create vector in direction of target, with length equal detection range
                if (Physics.Raycast(transform.position, RaycastDir, out hit))                 // raycast towards the object, if hits object:
                {
                    // if hits ANY object, add that to output array, else, doesnt return anything
                    DetectObjects[StoredDetectObj].objDistance = Vector3.Distance(hit.point, transform.position); // obj Distance
                    DetectObjects[StoredDetectObj].objPosition = hit.point;                    // Obj Position
                    DetectObjects[StoredDetectObj].objDirVector = hit.transform.eulerAngles;    // obj Rotation
                    DetectObjects[StoredDetectObj].objTag = hit.collider.tag;             // obj Tag
                    Debug.Log(DetectObjects[StoredDetectObj].objPosition);

                    Vector3 objraydir = DetectObjects[StoredDetectObj].objDirVector * DetectObjects[StoredDetectObj].objDistance;
                    Ray objray = new Ray(transform.position, objraydir);   // create ray described by stored data
                    Debug.DrawRay(transform.position, objray.direction, Color.red);
                    Debug.Log(objraydir);
                }

            }
        }

    }
}
