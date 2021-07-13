using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace GW2021
{
    public class HMDInfoManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if (!XRSettings.isDeviceActive)
            {
                Debug.Log("No headset plugged in.");
            }
            else if (XRSettings.isDeviceActive && (XRSettings.loadedDeviceName == "Mock HMD"
                || XRSettings.loadedDeviceName == "MockHMDDisplay"))
            {
                Debug.Log("Using Mock HMD");
            }
            else
            {
                Debug.Log("Using headset " + XRSettings.loadedDeviceName);
            }
        }

    }
}
