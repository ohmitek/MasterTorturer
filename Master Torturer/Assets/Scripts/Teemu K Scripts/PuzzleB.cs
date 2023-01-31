using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleB : MonoBehaviour {
    public Transform[] foundPieceSlots, foundPieceSlotsInUse ,slabSlots;
    public int foundPieceCount;

    void Start() {
        foundPieceSlots = GameObject.Find("Found piece area").GetComponentsInChildren<Transform>();
        foundPieceSlotsInUse = new Transform[foundPieceSlots.Length];
        slabSlots = GameObject.Find("Stone slab").GetComponentsInChildren<Transform>();
    }

    void Update() {
        if (foundPieceCount == 6) {
            GameObject triggerArea = GameObject.Find("Puzzle trigger area");
            triggerArea.GetComponent<PuzzleBTrigger>().enabled = false;
            triggerArea.GetComponent<Collider>().enabled = false;
        }
    }
}
