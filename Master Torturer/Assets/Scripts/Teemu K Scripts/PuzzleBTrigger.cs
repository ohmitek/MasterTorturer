using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBTrigger : MonoBehaviour {
    PuzzleB puzzleB;

    void Start() {
        puzzleB = FindObjectOfType<PuzzleB>();
    }

    void SetToRandomSlot(GameObject piece) {
        //Random Id starts from 1 because we don't want to set position to "Found piece area" in unity hierarchy.
        int randId = Random.Range(1, puzzleB.foundPieceSlots.Length);

        //Check if slot is already in use, if so reroll and try again.
        if (puzzleB.foundPieceSlotsInUse[randId] != null) {
            Debug.Log("Slot already in use, rerolling spot");
            SetToRandomSlot(piece);
        }
        else {
            piece.transform.parent = GameObject.Find("Found piece area").transform;
            piece.transform.position = new Vector3(puzzleB.foundPieceSlots[randId].position.x, puzzleB.foundPieceSlots[randId].position.y, puzzleB.foundPieceSlots[randId].position.z);
            piece.transform.rotation = puzzleB.foundPieceSlots[randId].rotation;
            piece.transform.localScale = puzzleB.foundPieceSlots[randId].transform.localScale;
            //Add unused slot to used slots.
            puzzleB.foundPieceSlotsInUse[randId] = piece.transform;
        }
    }

    void OnTriggerEnter(Collider puzzlePiece) {
        if (puzzlePiece.tag == "Puzzle piece") {
            Debug.Log("Piece hit trigger area");
            puzzlePiece.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            SetToRandomSlot(puzzlePiece.gameObject);

            #region Own slots approach
            /*
            switch (puzzlePiece.GetComponent<PuzzleBPiece>().pieceId) {
                case 1:
                puzzlePiece.transform.parent = GameObject.Find("Found piece area").transform;
                puzzlePiece.transform.position = new Vector3(puzzleB.foundPieceSlots[1].position.x, puzzleB.foundPieceSlots[1].position.y, puzzleB.foundPieceSlots[1].position.z);
                puzzlePiece.transform.rotation = puzzleB.foundPieceSlots[1].rotation;
                break;
                case 2:
                puzzlePiece.transform.parent = GameObject.Find("Found piece area").transform;
                puzzlePiece.transform.position = new Vector3(puzzleB.foundPieceSlots[2].position.x, puzzleB.foundPieceSlots[2].position.y, puzzleB.foundPieceSlots[2].position.z);
                puzzlePiece.transform.rotation = puzzleB.foundPieceSlots[2].rotation;
                break;
                case 3:
                puzzlePiece.transform.parent = GameObject.Find("Found piece area").transform;
                puzzlePiece.transform.position = new Vector3(puzzleB.foundPieceSlots[3].position.x, puzzleB.foundPieceSlots[3].position.y, puzzleB.foundPieceSlots[3].position.z);
                puzzlePiece.transform.rotation = puzzleB.foundPieceSlots[3].rotation;
                break;
                case 4:
                puzzlePiece.transform.parent = GameObject.Find("Found piece area").transform;
                puzzlePiece.transform.position = new Vector3(puzzleB.foundPieceSlots[4].position.x, puzzleB.foundPieceSlots[4].position.y, puzzleB.foundPieceSlots[4].position.z);
                puzzlePiece.transform.rotation = puzzleB.foundPieceSlots[4].rotation;
                break;
                case 5:
                puzzlePiece.transform.parent = GameObject.Find("Found piece area").transform;
                puzzlePiece.transform.position = new Vector3(puzzleB.foundPieceSlots[5].position.x, puzzleB.foundPieceSlots[5].position.y, puzzleB.foundPieceSlots[5].position.z);
                puzzlePiece.transform.rotation = puzzleB.foundPieceSlots[5].rotation;
                break;
                case 6:
                puzzlePiece.transform.parent = GameObject.Find("Found piece area").transform;
                puzzlePiece.transform.position = new Vector3(puzzleB.foundPieceSlots[6].position.x, puzzleB.foundPieceSlots[6].position.y, puzzleB.foundPieceSlots[6].position.z);
                puzzlePiece.transform.rotation = puzzleB.foundPieceSlots[6].rotation;
                break;
            }
            */
            #endregion
            puzzleB.foundPieceCount++;
        }
    }
}
