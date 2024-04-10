using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    // if object detected, raycast towards it. if detect object, add it to array of objects

    // --- Detecting search objects ---
    public float SearchRange;
    public const int SearchObj = 10;                        // Size of buffer of objects to search for
    Vector3[] SearchBuf = new Vector3[SearchObj];           // Array of Search objects positions in global space 
    int SearchBufIndex = 0;                                 // Number of objects stored in buffer

    // --- Detected objects ---
    struct castData                                         // Struct storing data about detected objects
    {
        //float Distance;
        Vector3 objPosition;
        Vector3 objRotation;
        string objTag;
    }

    public float DetectRange;
    public const int DetectObj  = 10;                       // Number of detected objects data to store
    castData[] DetectObjects    = new castData[DetectObj];  // Array of detected objects data 
    int NoDetectObj             = 0;                        // Number of detected objects stored so far

    private void OnTriggerStay(Collider other)
    {
        if (SearchBufIndex < SearchObj)                     // if space in buffer:
        {
            SearchBuf[SearchBufIndex] = other.transform.position;   // add objects position to object array to be searched for later
            SearchBufIndex++;                                       // Increment number of seacrh objects in buffer
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SphereCollider SearchSphere = gameObject.AddComponent<SphereCollider>();    // Add sphere collider component
        SearchSphere.radius = SearchRange;                                          // Scale sphere collider to represent scan range    
    }

    // Update is called once per frame
    void Update()
    {
        // Clear Detected objects array
        NoDetectObj = 0;                                    // set number of detected objects to zero
        for (int i = 0; i < DetectObj; i++)                 // for loop running through buffer of targets
        {
            Vector3 VecTowObj = SearchBuf[i] - gameObject.transform.position;                       // vector towards object transform
            RaycastHit hit;                                                                         // raycast hit data
            if (Physics.Raycast(gameObject.transform.position, VecTowObj, out hit))                 // raycast towards the object, if hits object:
            {
                // obj Distance, not implemented because can be calculated later if needed
                DetectObjects[].objPosition = hit.point;                    // Obj Position
                DetectObjects[].objRotation = hit.transform.rotation;       // obj Rotation
                DetectObjects[].objTag      = hit.collider.tag;             // obj Tag
                // if hits ANY object, add that to output array, else, doesnt return anything
            }
        }
    }
}
