using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    private static bool isPaused;
    private const string START_SCREEN = "StartScreen";

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuPanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resume();
                
            }
            else
            {
                pause();
                
            }
        }
    }

    public void resume()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        ThemeMusicScript.setThemeActivity(true);
        isPaused = false;
    }

    private void pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        ThemeMusicScript.setThemeActivity(false);
        isPaused = true;
    }

    public void quitGame()
    {
        Debug.Log("Quiting the game.");
        Application.Quit();
    }

    public void goToMenu()
    {
        SceneManager.LoadScene(START_SCREEN);
    }

    public static bool checkIfGamePaused()
    {
        return isPaused;
    }
}
