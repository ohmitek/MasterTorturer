using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { Paused, GameOver, GameWon, InGame }

public class GameManager : MonoBehaviour {
    public GameState gameState;
    [Tooltip("Keep this in the inspector as 0.")][SerializeField]int PuzzlesDone;

    [SerializeField] GameObject mouseSlider;
    public Slider slider;
    [SerializeField] float maxSensitivity;


    void Start() {
        gameState = GameState.InGame;
        slider = mouseSlider.GetComponentInChildren<Slider>();
        slider.maxValue = maxSensitivity;
        slider.value = maxSensitivity;
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

        MouseSlider();

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

    //Normally i would make a separate script for this, but it is what it is
    void MouseSlider() {
        //Slider for mouse sensitivity
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (mouseSlider.activeSelf == true) {
                Cursor.lockState = CursorLockMode.Locked;
                mouseSlider.SetActive(false);
            }
            else if (mouseSlider.activeSelf == false) {
                Cursor.lockState = CursorLockMode.None;
                mouseSlider.SetActive(true);
                slider.maxValue = maxSensitivity;
            }
        }
    }
}
