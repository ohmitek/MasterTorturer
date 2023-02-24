using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using Cinemachine;

public class PuzzleD : MonoBehaviour {
    public PuzzleState puzzleState;
    public Vector3[] gageStartPos, cageHeightlevels;
    [SerializeField] GameObject[] cages;
    public PuzzleDCage[] cageScripts;
    [Tooltip("Value wanted for the distance between levels")][SerializeField] float cageLevelDistance;

    void Start() {
        puzzleState = PuzzleState.Unfinished;

        //Find all cages from the scene
        cages = GameObject.FindGameObjectsWithTag("Cage");

        //Create new empty array with amount of levels wanted
        cageHeightlevels = new Vector3[4];

        //Same for cage scripts
        cageScripts = new PuzzleDCage[3];

        //Set cage height level heights
        cageHeightlevels[0].y = cages[0].transform.position.y;
        cageHeightlevels[1].y = cageHeightlevels[0].y - cageLevelDistance;
        cageHeightlevels[2].y = cageHeightlevels[1].y - cageLevelDistance;
        cageHeightlevels[3].y = cageHeightlevels[2].y - cageLevelDistance;

        //Set scripts to their array
        cageScripts[0] = cages[0].GetComponent<PuzzleDCage>();
        cageScripts[1] = cages[1].GetComponent<PuzzleDCage>();
        cageScripts[2] = cages[2].GetComponent<PuzzleDCage>();
    }

    void Update() {
        #region Debug test buttons, uncomment if you want to test
        /*
        //Debug test buttons "Vasen vastapäivä" simulation
        if (Input.GetKeyDown(KeyCode.K) && puzzleState != PuzzleState.Finished) {
            //kutsu cagen script kautta meno suuntaa yms
            cageScripts[2].StartCoroutine("MoveCageDown");
            cageScripts[1].StartCoroutine("MoveCageUp");
        }
        //"Vasen myötäpäivä" simulation
        else if (Input.GetKeyDown(KeyCode.L) && puzzleState != PuzzleState.Finished) {
            cageScripts[1].StartCoroutine("MoveCageDown");
            cageScripts[0].StartCoroutine("MoveCageUp");
        }
        //"Oikea vastapäivä"
        else if (Input.GetKeyDown(KeyCode.O) && puzzleState != PuzzleState.Finished) {
            cageScripts[0].StartCoroutine("MoveCageDown");
        }
        //"Oikea myötäpäivä"
        else if (Input.GetKeyDown(KeyCode.P) && puzzleState != PuzzleState.Finished) {
            cageScripts[2].StartCoroutine("MoveCageUp");
        }
        */
        #endregion
        //Check if cages are in correct spots to finish puzzle
        if (puzzleState != PuzzleState.Finished && cages[0].transform.position.y == cageHeightlevels[3].y && cages[1].transform.position.y == cageHeightlevels[2].y
            && cages[2].transform.position.y == cageHeightlevels[1].y) {
            Debug.Log("Puzzle is correct!");
            puzzleState = PuzzleState.Finished;

            PuzzleDValve[] valves = FindObjectsOfType<PuzzleDValve>();
            //Destroy valve rigidbodies, so they can't be moved after the puzzle is done.
            Destroy(valves[0].valveRb);
            Destroy(valves[1].valveRb);

            //Play Puzzle is done sound and activate reward camera
            AudioManager aM = FindObjectOfType<AudioManager>();
            aM.Play("Puzzledone");
            StartCoroutine(RewardCamera());
            StartCoroutine(RewardCameraFadeOut());
        }

    }

