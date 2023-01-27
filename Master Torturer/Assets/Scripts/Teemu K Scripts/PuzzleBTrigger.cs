using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBTrigger : MonoBehaviour {
    PuzzleB puzzleB;

    void Start() {
        puzzleB = FindObjectOfType<PuzzleB>();
    }

    void OnTriggerEnter(Collider puzzlePiece) {
        if (puzzlePiece.tag == "Puzzle piece") {
            Debug.Log("Piece hit trigger area");
            puzzlePiece.GetComponent<Rigidbody>().useGravity = false;

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
            puzzleB.foundPieceCount++;
        }
    }
}
