using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum GameState { Paused, GameOver, GameWon, InGame }

public class GameManager : MonoBehaviour {
    public GameState gameState;
    [Tooltip("Keep this in the inspector as 0.")][SerializeField]int PuzzlesDone;

    void Start() {
        gameState = GameState.InGame;
    }

    void Update() {
        //Stop the time when game is either paused, won or lost
        switch (gameState) {
            case GameState.GameOver or GameState.Paused or GameState.GameWon:
            Time.timeScale = 0f;
            break;
            default:
            Time.timeScale = 1f;
            break;
        }

        /* TODO
         * if (PuzzlesDone == puzzleAmount && gameState != GameState.GameWon){
         *      WinGame();
         * }
         */
    }

    public void WinGame() {
        gameState = GameState.GameWon;
        Debug.Log("You have won the game!");
        Application.Quit();
    }
    public void GameOver() {
        Debug.Log("Game over!");
        gameState = GameState.GameOver;
        Application.Quit();
    }

    public void PuzzleDone() {
        PuzzlesDone++;
    }
}
