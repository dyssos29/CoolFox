using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    private const string FIRST_LEVEL = "Level1";

    public void guitGame()
    {
        Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadScene(FIRST_LEVEL);
        LevelManager.resetScore();
        LevelManager.resetLevel();
    }
}
