using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class TortureDeviceScriptCopyTK2 : MonoBehaviour
{
    [SerializeField] private GameObject[] tortureDevices;

    [SerializeField] private Transform[] snapLocations;

    [SerializeField] private CinemachineClearShot virtualTortureCamera;
    [SerializeField] private CinemachineClearShot virtualTortureCameraZoom;

    //AudioManager audioManager = AudioManager.Instance;

    public GameObject LeftValve; // reference to the ring prefab (REWARD from Puzzle A)

    private bool[] devicesInPlace;
    private bool tortureStarted;
    private int selectedDeviceIndex = -1;
    private int disabledSnapRendererIndex = -1; // declare the variable here


    //Teemu K additions
    public bool puzzleBFinished;

    //Teemu H
    public bool puzzleAFinished;
    public bool leftValveInstantiated;

    private void Start() {
        devicesInPlace = new bool[tortureDevices.Length];
    }

    private void Update() {
        //Modified this to use bool
        if (puzzleBFinished) CheckTortureOrder();

        if (puzzleAFinished && !leftValveInstantiated) {
            Vector3 ringPosition = new Vector3(-5.424f, 0.100f, 1.767f); // position to instantiate the ring
            Instantiate(LeftValve, ringPosition, Quaternion.identity); // instantiate the Left Valve at the specified position (REWARD from Puzzle A)
            leftValveInstantiated = true; // set the flag to indicate that the valve has been instantiated
        }

    }

    public void AddTortuteItemToList(GameObject device, int tortureSlot) {
        //Add missing torture device to list so CheckTortureOrder() function can work properly.
        tortureDevices[tortureSlot] = device;
    }

    private void CheckTortureOrder() {
        // Check if all devices are in the correct position
        for (int i = 0; i < tortureDevices.Length; i++) {
            if (Vector3.Distance(tortureDevices[i].transform.position, snapLocations[i].position) > 0.2f) {
                // A device is out of place, reset the devicesInPlace array and break the loop
                for (int j = 0; j < devicesInPlace.Length; j++) {
                    devicesInPlace[j] = false;

                }
                return;
            }

            devicesInPlace[i] = true;
        }
        // All devices are in the correct position and torture hasn't started yet, start the torturing method
        if (!tortureStarted) {
            StartTortureMethod();
            tortureStarted = true;
        }
    }

    //If your GameObject starts to collide with another GameObject with a Collider
    void OnCollisionEnter(Collision piece) {
        if (piece.collider.tag == "Torture Tool") {
            //Output the Collider's GameObject's name
            Debug.Log(piece.collider.name + " placed to the table");

            //Snap each torture device to the corresponding snap location
            for (int i = 0; i < tortureDevices.Length; i++) {
                if (tortureDevices[i].name == piece.collider.name) {
                    selectedDeviceIndex = i;
                    break;
                }
            }

            // Find the closest snap location to the collider
            int closestSnapLocationIndex = -1;
            float closestSnapLocationDistance = float.MaxValue;
            for (int i = 0; i < snapLocations.Length; i++) {
                float distance = Vector3.Distance(piece.transform.position, snapLocations[i].position);
                if (distance < closestSnapLocationDistance) {
                    closestSnapLocationIndex = i;
                    closestSnapLocationDistance = distance;
                }
            }

            // Snap the selected torture device to the closest snap location
            if (selectedDeviceIndex != -1 && closestSnapLocationIndex != -1) {
                tortureDevices[selectedDeviceIndex].transform.position = snapLocations[closestSnapLocationIndex].position;
                tortureDevices[selectedDeviceIndex].transform.rotation = snapLocations[closestSnapLocationIndex].rotation;

                // Disable the mesh renderer of the snap location
                Renderer snapRenderer = snapLocations[closestSnapLocationIndex].GetComponent<Renderer>();
                if (snapRenderer != null) {
                    AudioManager.Instance.Play("Metalsound");
                    snapRenderer.enabled = false;
                }
            }
        }
    }

    void OnCollisionExit(Collision piece) {
        if (piece.collider.tag == "Torture Tool") {
            //Output the Collider's GameObject's name
            Debug.Log(piece.collider.name + " removed from the table");

            // Re-enable the mesh renderer of the device
            Renderer deviceRenderer = piece.collider.gameObject.GetComponent<Renderer>();
            if (deviceRenderer != null) {
                deviceRenderer.enabled = true;
            }

            // Enable the mesh renderer of the snap location
            for (int i = 0; i < snapLocations.Length; i++) {
                if (snapLocations[i].childCount > 0 && snapLocations[i].GetChild(0).gameObject == piece.collider.gameObject) {
                    Renderer snapRenderer = snapLocations[i].GetComponent<Renderer>();
                    if (snapRenderer != null) {
                        snapRenderer.enabled = true;
                        disabledSnapRendererIndex = i; // Store the index of the disabled snap renderer
                    }
                    break;
                }
            }

            // Re-enable the mesh renderer of the previously disabled snap location, if applicable
            if (disabledSnapRendererIndex != -1) {
                Renderer snapRenderer = snapLocations[disabledSnapRendererIndex].GetComponent<Renderer>();
                if (snapRenderer != null) {
                    snapRenderer.enabled = true;
                }
                disabledSnapRendererIndex = -1; // Reset disabledSnapRendererIndex
            }
        }
    }

    private void StartTortureMethod() {
        bool correctOrder = true;
        for (int i = 0; i < devicesInPlace.Length; i++) {
            if (!devicesInPlace[i]) {
                correctOrder = false;
                break;
            }
        }

        if (!correctOrder) {
            for (int i = 0; i < tortureDevices.Length; i++) {
                //Rigidbody deviceRigidbody = tortureDevices[i].GetComponent<Rigidbody>();
                //deviceRigidbody.useGravity = true;
                //deviceRigidbody.AddForce(pile.transform.position - tortureDevices[i].transform.position);
                //tortureDevices[i].transform.position = pile.transform.position;
                //tortureDevices[i].transform.rotation = pile.transform.rotation;
                //selectedDeviceIndex = -1;
                Debug.Log("Incorrect order!");
            }
        } else {
            Debug.Log("TORTURE STARTED!");

            AudioManager.Instance.Play("Puzzledone");

            virtualTortureCamera.Priority = 11;

            // Wait for 8 seconds and then change the camera priority to 12
            StartCoroutine(ResetCameraPriority());

            tortureStarted = true;
        }
    }

    private IEnumerator ResetCameraPriority() {
        yield return new WaitForSeconds(6f);
        virtualTortureCameraZoom.Priority = 12;
        StartCoroutine(ResetCameraPriorityAfterDelay());
    }

    private IEnumerator ResetCameraPriorityAfterDelay()
    {
        AudioManager.Instance.Play("MaleScream02");
        yield return new WaitForSeconds(7f);
        virtualTortureCameraZoom.Priority = 1;
        virtualTortureCamera.Priority = 1;
        //Vector3 ringPosition = new Vector3(-5.424f, 0.100f, 1.767f); // position to instantiate the ring
        //Instantiate(LeftValve, ringPosition, Quaternion.identity); // instantiate the Left Valve at the specified position (REWARD from Puzzle A)
        puzzleAFinished = true;
        //if (puzzleAFinished = true;) {
        //            Vector3 ringPosition = new Vector3(-5.424f, 0.100f, 1.767f); // position to instantiate the ring
        //            Instantiate(LeftValve, ringPosition, Quaternion.identity); // instantiate the Left Valve at the specified position (REWARD from Puzzle A)
        //}
    }


}