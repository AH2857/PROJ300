using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvyPieceOfWood : MonoBehaviour
{
    public int aggression;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.right, 10 * Time.deltaTime);
    }
}
