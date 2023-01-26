using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTestTK : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Puzzle piece") {
            GameObject.Find("PlaceHolderManager").GetComponent<PlaceholderManager>().WinGame();
        }
    }
}
