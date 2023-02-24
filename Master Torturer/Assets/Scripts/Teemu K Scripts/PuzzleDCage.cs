using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CageState { ZeroLevel, FirstLevel, SecondLevel, ThirdLevel }
public class PuzzleDCage : MonoBehaviour {
    public CageState cageState;
    PuzzleD puzzleD;
    AudioManager aM;
    [SerializeField] float cageMoveSpeed;

    void Start() {
        cageState = CageState.ZeroLevel;
        puzzleD = FindObjectOfType<PuzzleD>();
        aM = FindObjectOfType<AudioManager>();
    }

    void MoveCage(Vector3 direction) {
        transform.position += cageMoveSpeed * Time.deltaTime * direction;
    }
    
    public IEnumerator MoveCageDown() {
        aM.Play("chage_move");
        while (cageState == CageState.ZeroLevel && transform.position.y > puzzleD.cageHeightlevels[1].y) {
            yield return new WaitForSeconds(Time.deltaTime);
            MoveCage(Vector3.down);
        }
        while (cageState == CageState.FirstLevel && transform.position.y > puzzleD.cageHeightlevels[2].y) {
            yield return new WaitForSeconds(Time.deltaTime);
            MoveCage(Vector3.down);
        }
        while (cageState == CageState.SecondLevel && transform.position.y > puzzleD.cageHeightlevels[3].y) {
            yield return new WaitForSeconds(Time.deltaTime);
            MoveCage(Vector3.down);
        }
        CheckLevel1();
        aM.Stop("chage_move");
        //Debug.Log("Olet t‰‰ll‰ 1");
    }
    public IEnumerator MoveCageUp() {
        aM.Play("chage_move");
        while (cageState == CageState.ThirdLevel && transform.position.y < puzzleD.cageHeightlevels[2].y) {
            yield return new WaitForSeconds(Time.deltaTime);
            MoveCage(Vector3.up);
        }
        while (cageState == CageState.SecondLevel && transform.position.y < puzzleD.cageHeightlevels[1].y) {
            yield return new WaitForSeconds(Time.deltaTime);
            MoveCage(Vector3.up);
        }
        while (cageState == CageState.FirstLevel && transform.position.y < puzzleD.cageHeightlevels[0].y) {
            yield return new WaitForSeconds(Time.deltaTime);
            MoveCage(Vector3.up);
        }
        CheckLevel2();
        aM.Stop("chage_move");
        //Debug.Log("Olet t‰‰ll‰ 2");
    }

    void CheckLevel1() {
        //Check if where cage is and update its state to correct one
        if(cageState == CageState.ZeroLevel && transform.position.y < puzzleD.cageHeightlevels[1].y) {
            transform.position = new Vector3(transform.position.x, puzzleD.cageHeightlevels[1].y, transform.position.z);
            cageState = CageState.FirstLevel;
        }
        else if (cageState == CageState.FirstLevel && transform.position.y < puzzleD.cageHeightlevels[2].y) {
            transform.position = new Vector3(transform.position.x, puzzleD.cageHeightlevels[2].y, transform.position.z);
            cageState = CageState.SecondLevel;
        }
        else if (cageState == CageState.SecondLevel && transform.position.y < puzzleD.cageHeightlevels[3].y) {
            transform.position = new Vector3(transform.position.x, puzzleD.cageHeightlevels[3].y, transform.position.z);
            cageState = CageState.ThirdLevel;
        }
    }
    void CheckLevel2() {
        //Check if where cage is and update its state to correct one
        if (cageState == CageState.ThirdLevel && transform.position.y > puzzleD.cageHeightlevels[2].y) {
            transform.position = new Vector3(transform.position.x, puzzleD.cageHeightlevels[2].y, transform.position.z);
            cageState = CageState.SecondLevel;
        }
        else if (cageState == CageState.SecondLevel && transform.position.y > puzzleD.cageHeightlevels[1].y) {
            transform.position = new Vector3(transform.position.x, puzzleD.cageHeightlevels[1].y, transform.position.z);
            cageState = CageState.FirstLevel;
        }
        else if (cageState == CageState.FirstLevel && transform.position.y > puzzleD.cageHeightlevels[0].y) {
            transform.position = new Vector3(transform.position.x, puzzleD.cageHeightlevels[0].y, transform.position.z);
            cageState = CageState.ZeroLevel;
        }
    }
}
