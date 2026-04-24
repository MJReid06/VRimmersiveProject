using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using System.Collections;
using System.Collections.Generic;

public class HMD_Manager : MonoBehaviour
{
    [SerializeField] GameObject xrPlayer;
    [SerializeField] GameObject fpsPlayer;

    void Start()
    {
        xrPlayer.SetActive(false);
        fpsPlayer.SetActive(false);
        StartCoroutine(DetectHMD());
    }

    private IEnumerator DetectHMD()
    {
        
        yield return new WaitForSeconds(2f);

        
        var xrManager = XRGeneralSettings.Instance?.Manager;

        if (xrManager != null && xrManager.isInitializationComplete)
        {
            List<InputDevice> devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, devices);

            Debug.Log("HMD devices found: " + devices.Count);

            if (devices.Count > 0)
            {
                Debug.Log("HMD detected: " + devices[0].name);
                fpsPlayer.SetActive(false);
                xrPlayer.SetActive(true);
            }
            else
            {
                Debug.Log("No HMD detected - using FPS Player");
                xrPlayer.SetActive(false);
                fpsPlayer.SetActive(true);
            }
        }
        else
        {
            Debug.Log("XR Manager not initialised - using FPS Player");
            xrPlayer.SetActive(false);
            fpsPlayer.SetActive(true);
        }
    }
}