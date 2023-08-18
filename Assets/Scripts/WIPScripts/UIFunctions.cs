using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunctions : MonoBehaviour
{

    [SerializeField]private GameObject pauseScreen = null;
    [SerializeField]private GameObject winScreen = null;
    [SerializeField]private GameObject failScreen = null;

    public void OpenPauseScreen()
    {
        pauseScreen.SetActive(true);
    }
    public void OpenWinScreen()
    {
        winScreen.SetActive(true);
    }
    public void OpenFailScreen()
    {
        failScreen.SetActive(true);
    }
    public void ClosePauseScreen()
    {
        pauseScreen.SetActive(false);
    }
    public void CloseWinScreen()
    {
        winScreen.SetActive(false);
    }
    public void CloseFailScreen()
    {
        failScreen.SetActive(false);
    }

    public bool uiON
    {
        get { return pauseScreen.activeInHierarchy || winScreen.activeInHierarchy || failScreen.activeInHierarchy;  }
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            ReturnToMainMenu();
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

}
