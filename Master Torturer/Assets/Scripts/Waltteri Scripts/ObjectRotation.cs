using System.Collections;
using System.Collections.Generic;
using System.Net.Cache;
using UnityEditor.Rendering;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float rotationSpeed = 90.0f;
    public float speed = 1.0f;
    
    private CubeMovement cubemovement;

    public GameObject valve1;
    public GameObject valve2;

    private void Start()
    {
        cubemovement = FindObjectOfType<CubeMovement>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.V) && cubemovement.cage1.transform.position.y < 12)
        {
            valve1.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            cubemovement.cage1.transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.V) && cubemovement.cage2.transform.position.y < 8.5)
        {
            valve1.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            cubemovement.cage2.transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.C) && cubemovement.cage3.transform.position.y < 7 && cubemovement.cage2.transform.position.y < 10)
        {
            valve2.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            cubemovement.cage3.transform.position += Vector3.up * speed * Time.deltaTime;
            valve2.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            cubemovement.cage2.transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }
}