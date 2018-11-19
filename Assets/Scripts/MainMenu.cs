using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GameObject introScreen, mainScreen, gameOverScreen;
    public void IntroScreen()
    {
        mainScreen.SetActive(false);
        introScreen.SetActive(true);
    }
    public void PlayGame()
    {
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene("Level");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
}
