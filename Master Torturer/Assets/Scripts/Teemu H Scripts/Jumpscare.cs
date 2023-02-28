using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] private CinemachineClearShot virtualJumpscareCamera;
    [SerializeField] private CinemachineClearShot virtualJumpscareFallCamera;

    public GameObject deadmanx; // THIS IS THE SCARY THING

    private bool jumpscareTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !jumpscareTriggered) 
        {
            TortureDeviceScriptCopyTK2 puzzleA = FindObjectOfType<TortureDeviceScriptCopyTK2>();
            if (puzzleA.puzzleAFinished && puzzleA.leftValveInstantiated)
            {
                Debug.Log("JUMPSCARE ENABLED!");
                AudioManager.Instance.Play("Slam10");
                Vector3 deadmanxPosition = new Vector3(-6.098f, 0.11f, 2.215924f);
                Quaternion deadmanxRotation = Quaternion.Euler(0.0274106748f, 124.526f, 0.00338907214f); // create a new Quaternion with the desired rotation
                GameObject newdeadmanx = Instantiate(deadmanx, deadmanxPosition, deadmanxRotation); // instantiate the crawler object with the desired position and rotation

                virtualJumpscareCamera.Priority = 13;
                StartCoroutine(CameraPriority14());
                jumpscareTriggered = true;
            }
        }
    }

    private IEnumerator CameraPriority14() {
        yield return new WaitForSeconds(4f);
        virtualJumpscareFallCamera.Priority = 14;
        StartCoroutine(ResetCameraPriorityAfterDelay());
    }

    private IEnumerator ResetCameraPriorityAfterDelay()
    {
        yield return new WaitForSeconds(4f);
        virtualJumpscareCamera.Priority = 1;
        virtualJumpscareFallCamera.Priority = 1;
    }
}

