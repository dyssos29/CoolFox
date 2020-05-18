using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogEnemyScript : MonoBehaviour
{
    public Transform target;
    public AudioClip jumpSound;

    private Animator frogEnemyAnimator;
    private string direction;
    private AudioSource frogSound;
    private bool frogSoundOn;

    // Start is called before the first frame update
    void Start()
    {
        frogEnemyAnimator = GetComponent<Animator>();
        frogSound = GetComponent<AudioSource>();
        direction = "left";
        frogSoundOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        lookAtTarget();
        playFrogSound();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            frogEnemyAnimator.SetBool("PlayerClose", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        frogEnemyAnimator.SetBool("PlayerClose", false);
    }

    private void playFrogSound()
    {
        Debug.Log("Before ifs the frogSoundOn is : " + frogSoundOn);
        if (PauseMenuScript.checkIfGamePaused() && frogSoundOn)
        {
            frogSound.Pause();
            frogSoundOn = false;
            Debug.Log("To pause: " + frogSoundOn);
        }
        else if (!PauseMenuScript.checkIfGamePaused() && !frogSoundOn)
        {
            frogSound.Play();
            frogSoundOn = true;
            Debug.Log("To play");
        }
    }

    private void lookAtTarget()
    {
        if ((target.position.x - transform.position.x) > 0 && direction == "left")
            flipFrog("right");
        else if ((target.position.x - transform.position.x) < 0 && direction == "right")
            flipFrog("left");
    }

    private void flipFrog(string aDirection)
    {
        if (aDirection == "right")
        {
            transform.localScale = new Vector2(System.Math.Abs(transform.localScale.x) * (-1), transform.localScale.y);
            direction = "right";
        }    
        else
        {
            transform.localScale = new Vector2(System.Math.Abs(transform.localScale.x), transform.localScale.y);
            direction = "left";
        }
    }
}
