using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreakyCircle : MonoBehaviour
{
    public Vector3 ColliderSize;
    float newX;
    float newY;
    float newZ;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            print(other.gameObject.transform.position);
            //newX = other.gameObject.transform.position.x;
            //newY = other.gameObject.transform.position.y; 
            //newZ = other.gameObject.transform.position.z;

            if (other.gameObject.transform.position.x >= ColliderSize.x)
            {
                newX = -1 *  ColliderSize.x;
            }

            if (other.gameObject.transform.position.x <= (-1 * ColliderSize.x))
            {
                newX = ColliderSize.x;
            }

            if (other.gameObject.transform.position.y >= ColliderSize.y)
            {
                newY = -1 * ColliderSize.y;
            }

            if (other.gameObject.transform.position.y <= (-1 * ColliderSize.y))
            {
                newY = ColliderSize.y;
            }

            if (other.gameObject.transform.position.z >= ColliderSize.z)
            {
                newZ = -1 * ColliderSize.z;
            }

            if (other.gameObject.transform.position.z <= (-1 * ColliderSize.z))
            {
                newZ = ColliderSize.z;
            }

            other.gameObject.transform.position = new Vector3(newX, newY, newZ);          
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {    
             
    }
}