    IEnumerator RewardCamera() {
        CinemachineVirtualCamera dollyCam = GameObject.Find("DollyCamera").GetComponent<CinemachineVirtualCamera>();
        CinemachineDollyCart dollyCart = FindObjectOfType<CinemachineDollyCart>();
        CinemachineSmoothPath dollyTrack = FindObjectOfType<CinemachineSmoothPath>();
        GameManager gM = FindObjectOfType<GameManager>();
        yield return new WaitForSeconds(1);
        dollyCam.Priority = 11;
        dollyCart.m_Speed = 1f;
        yield return new WaitUntil(() => dollyCart.m_Position >= dollyTrack.PathLength);
        yield return new WaitForSeconds(1f); //Extra time to make sure dolly cart and fadeout have finished
        gM.WinGame();
    }
    IEnumerator RewardCameraFadeOut() {
        CinemachineStoryboard storyboard = GameObject.Find("DollyCamera").GetComponent<CinemachineStoryboard>();
        yield return new WaitForSeconds(1);
        while(storyboard.m_Alpha <= 1f) {
            storyboard.m_Alpha += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    //Not like this
    /*
    void MoveCageDown(GameObject cage, Vector3 direction) {
        //Downwards movement stuff
        if (cage.GetComponent<PuzzleDCage>().cageState == CageState.ZeroLevel && cage.transform.position.y > cageHeightlevels[1].y) {
            cage.transform.position += cageMoveSpeed * Time.deltaTime * direction;
            if (cage.transform.position.y < cageHeightlevels[1].y) {
                cageMoving3 = false;
                cage.GetComponent<PuzzleDCage>().cageState = CageState.FirstLevel;
                cage.transform.position = new Vector3(cage.transform.position.x, cageHeightlevels[1].y);
            }
        }
        else if (cage.GetComponent<PuzzleDCage>().cageState == CageState.FirstLevel && cage.transform.position.y > cageHeightlevels[2].y) {
            cage.transform.position += cageMoveSpeed * Time.deltaTime * direction;
            if (cage.transform.position.y < cageHeightlevels[2].y) {
                cageMoving3 = false;
                cage.GetComponent<PuzzleDCage>().cageState = CageState.SecondLevel;
                cage.transform.position = new Vector3(cage.transform.position.x, cageHeightlevels[2].y);

            }
        }
        else if (cage.GetComponent<PuzzleDCage>().cageState == CageState.SecondLevel && cage.transform.position.y >= cageHeightlevels[3].y) {
            cage.transform.position += cageMoveSpeed * Time.deltaTime * direction;
            if (cage.transform.position.y <= cageHeightlevels[3].y || cage.transform.position.y == cageHeightlevels[3].y) {
                cageMoving3 = false;
                cage.GetComponent<PuzzleDCage>().cageState = CageState.ThirdLevel;
                cage.transform.position = new Vector3(cage.transform.position.x, cageHeightlevels[3].y);
            }
        }
        //else if(cage.GetComponent<PuzzleDCage>().cageState == CageState.ThirdLevel && cage.transform.position)
    }

    void MoveCageUp(GameObject cage, Vector3 direction) {
        //Upwards movement stuff
        if (cage.GetComponent<PuzzleDCage>().cageState == CageState.ThirdLevel && cage.transform.position.y < cageHeightlevels[2].y) {
            cage.transform.position += cageMoveSpeed * Time.deltaTime * direction;
            if (cage.transform.position.y > cageHeightlevels[2].y) {
                cageMoving2 = false;
                cage.GetComponent<PuzzleDCage>().cageState = CageState.SecondLevel;
                cage.transform.position = new Vector3(cage.transform.position.x, cageHeightlevels[2].y);
            }
        }
        else if (cage.GetComponent<PuzzleDCage>().cageState == CageState.SecondLevel && cage.transform.position.y < cageHeightlevels[1].y) {
            cage.transform.position += cageMoveSpeed * Time.deltaTime * direction;
            if (cage.transform.position.y > cageHeightlevels[1].y) {
                cageMoving2 = false;
                cage.GetComponent<PuzzleDCage>().cageState = CageState.FirstLevel;
                cage.transform.position = new Vector3(cage.transform.position.x, cageHeightlevels[1].y);

            }
        }
        else if (cage.GetComponent<PuzzleDCage>().cageState == CageState.FirstLevel && cage.transform.position.y <= cageHeightlevels[0].y) {
            cage.transform.position += cageMoveSpeed * Time.deltaTime * direction;
            if (cage.transform.position.y >= cageHeightlevels[0].y || cage.transform.position.y == cageHeightlevels[0].y) {
                cageMoving2 = false;
                cage.GetComponent<PuzzleDCage>().cageState = CageState.ZeroLevel;
                cage.transform.position = new Vector3(cage.transform.position.x, cageHeightlevels[0].y);
            }
        }
    }
    */
}
