using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phys1 : MonoBehaviour
{
    // Script that
    // + relies on constant force component
    // + fires a ray to attempt to detect an obstacle
    // + if obstacle, turn

    RaycastHit hitData;         // Struct storing data about raycast hit
    public float RayRange;      // Max Range of ray's detection
    public int aggression;      // How aggressively the unit turns to avoid obstacles
    public int evasionDistance; // How close the detected obstacle has to be for the unit to turn
    Ray ray;                    // Struct representing the ray

    //Rigidbody rigidbody;        // rigidbody of unit


    void FireRay(float MaxDistance)
    {
        ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray.origin, ray.direction, out hitData, MaxDistance);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FireRay(RayRange);
        if (hitData.collider.tag == "Obstacle")
        {
            if(hitData.distance < evasionDistance)
            {
                Debug.DrawRay(ray.origin, ray.direction * hitData.distance, Color.red);
                transform.RotateAround(transform.position, transform.up, aggression * Time.deltaTime);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * hitData.distance, Color.white);
            }
        }   
    }
}
