using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    public float maxScanRange   = 50;           
    public int scanArcAngle     = 45;           // Arc of the scan 
    public int scanResolution   = 24;           // Scans per 360 degrees

    int ScanArraySize;

    

    struct ScanData_t
    {
        //int Angle;
        float Distance;
        Vector3 TarVector;
        string TarType;
    }

    ScanData_t[] ScanArray;

    // Start is called before the first frame update
    void Start()
    {
        ScanArraySize = (scanArcAngle * 2 * scanResolution) / 360; // Use scanResolution (scans per 360 deg) and scanAngle to calculate number of scans
        ScanArray = new ScanData_t[ScanArraySize];
    }

    // Update is called once per frame
    void Update()
    {
        for(int scanIndex = 0; scanIndex < ScanArraySize; scanIndex++)
        {

            ScanArray[scanIndex] = // pass data of type ScanData_t to the array 
        }
        
        
    }
}
