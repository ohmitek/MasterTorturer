using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script only basicly tries to activate "Outline" script that is from the Asset store 
public class OutlineActivator : MonoBehaviour {
    float rayRange = 10f;

    void Update() {
        //Cast the ray from main camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        //Try to activate outline from puzzle piece tagged object
        if (Physics.Raycast(ray, out hit, rayRange) && hit.collider.tag == "Puzzle piece") {
            Outline outline = hit.collider.gameObject.GetComponent<Outline>();
            outline.enabled = true;
        }
        else {
            GetComponent<Outline>().enabled = false;
        }
    }
}
