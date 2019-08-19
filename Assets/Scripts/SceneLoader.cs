using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void loadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void loadStartScene()
    {
        //destroys any existing Gamestatus object when start a new game
        FindObjectOfType<GameStatus>().ResetGame();
        SceneManager.LoadScene(0);
    }  

    public void quitGame()
    {
        Application.Quit();
    }
}
