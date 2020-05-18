using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float respawnDelay;
    public float completeLevelDelay;
    public float loseGameDelay;
    public Text scoreGUI;
    public Text lifeCounterGUI;
    public Text completeLevelGUI;
    public Text loseGameGUI;
    public Text deathMessageGUI;
    public Text levelGUI;
    public AudioClip deathSound;
    public AudioClip loseGameSound;
    public const int NUMBER_OF_LIVES = 3;

    private PlayerControlScript playerControler;
    private Animator playerAnimator;
    private int deathCounter;
    private static int globalScore = 0;
    private static int startScoreSecondLevel;
    private static int level = 1;
    private const string START_SCREEN = "StartScreen";

    // Start is called before the first frame update
    void Start()
    {
        playerControler = FindObjectOfType<PlayerControlScript>();
        playerAnimator = playerControler.GetComponent<Animator>();
        scoreGUI.text = "Score: " + globalScore;
        lifeCounterGUI.text = "Number of lives: " + NUMBER_OF_LIVES;
        levelGUI.text = "Level: " + level;
        completeLevelGUI.text = "";
        loseGameGUI.text = "";
        deathMessageGUI.text = "";
        deathCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkIfLostAllLives())
            loseGame();
    }

    public void respawnFromFall()
    {
        deathCounter++;
        StartCoroutine("respawnFromFallCoroutine");
        lifeCounterGUI.text = "Number of lives: " + (NUMBER_OF_LIVES - deathCounter);
    }

    private IEnumerator respawnFromFallCoroutine()
    {
        playerControler.gameObject.SetActive(false);
        if (deathCounter != 3)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            yield return new WaitForSeconds(1f);
            deathMessageGUI.text = "Avoid falling in gaps!";
            playerControler.respawn();
        }
        yield return new WaitForSeconds(respawnDelay);
        deathMessageGUI.text = "";
        playerControler.gameObject.SetActive(true);
    }

    public void respawnFromBomb()
    {
        deathCounter++;
        StartCoroutine("respawnFromBombCoroutine");
        lifeCounterGUI.text = "Number of lives: " + (NUMBER_OF_LIVES - deathCounter);
    }

    private IEnumerator respawnFromBombCoroutine()
    {
        playerControler.setPlayerActivity(false);
        playerAnimator.SetBool("IsHurt", true);
        if (deathCounter != 3)
        {
            yield return new WaitForSeconds(1f);
            deathMessageGUI.text = "Avoid stepping into bombs!";
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            
            yield return new WaitForSeconds(respawnDelay);
            playerAnimator.SetBool("IsHurt", false);
            playerControler.respawn();
        }
        
        deathMessageGUI.text = "";
        playerControler.setPlayerActivity(true);
    }

    public void respawnFromFrog()
    {
        deathCounter++;
        StartCoroutine("respawnFromFrogCoroutine");
        lifeCounterGUI.text = "Number of lives: " + (NUMBER_OF_LIVES - deathCounter);
    }

    private IEnumerator respawnFromFrogCoroutine()
    {
        playerControler.setPlayerActivity(false);
        playerAnimator.SetBool("IsHurt", true);
        if (deathCounter != 3)
        {
            yield return new WaitForSeconds(1f);
            deathMessageGUI.text = "Avoid stepping into frogs!";
            AudioSource.PlayClipAtPoint(deathSound, transform.position);

            yield return new WaitForSeconds(respawnDelay);
            playerAnimator.SetBool("IsHurt", false);
            playerControler.respawn();
        }
        
        deathMessageGUI.text = "";
        playerControler.setPlayerActivity(true);
    }

    public void goToNextLevel()
    {
        StartCoroutine("goToNextLevelCoroutine");
    }

    private IEnumerator goToNextLevelCoroutine()
    {
        playerControler.setPlayerActivity(false);
        
        if (level == 3)
        {
            completeLevelGUI.text = "Completed the game with score " + globalScore;
            yield return new WaitForSeconds(completeLevelDelay);
            SceneManager.LoadScene(START_SCREEN);
        }
        else
        {
            completeLevelGUI.text = "Completed level with score " + globalScore;
            yield return new WaitForSeconds(completeLevelDelay);

            if (level == 1)
                startScoreSecondLevel = globalScore;

            level++;
            SceneManager.LoadScene("Level" + level);
        }
    }

    private void loseGame()
    {
        ThemeMusicScript.setThemeActivity(false);
        StartCoroutine("loseGameCoroutine");
    }

    private IEnumerator loseGameCoroutine()
    {
        playerControler.setPlayerActivity(false);
        AudioSource.PlayClipAtPoint(loseGameSound, transform.position);
        completeLevelGUI.text = "Game over!";
        yield return new WaitForSeconds(loseGameDelay);
        
        if (level == 1)
            SceneManager.LoadScene(START_SCREEN);
        else
        {
            if (level == 2)
                globalScore = 0;
            else
                globalScore = startScoreSecondLevel;

            SceneManager.LoadScene("Level" + (--level));
        }
    }

    public static void resetScore()
    {
        globalScore = 0;
    }

    public void updateScore(int scoreValue)
    {
        globalScore += scoreValue;
        scoreGUI.text = "Score: " + globalScore;
    }

    public static void resetLevel()
    {
        level = 1;
    }

    private bool checkIfLostAllLives()
    {
        if (deathCounter == NUMBER_OF_LIVES)
        {
            deathCounter = 0;
            return true;
        }
        else
            return false;
    }
}
