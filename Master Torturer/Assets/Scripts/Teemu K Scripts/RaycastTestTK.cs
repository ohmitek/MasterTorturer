using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTestTK : MonoBehaviour {
    [SerializeField] float rayLenght;
    void Start() {

    }

    void Update() {
        RaycastHit hit;
        int layermask=1<<7;
        layermask=~layermask;
        bool rayHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLenght, layermask);

        if (rayHit) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log(hit.collider.gameObject);
        }
        else {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLenght, Color.white);
            Debug.Log("No hits");
        }
    }

}
