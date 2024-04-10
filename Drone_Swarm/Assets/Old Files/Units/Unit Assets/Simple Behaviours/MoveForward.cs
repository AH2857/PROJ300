using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public int Speed = -10; // Controls speed of the Unit

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime); //Move the unit forward along its x axis 1 unit/second
        //transform.Translate(Transform.forward * Speed * Time.deltaTime); //Not doing this, because each axis will be interacted with individually
        //print(gameObject.transform.position);
    }
}
