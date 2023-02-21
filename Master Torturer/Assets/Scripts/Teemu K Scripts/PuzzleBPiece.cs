using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBPiece : MonoBehaviour {
    public int pieceId;
    //[SerializeField] Color[] placeHolderColors;
    [SerializeField] Material[] piecePictures;

    void Awake() {
        switch (pieceId) {
            case 1: GetComponent<MeshRenderer>().material = piecePictures[0];
            break;
            case 2:
            GetComponent<MeshRenderer>().material = piecePictures[1];
            break;
            case 3:
            GetComponent<MeshRenderer>().material = piecePictures[2];
            break;
            case 4:
            GetComponent<MeshRenderer>().material = piecePictures[3];
            break;
            case 5:
            GetComponent<MeshRenderer>().material = piecePictures[4];
            break;
            case 6:
            GetComponent<MeshRenderer>().material = piecePictures[5];
            break;
            default: GetComponent<MeshRenderer>().material = piecePictures[6];
            break;
        }
    }

}
