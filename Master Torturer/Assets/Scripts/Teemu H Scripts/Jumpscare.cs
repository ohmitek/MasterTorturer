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
                AudioManager.Instance.Play("MonsterRoarGrowl024");
                Vector3 deadmanxPosition = new Vector3(-6.476f, 0.08f, 2.29f);
                Quaternion deadmanxRotation = Quaternion.Euler(0f, 114.1f, 0f); // create a new Quaternion with the desired rotation
                AudioManager.Instance.Play("Slam10");
                GameObject newdeadmanx = Instantiate(deadmanx, deadmanxPosition, deadmanxRotation); // instantiate the crawler object with the desired position and rotation

                virtualJumpscareCamera.Priority = 13;
                //StartCoroutine(CameraPriority14());
                jumpscareTriggered = true;
                StartCoroutine(ResetCameraPriorityAfterDelay());

            }
        }
    }

    //private IEnumerator CameraPriority14() {
    //    yield return new WaitForSeconds(4f);
    //    virtualJumpscareFallCamera.Priority = 14;
    //    StartCoroutine(ResetCameraPriorityAfterDelay());
    //}

    private IEnumerator ResetCameraPriorityAfterDelay()
    {
        yield return new WaitForSeconds(8f);
        virtualJumpscareCamera.Priority = 1;
        virtualJumpscareFallCamera.Priority = 1;
    }
}

