using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public GameObject Wall; // Wall Object
    //public GameObject Environment; 
    
    // Get size of parent game object "Environment"

    void InstantiateWall(Vector3 posMod, Vector3 rotation)                  // Position modifer for each axis, and rotation for each axis
    {
        Transform wallTransform = transform;
        //wallTransform.position = transform.position + posMod;   // Set pos of new wall object to be the environment pos + the modifier 
        wallTransform.position = posMod;
        //wallTransform.Rotate(rotation, Space.Self);
        wallTransform.rotation = Quaternion.LookRotation(transform.position);
        //print(wallTransform.position);
        print(wallTransform.rotation);
        Instantiate(Wall, wallTransform);

        //Instantiate(Wall, transform.position, transform.rotation);
        


        //Quaternion Qrot = Quaternion.Euler(rotation);
        //Instantiate(Wall, position, Qrot);

    }

    // Start is called before the first frame update
    void Start()
    {
        //InstantiateWall(new Vector3(0, 0, -50), Vector3.forward);
        //InstantiateWall(new Vector3(0,      0,      50),    -1 * Vector3.forward);
        //InstantiateWall(new Vector3(0,      50,     0),     Vector3.up);
        //InstantiateWall(new Vector3(0,      -50,    0),     -1 * Vector3.up);
        //InstantiateWall(new Vector3(50,     0,      0),     Vector3.right);
        //InstantiateWall(new Vector3(-50,    0,      0),     -1 * Vector3.right);

        InstantiateWall(new Vector3(0, 0, -50), new Vector3(0, 0, 0));
        InstantiateWall(new Vector3(0, 0, 50),  new Vector3(0, 0, 180));
        InstantiateWall(new Vector3(0, 50, 0),  new Vector3(90, 0, 0));
        InstantiateWall(new Vector3(0, -50, 0), new Vector3(-90, 0, 0));
        InstantiateWall(new Vector3(50, 0, 0),  new Vector3(0, 90, 0));
        InstantiateWall(new Vector3(-50, 0, 0), new Vector3(0, -90,0));
    }

    // Update is called once per frame
    void Update()
    {
        // set the wall transforms to equal the current size and position of the environment cube object
        
    }
}
