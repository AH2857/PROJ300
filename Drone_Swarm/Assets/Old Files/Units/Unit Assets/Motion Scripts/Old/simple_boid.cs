using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simple_boid : MonoBehaviour
{
    public GameObject Target;
    public float Speed = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // take target vec3
        // take transform of this unit, extract the position vec3
        Vector3 Dif = Target.transform.position - transform.position;   // use unity .lookat to find rotation to the target position
        transform.Rotate(Dif * Time.deltaTime); // rotate to the target position
        transform.Translate(Vector3.forward * Speed * Time.deltaTime); // move towards position (replace with acceleration later // accelerate towards the position (*time * deltatime)

        

    }
}
