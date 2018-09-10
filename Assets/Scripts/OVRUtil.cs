using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRUtil : MonoBehaviour
{
    private void Start()
    {
        OVRManager.display.displayFrequency = 72f;

        OVRManager.cpuLevel = 2;
        OVRManager.gpuLevel = 2;
    }
}