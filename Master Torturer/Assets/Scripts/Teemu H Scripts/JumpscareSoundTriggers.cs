using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareSoundTriggers : MonoBehaviour
{
    private bool jumpscareSoundTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !jumpscareSoundTriggered) 
        {          
                AudioManager.Instance.Play("Tension1");
                jumpscareSoundTriggered = true;
        }
    }       
}
