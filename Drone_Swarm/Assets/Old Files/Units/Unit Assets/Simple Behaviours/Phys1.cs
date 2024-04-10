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


    void FireRay(float MaxDistance, int angle) // modify to take direction -45 and 45
    {
        ray = new Ray(transform.position, transform.forward); //* (45 * angle));
        Physics.Raycast(ray.origin, ray.direction, out hitData, MaxDistance);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FireRay(RayRange, 0);
        if (hitData.collider.tag == "Obstacle" || hitData.collider.tag == "Unit")
        {
            if(hitData.distance < evasionDistance)
            {
                Debug.DrawRay(ray.origin, ray.direction * hitData.distance, Color.red);
                transform.RotateAround(transform.position, transform.up, aggression * Time.deltaTime);

                // additional raycasts (randomly left or right) after original obstacle 
                // int Direction = Random.Range(-45, 45) * 2 - 1; // determine direction randomly
                // cast ray in that random direction


            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * hitData.distance, Color.white);
            }
        }   
    }
}
