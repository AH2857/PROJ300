using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySensor : MonoBehaviour
{
    RaycastHit hitData;
    public float RayRange;      // Max range of ray's detection
    public int aggression;      // how aggressively the unit's turns to avoid obstacles
    public int avoidDist;       // how close the detected obstacle has to be for the unit to turn
    Ray ray;

    void FireRay(float MaxDistance)
    {
        ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray.origin, ray.direction, out hitData, MaxDistance);
        //Debug.DrawRay(ray.origin, ray.direction * MaxDistance);
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
            if (hitData.distance < avoidDist)
            {
                Debug.DrawRay(ray.origin, ray.direction * hitData.distance, Color.red);
                transform.RotateAround(transform.position, transform.up, aggression * Time.deltaTime);
                //transform.RotateAround(transform.position, transform.up, aggression * hitData.distance * Time.deltaTime);
                //transform.RotateAround(transform.position, new Vector3 (Random.Range(-90,90),Random.Range(-90,90),Random.Range(-90,90)), aggression * Time.deltaTime);
            }
            else
            {
                //Debug.DrawRay(ray.origin, ray.direction * RayRange, Color.white);
                Debug.DrawRay(ray.origin, ray.direction * hitData.distance, Color.white);
            }
        }

    }
}
