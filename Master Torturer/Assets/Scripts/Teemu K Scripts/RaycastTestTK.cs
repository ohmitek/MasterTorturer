using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTestTK : MonoBehaviour {
    [SerializeField] float rayLenght;
    void Start() {

    }

    void Update() {
        RaycastHit hit;
        bool rayHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLenght);

        if (rayHit) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
        }
        else {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLenght, Color.white);
            Debug.Log("No hits");
        }
    }

}
