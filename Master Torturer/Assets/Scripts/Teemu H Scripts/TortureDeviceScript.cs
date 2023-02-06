using System.Collections;
using UnityEngine;

public class TortureDeviceScript : MonoBehaviour
{
    [SerializeField] private GameObject[] tortureDevices;

    [SerializeField] private Transform[] snapLocations;

    [SerializeField] private GameObject pile;


    private bool[] devicesInPlace;
    private bool tortureStarted;
    private int selectedDeviceIndex = -1;
    
    private void Start()
    {
        devicesInPlace = new bool[tortureDevices.Length];
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
            if (Vector3.Distance(tortureDevices[i].transform.position, snapLocations[i].position) > 0.2f)
            {
                // A device is out of place, reset the devicesInPlace array and break the loop
                for (int j = 0; j < devicesInPlace.Length; j++)
                {
                    devicesInPlace[j] = false;

                }
                return;
            }

            devicesInPlace[i] = true;
        }
            // All devices are in the correct position and torture hasn't started yet, start the torturing method
        if (!tortureStarted)
        {
            StartTortureMethod();
            tortureStarted = true;
        }
    }

    //If your GameObject starts to collide with another GameObject with a Collider
    void OnCollisionEnter(Collision piece)
    {
      if (piece.collider.tag == "Puzzle piece")
      {
        //Output the Collider's GameObject's name
        Debug.Log(piece.collider.name + " placed to the table");

        //Snap each torture device to the corresponding snap location
        for (int i = 0; i < tortureDevices.Length; i++)
        if (tortureDevices[i].name == piece.collider.name)
        {
          selectedDeviceIndex = i;
          break;
        }
      }

      // Find the closest snap location to the collider
      int closestSnapLocationIndex = -1;
      float closestSnapLocationDistance = float.MaxValue;
      for (int i = 0; i < snapLocations.Length; i++)
      {
        float distance = Vector3.Distance(piece.transform.position, snapLocations[i].position);
        if (distance < closestSnapLocationDistance)
        {
          closestSnapLocationIndex = i;
          closestSnapLocationDistance = distance;
        }
      }

      // Snap the selected torture device to the closest snap location
      if (selectedDeviceIndex != -1 && closestSnapLocationIndex != -1)
      {
        tortureDevices[selectedDeviceIndex].transform.position = snapLocations[closestSnapLocationIndex].position;
        tortureDevices[selectedDeviceIndex].transform.rotation = snapLocations[closestSnapLocationIndex].rotation;
      }

    }

    private void StartTortureMethod()
    {
        bool correctOrder = true;
        for (int i = 0; i < devicesInPlace.Length; i++)
        {
            if (!devicesInPlace[i])
            {
                correctOrder = false;
                break;
            }
        }

        if (!correctOrder)
        {
            
            for (int i = 0; i < tortureDevices.Length; i++)
            {
                Rigidbody deviceRigidbody = tortureDevices[i].GetComponent<Rigidbody>();
                deviceRigidbody.useGravity = true;
                deviceRigidbody.AddForce(pile.transform.position - tortureDevices[i].transform.position);
                //tortureDevices[i].transform.position = pile.transform.position;
                //tortureDevices[i].transform.rotation = pile.transform.rotation;
                //selectedDeviceIndex = -1;
            }
        }
        else
        {
            Debug.Log("CORRECT ORDER! START TORTURE!");
            // Fade out the camera
            // Play the sounds of torture
            // Fade in the camera
        }
    }
  }