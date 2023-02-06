using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTestTK : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Puzzle piece") {
            FindObjectOfType<GameManager>().WinGame();
        }
    }
}
