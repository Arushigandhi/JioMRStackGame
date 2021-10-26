using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigateScene : MonoBehaviour
{

    public void SampleScene()
    {
        SceneManager.LoadScene("SampleScene");
        
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");

    }
    public void startgame()
    {
        SceneManager.LoadScene("startgame");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit button pressed");
    }
}
