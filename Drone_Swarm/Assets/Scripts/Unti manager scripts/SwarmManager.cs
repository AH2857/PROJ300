using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmManager : MonoBehaviour
{
    public string ManagedUnits;

    public int MaxUnits = 50;    
    public int Range = 20;

    // use transform of this object as centre point by default, can edit in inspector
    public bool UpdateCentre = true;
    public bool ObjTrackRays = true;
    public bool DisplayRays = true;
    public bool ForceRay = true;

    public int SeperationRange = 10, AlignmentRange = 20, CohesionRange = 20;
    public int SeperationStr = 20, AlignmentStr = 20, CohesionStr = 10;

    public Vector3 CentrePos;
    public int TargetStr = 10;

    public int ForwardThrust = 150;
    public bool ContMotion = false; // Set whether Units of this type can stay stationary

    // Start is called before the first frame update
    void Start()
    {
        ManagedUnits = gameObject.tag; 
    }

    // Update is called once per frame
    void Update()
    {
        if (UpdateCentre)                       // if update centre set to true, 
        {
            CentrePos = transform.position;     // update centre point that all units of this swarm are attracted to
        }
    }
}
