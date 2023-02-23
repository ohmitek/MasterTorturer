using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] private CinemachineClearShot virtualJumpscareCamera;
    [SerializeField] private CinemachineClearShot virtualJumpscareFallCamera;

    //public GameObject crawler; // THIS IS THE SCARY THING

    private bool jumpscareTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !jumpscareTriggered) 
        {
            TortureDeviceScriptCopyTK2 puzzleA = FindObjectOfType<TortureDeviceScriptCopyTK2>();
            if (puzzleA.puzzleAFinished && puzzleA.leftValveInstantiated)
            {
                Debug.Log("JUMPSCARE ENABLED!");
                //Vector3 crawlerPosition = new Vector3(-5.941355f, 0.05f, 2.165852f);
                //Quaternion crawlerRotation = Quaternion.Euler(0.0274106748f, 198.334625f, 0.00338907214f); // create a new Quaternion with the desired rotation
                //GameObject newCrawler = Instantiate(crawler, crawlerPosition, crawlerRotation); // instantiate the crawler object with the desired position and rotation

                virtualJumpscareCamera.Priority = 13;
                AudioManager.Instance.Play("Slam10");
                StartCoroutine(CameraPriority14());
                jumpscareTriggered = true;
            }
        }
    }

    private IEnumerator CameraPriority14() {
        yield return new WaitForSeconds(3f);
        virtualJumpscareFallCamera.Priority = 14;
        StartCoroutine(ResetCameraPriorityAfterDelay());
    }

    private IEnumerator ResetCameraPriorityAfterDelay()
    {
        yield return new WaitForSeconds(8f);
        virtualJumpscareCamera.Priority = 1;
        virtualJumpscareFallCamera.Priority = 1;
    }
}

