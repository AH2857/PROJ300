using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public float countPeriod = 5;
    float countCountdown;
    public float fps;

    // Start is called before the first frame update
    void Start()
    {
        if (countPeriod < 1) { countPeriod = 1; }
        countCountdown = countPeriod;
    }

    // Update is called once per frame
    void Update()
    {
        countCountdown -= Time.deltaTime;
        if (countCountdown <= 0)
        {
            //fps = 1 / Time.deltaTime;
            //Debug.Log(fps);
            Debug.Log(Time.deltaTime);
            countCountdown = countPeriod;
        }
    }
}
