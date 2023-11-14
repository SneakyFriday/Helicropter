using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool IsGameRunning = false;
    public static bool IsMainMenu = false;
    public int Score = 0;
    public GameObject startScreen;
    public GameObject highscoreScreen;
    public GameObject menuScreen;
    public GameObject gameOverText;
    public GameObject StartHintText;
    public int highscore = 0;
    public GameObject player;
    public UnityEvent RestartGameEvent = new UnityEvent();
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void StartGame()
    {
        player.SetActive(true);
        Score = 0;
        StartHintText.SetActive(true);
        startScreen.SetActive(false);
        highscoreScreen.SetActive(false);
        menuScreen.SetActive(false);
        gameOverText.SetActive(false);
    } 
    
    public void ShowMainMenu()
    {
        RestartGameEvent.Invoke();
        IsGameRunning = false;
        startScreen.SetActive(true);
        highscoreScreen.SetActive(false);
        menuScreen.SetActive(false);
        gameOverText.SetActive(false);
    }
    
    public void EndGame()
    {
        IsGameRunning = false;
        highscoreScreen.SetActive(true);
        startScreen.SetActive(false);
        menuScreen.SetActive(false);
        gameOverText.SetActive(false);
    }
    
    public void RestartGame()
    {
        RestartGameEvent.Invoke();
        highscoreScreen.SetActive(false);
        menuScreen.SetActive(false);
        startScreen.SetActive(false);
        gameOverText.SetActive(false);
        player.SetActive(true);
        StartHintText.SetActive(true);
    }
    
    public void ShowHighscore()
    {
        highscoreScreen.SetActive(true);
        startScreen.SetActive(false);
        menuScreen.SetActive(false);
        gameOverText.SetActive(false);
    }
    
    public void ShowMenu()
    {
        IsGameRunning = false;
        startScreen.SetActive(false);
        highscoreScreen.SetActive(false);
        menuScreen.SetActive(true);
        gameOverText.SetActive(false);
    }

    public void ShowMenuFromMainMenu()
    {
        IsGameRunning = false;
        IsMainMenu = true;
        startScreen.SetActive(false);
        highscoreScreen.SetActive(false);
        menuScreen.SetActive(true);
        gameOverText.SetActive(false);
    }
    
    public void HideMenu()
    {
        if(IsMainMenu)
        {
            IsGameRunning = false;
            startScreen.SetActive(true);
            IsMainMenu = false;
        }
        else
        {
            IsGameRunning = true;
            startScreen.SetActive(false);
        }
        highscoreScreen.SetActive(false);
        menuScreen.SetActive(false);
        gameOverText.SetActive(false);
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
        highscore = Score;
    }

    public void StartGameAfterHint()
    {
        StartHintText.SetActive(false);
        IsGameRunning = true;
    }
    
}
