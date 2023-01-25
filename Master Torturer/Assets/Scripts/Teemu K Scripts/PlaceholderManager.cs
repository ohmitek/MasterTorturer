using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, Paused, GameOver, InGame }
public class PlaceholderManager : MonoBehaviour {
    public GameState gameState;

    //FYI: This script is not finished or might not be even used. 
    void Start() {

    }

    void Update() {

        //Stop time when these states are on
        if (gameState == GameState.Paused ||  gameState == GameState.GameOver) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f; 
        }

    }
}
