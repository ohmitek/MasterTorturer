using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    //This can be modifed in the unity inspector
    [Tooltip("Time for the timer. Press T to activate")][SerializeField] float time;

    bool timerOn;

    void Update() {
        //Test button
        if (Input.GetKey(KeyCode.T) && !timerOn) {
            StartCoroutine(CoroutineTimer());
        }
    }

    IEnumerator CoroutineTimer() {
        timerOn = true;
        Debug.Log("Timer started");
        yield return new WaitForSeconds(time);
        Debug.Log("Time ran out! Do something here.");
        //Placeholder line here
        GameObject.Find("PlaceHolderManager").GetComponent<PlaceholderManager>().gameState = GameState.GameOver;
        timerOn = false;
    }
}
