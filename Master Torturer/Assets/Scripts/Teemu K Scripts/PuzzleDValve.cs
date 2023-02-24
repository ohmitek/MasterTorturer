using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ValveType { Left, Right };
public class PuzzleDValve : MonoBehaviour {
    public ValveType valveType;
    public Rigidbody valveRb;
    public bool attached;
    [SerializeField] float rotateAmount, rotateSpeed;
    [SerializeField] Vector3 currentEulerAngles;
    [SerializeField] Quaternion currentRotation;

    void Start() {
        valveRb = GetComponent<Rigidbody>();
        currentEulerAngles = new Vector3(0f, 90f, 90f);
    }

    void Update() {
        //Debug testing
        //if (Input.GetKeyDown(KeyCode.N)) {
        //    StartCoroutine(RotateCounterClockwise());
        //}
        //if (Input.GetKeyDown(KeyCode.M)) {
        //    StartCoroutine(RotateClockwise());
        //}

        if (attached) {
            currentEulerAngles = new Vector3(currentEulerAngles.x, 90f, 90f);
            currentRotation.eulerAngles = currentEulerAngles;
            transform.rotation = currentRotation;
        }
        
    }

    IEnumerator RotateCounterClockwise() {
        while (currentEulerAngles.x > -rotateAmount) {
            currentEulerAngles.x -= rotateSpeed * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Debug.Log("Rotate is done Counterclockwise");
        //Reset euler.x back to 0
        currentEulerAngles.x = 0f;
    }
    IEnumerator RotateClockwise() {
        while (currentEulerAngles.x < rotateAmount) {
            currentEulerAngles.x += rotateSpeed * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Debug.Log("Rotate is done Clockwise");
        //Reset euler.x back to 0
        currentEulerAngles.x = 0f;
    }
}
