using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PuzzleState { WaitingPieces, InProgress, Unfinished, Finished }
public class PuzzleB : MonoBehaviour {
    public PuzzleState puzzleState;
    public Transform[] foundPieceSlots, foundPieceSlotsInUse, slabSlots;
    public int foundPieceCount, piecesNeeded;

    [SerializeField] GameObject puzzleCam, mainCam, rayOriginObject;
    Camera pCam, mCam;
    Ray mouseRay;

    void Start() {
        puzzleState = PuzzleState.WaitingPieces;

        //Cameras
        pCam = puzzleCam.GetComponent<Camera>();
        mCam = mainCam.GetComponent<Camera>();

        foundPieceSlots = GameObject.Find("Found piece area").GetComponentsInChildren<Transform>();
        foundPieceSlotsInUse = new Transform[foundPieceSlots.Length];
        slabSlots = GameObject.Find("Stone slab").GetComponentsInChildren<Transform>();
    }

    void Update() {
        //Mouse ray origin
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (foundPieceCount == piecesNeeded & puzzleState == PuzzleState.WaitingPieces) {
            GameObject triggerArea = GameObject.Find("Puzzle trigger area");
            triggerArea.GetComponent<PuzzleBTrigger>().enabled = false;
            triggerArea.GetComponent<Collider>().enabled = false;

            puzzleState = PuzzleState.InProgress;
        }

        //Debug test button
        if (Input.GetKeyDown(KeyCode.Escape) && puzzleState == PuzzleState.InProgress) {
            puzzleState = PuzzleState.Unfinished;
        }

        //Switch that controls player and puzzle camera
        switch (puzzleState) {
            case PuzzleState.InProgress:
            puzzleCam.SetActive(true);
            mainCam.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            break;
            case PuzzleState.Unfinished:
            puzzleCam.SetActive(false);
            mainCam.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            break;
        }

        RaycastHit ray;
        int layerMask = 1 << 8;
        bool rayHit = Physics.Raycast(mouseRay.origin, mouseRay.direction, out ray, 3f, layerMask);
        
        //Puzzle piece moving in the table
        if (Input.GetMouseButton(0) && puzzleState == PuzzleState.InProgress) {
            if (rayHit) {
                Debug.DrawRay(mouseRay.origin, mouseRay.direction * ray.distance, Color.red);
                Debug.Log(ray.transform.name + " hit" + ray.transform.position);
                //ray.transform.position = ray.transform.position + new Vector3(0f, 0.2f, 0f);
                ray.transform.position = new Vector3(ray.point.x, ray.transform.position.y, ray.point.z);
            }
            else {
                Debug.DrawRay(mouseRay.origin, mouseRay.direction * ray.distance, Color.white);
            }
        }

    }
}
