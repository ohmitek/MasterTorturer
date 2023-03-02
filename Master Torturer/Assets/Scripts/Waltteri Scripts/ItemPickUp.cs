using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] Transform holdArea;
    private GameObject holdObj;
    private Rigidbody holdObjRB;

    [SerializeField] private float pickupRange = 10.0f;
    [SerializeField] private float pickupForce = 150.0f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (holdObj == null)
            {
                int layermask = 1 << 7;
                layermask = ~layermask;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layermask))
                { 
                    //Teemu K addition
                    try {
                        PuzzleD pD = FindObjectOfType<PuzzleD>();
                        PuzzleDValve valve = hit.transform.GetComponent<PuzzleDValve>();
                        if (valve.valveType == ValveType.Left && valve.attached && hit.point.x < valve.transform.position.x && pD.puzzleState != PuzzleState.Finished) {
                            //Debug.Log(hit.point.x);
                            pD.cageScripts[2].StartCoroutine("MoveCageDown");
                            pD.cageScripts[1].StartCoroutine("MoveCageUp");
                            valve.StartCoroutine("RotateCounterClockwise");
                        }
                        else if (valve.valveType == ValveType.Left && valve.attached && hit.point.x > valve.transform.position.x && pD.puzzleState != PuzzleState.Finished) {
                            //Debug.Log(hit.point.x);
                            pD.cageScripts[1].StartCoroutine("MoveCageDown");
                            pD.cageScripts[0].StartCoroutine("MoveCageUp");
                            valve.StartCoroutine("RotateClockwise");
                        }
                        else if (valve.valveType == ValveType.Right && valve.attached && hit.point.x < valve.transform.position.x && pD.puzzleState != PuzzleState.Finished) {
                            //Debug.Log(hit.point.x);
                            pD.cageScripts[0].StartCoroutine("MoveCageDown");
                            valve.StartCoroutine("RotateCounterClockwise");
                        }
                        else if (valve.valveType == ValveType.Right && valve.attached && hit.point.x > valve.transform.position.x && pD.puzzleState != PuzzleState.Finished) {
                            //Debug.Log(hit.point.x);
                            pD.cageScripts[2].StartCoroutine("MoveCageUp");
                            valve.StartCoroutine("RotateClockwise");
                        }
                        else {
                            PickupObject(hit.transform.gameObject);
                        }
                    }
                    catch {
                        PickupObject(hit.transform.gameObject);
                        Debug.Log("Doimi pls");
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                        Debug.Log(hit.collider.gameObject);
                    }
                }
            }
            else
            {
                DropObject();
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * pickupRange, Color.white);
                Debug.Log("No hits");
            }
        }
        if (holdObj != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(holdObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - holdObj.transform.position);
            holdObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            holdObjRB = pickObj.GetComponent<Rigidbody>();
            holdObjRB.useGravity = false;
            holdObjRB.drag = 10;
            holdObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            holdObjRB.transform.parent = holdArea;
            holdObj = pickObj;
        }
    }

    public void DropObject()
    {
        holdObjRB.useGravity = true;
        holdObjRB.drag = 1;
        holdObjRB.constraints = RigidbodyConstraints.None;

        holdObjRB.transform.parent = null;
        holdObj = null;
    }
}
