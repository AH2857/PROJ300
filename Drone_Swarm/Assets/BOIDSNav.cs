using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOIDSNav : MonoBehaviour
{
    Vector3 navVector = Vector3.zero;
    //public GameObject ObjTrack;



    // Functions updating navVector

    // Seperation
    //Vector3 Seperation()

    // Alignment

    // Cohesion

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        // copy tracked object data array 
        //ObjectTracker2 objTrackRef = ObjectTracking.GetComponent<ObjectTracker2>();    // Get reference to Object Tracker script

        //ObjTrack.ObjData[] Objects = ObjTrack.navObj

        GameObject objectTracking = GameObject.Find("ObjectTracking");
        ObjectTracker2 TrackerRef = objectTracking.GetComponent<ObjectTracker2>();

        bool isdatalocked = TrackerRef.navObj[1].Locked;
        

        // nav recalculation timer
    }
}
