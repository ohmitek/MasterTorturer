using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    //This can be modifed in the unity inspector
    [Tooltip("Time for the Coroutine timer.")][SerializeField] float time;
    bool timerOn;

    [Tooltip("Time value for style 2 timer.")][SerializeField] float maxTime;
    float time2;
    bool point1, point2, point3, point4;

    MonsterMovement mm;
    GameManager gm;

    void Start() {
        time2 = maxTime;
        gm = FindObjectOfType<GameManager>();
        mm = FindObjectOfType<MonsterMovement>();
    }

    void Update() {
        //Style number 2 for timer. Checks GameManager state and lowers the value of time based on that.
        switch (gm.gameState) {
            case GameState.InGame:
            time2 -= Time.deltaTime;
            break;
        }
        //Possible events that can happen when timer reaches certain point.
        if (time2 <= 0 && !point4) {
            point4 = true;
            gm.GameOver();
        } 
        else if (time2 <= maxTime * 0.3 && !point3) {
            point3 = true;
            DisplayTime(time2);
            mm.MoveToNextPosition();
        }
        else if (time2 <= maxTime * 0.5 && !point2) {
            point2 = true;
            DisplayTime(time2);
            mm.MoveToNextPosition();
        }
        else if (time2 <= maxTime * 0.8 && !point1) {
            point1 = true;
            DisplayTime(time2);
            mm.MoveToNextPosition();
        }

        //Test button for Coroutine timer
        if (Input.GetKey(KeyCode.T) && !timerOn) {
            StartCoroutine(CoroutineTimer());
        }
    }

    void DisplayTime(float time) {
        Debug.Log("Time left: " + time + " seconds");
    }

    //This is style number 1 to do this. Good if timer doesn't have to be paused at all.
    IEnumerator CoroutineTimer() {
        timerOn = true;
        Debug.Log("Timer started");
        yield return new WaitForSeconds(time);
        Debug.Log("Time ran out! Do something here.");
        gm.gameState = GameState.GameOver;
        timerOn = false;
    }
}
