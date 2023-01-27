using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBPiece : MonoBehaviour {
    public int pieceId;
    [SerializeField] Color[] placeHolderColors;

    void Start() {
        switch (pieceId) {
            case 1: GetComponent<MeshRenderer>().material.color = placeHolderColors[0];
            break;
            case 2:
            GetComponent<MeshRenderer>().material.color = placeHolderColors[1];
            break;
            case 3:
            GetComponent<MeshRenderer>().material.color = placeHolderColors[2];
            break;
            case 4:
            GetComponent<MeshRenderer>().material.color = placeHolderColors[3];
            break;
            case 5:
            GetComponent<MeshRenderer>().material.color = placeHolderColors[4];
            break;
            case 6:
            GetComponent<MeshRenderer>().material.color = placeHolderColors[5];
            break;
            default: GetComponent<MeshRenderer>().material.color = placeHolderColors[6];
            break;
        }
    }

}
