using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
public enum PuzzleState { WaitingPieces, InProgress, Unfinished, Finished }
public class PuzzleB : MonoBehaviour {
    public PuzzleState puzzleState;
    public Transform[] foundPieceSlots, foundPieceSlotsInUse, slabSlots;
    public int foundPieceCount, piecesNeeded;
    [SerializeField] int piecesInCorrectSpot;
    bool originSpotChecked;
    Vector3 originalSpot;

    [SerializeField] GameObject[] puzzleRewards;
    [SerializeField] Transform[] rewardSpawnSpots;
    [SerializeField] GameObject puzzleCam, mainCam;
    Ray mouseRay;

    GameManager gm;

    void Start() {
        puzzleState = PuzzleState.WaitingPieces;
        gm = FindObjectOfType<GameManager>();

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
            case PuzzleState.Unfinished or PuzzleState.Finished:
            puzzleCam.SetActive(false);
            mainCam.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            break;
        }

        RaycastHit ray;
        //Set ray to hit only Puzzle piece layer
        int layerMask = 1 << 8;
        bool rayHit = Physics.Raycast(mouseRay.origin, mouseRay.direction, out ray, 5f, layerMask);
        
        //Puzzle piece moving in the table
        if (Input.GetMouseButton(0) && puzzleState == PuzzleState.InProgress) {
            if (rayHit) {
                //Look at where piece was and store it's point for possible reset
                if (!originSpotChecked) {
                    originalSpot = ray.transform.position;
                    originSpotChecked = true;
                }

                Debug.DrawRay(mouseRay.origin, mouseRay.direction * ray.distance, Color.red);
                ray.transform.position = new Vector3(ray.point.x, ray.transform.position.y, ray.point.z);

                //Rotate while holding a piece, kind of works. Needs fine tuning
                if(Input.GetKeyDown(KeyCode.E)) ray.transform.Rotate(0f, 90f, 0f, Space.World);
            }
            else {
                Debug.DrawRay(mouseRay.origin, mouseRay.direction * ray.distance, Color.white);
            }
        }
        //Mouse button release
        else if (Input.GetMouseButtonUp(0) && puzzleState == PuzzleState.InProgress){
            try {
                int pieceId = ray.transform.GetComponent<PuzzleBPiece>().pieceId;
                
                //Check if piece is close enough to it's spot
                if (ray.transform.position.x - slabSlots[pieceId].position.x >= -0.2f && ray.transform.position.x - slabSlots[pieceId].position.x <= 0.2f
                    && ray.transform.position.z - slabSlots[pieceId].position.z >= -0.2f && ray.transform.position.z - slabSlots[pieceId].position.z <= 0.2f) {
                    Debug.Log("Close enough");
                    ray.transform.position = slabSlots[pieceId].position;

                    //Set piece tag and layer back to default so it cannot be picked up again or moved.
                    ray.transform.tag = "Untagged";
                    ray.transform.gameObject.layer = 0;
                    ray.rigidbody.constraints = RigidbodyConstraints.FreezeAll;

                    piecesInCorrectSpot++;
                    if (piecesInCorrectSpot == piecesNeeded) {
                        puzzleState = PuzzleState.Finished;
                        //Instaniate rewards and "mark" puzzle as finished in GameManager
                        Instantiate(puzzleRewards[0], rewardSpawnSpots[0].position, puzzleRewards[0].transform.rotation);
                        Instantiate(puzzleRewards[1], rewardSpawnSpots[1].position, puzzleRewards[1].transform.rotation);
                        gm.PuzzleDone();
                    }
                    
                }
                else {
                    Debug.Log("Not close enough");
                    //Reset the piece back to its original spot
                    if(originSpotChecked) {
                        ray.transform.position = originalSpot;
                        originSpotChecked = false;
                    }
                }
            }
            catch {
                Debug.Log("Not releasing mouse button from puzzle piece");
            }
        }

    }
}
