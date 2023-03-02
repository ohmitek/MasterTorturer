using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDValveSlot : MonoBehaviour {
    AudioManager aM;
    void Start() {
        aM = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other) {
        PuzzleDValve valve = other.GetComponent<PuzzleDValve>();
        //HighlightOutline outline = other.gameObject.GetComponent<HighlightOutline>();

        if(other.tag == "Valve" && valve.valveType == ValveType.Left) {
            Debug.Log("Valve entered trigger");
            valve.attached = true;
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            other.GetComponent<Rigidbody>().isKinematic = true;
            //outline.enabled = false;
            other.transform.parent = GameObject.Find("Valves").transform;
            other.transform.position = transform.position;

            //Play the place item sound
            aM.Play("MetalSound");
            //Lastly destroy slot object
            Destroy(gameObject);
        }
    }

}
