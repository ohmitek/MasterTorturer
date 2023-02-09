using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CubeMovement : MonoBehaviour
{
    public float speed = 1.0f;

    public GameObject cage1;
    public GameObject cage2;
    public GameObject cage3;

    private Vector3 cage1StartPos;
    private Vector3 cage1EndPos;
    private Vector3 cage2StartPos;
    private Vector3 cage2EndPos;
    private Vector3 cage3StartPos;
    private Vector3 cage3EndPos;


    private void Start()
    {
        cage1StartPos = cage1.transform.position;
        cage1EndPos = cage1.transform.position;
        cage2StartPos = cage1.transform.position;
        cage2EndPos = cage1.transform.position;
        cage3StartPos = cage1.transform.position;
        cage3EndPos = cage1.transform.position;
    }
    private void Update()
    {

    }
}