using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public GameObject Wall; // Wall Object

    
    Transform wallTransform;

    // Get size of parent game object "Environment"

    void InstantiateWall(Vector3 position, Vector3 rotation)
    {
        wallTransform.position = position;
        wallTransform.rotation = Quaternion.Euler(rotation);
        Instantiate(Wall, wallTransform);
    }

    // Start is called before the first frame update
    void Start()
    {
        InstantiateWall(new Vector3(0,      0,      -50),   Vector3.forward);
        InstantiateWall(new Vector3(0,      0,      50),    -1 * Vector3.forward);
        InstantiateWall(new Vector3(0,      50,     0),     Vector3.up);
        InstantiateWall(new Vector3(0,      -50,    0),     -1 * Vector3.up);
        InstantiateWall(new Vector3(50,     0,      0),     Vector3.right);
        InstantiateWall(new Vector3(-50,    0,      0),     -1 * Vector3.right);
    }

    // Update is called once per frame
    void Update()
    {
        // set the wall transforms to equal the current size and position of the environment cube object
        
    }
}
