using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public GameObject CollisionMarker;
    Vector3 CollisionPos;
    Quaternion CollisionRot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            print(other.gameObject.transform.position);
            CollisionPos = other.gameObject.transform.position;
            CollisionRot = other.gameObject.transform.rotation;
            Instantiate(CollisionMarker, CollisionPos, CollisionRot);
            Destroy(other.gameObject);                
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
