using System.Collections;
using UnityEngine;

public class TortureDeviceScript : MonoBehaviour
{
    [SerializeField] private GameObject[] tortureDevices;
    [SerializeField] private Transform[] snapLocations;

    private bool[] devicesInPlace;

    private void Start()
    {
        devicesInPlace = new bool[tortureDevices.Length];

        // Snap each torture device to the corresponding snap location
        for (int i = 0; i < tortureDevices.Length; i++)
        {
            tortureDevices[i].transform.position = snapLocations[i].position;
            tortureDevices[i].transform.rotation = snapLocations[i].rotation;
        }
    }

    private void Update()
    {
        CheckTortureOrder();
    }

    private void CheckTortureOrder()
    {
        // Check if all devices are in the correct position
        for (int i = 0; i < tortureDevices.Length; i++)
        {
            if (Vector3.Distance(tortureDevices[i].transform.position, snapLocations[i].position) > 0.1f)
            {
                // A device is out of place, break the loop
                return;
            }

            devicesInPlace[i] = true;
        }

        // All devices are in the correct position, start the torturing method
        StartTortureMethod();
    }

    private void StartTortureMethod()
    {
        // Fade out the camera
        // Play the sounds of torture
        // Fade in the camera
    }
}