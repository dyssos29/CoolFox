using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 5f;
    public Transform jumpCheckPoint;
    public Transform fallDetector;
    public float jumpCheckRadius;
    public LayerMask jumpLayer;
    public Vector3 respawnPoint;
    public AudioClip jumpSound;

    private LevelManager gameLevelManager;
    private float direction = 0f;
    private Rigidbody2D avatarRigidBody;
    private bool isTouchingJumpLayer;
    private bool isActive;
    private Animator avatarAnimator;
    private bool checkedCollision;
    private int frameCounter;
    private const int MAXIMUM_FRAME_COUNTER = 10;

    // Start is called before the first frame update
    void Start()
    {
        avatarRigidBody = GetComponent<Rigidbody2D>();
        avatarAnimator = GetComponent<Animator>();
        respawnPoint = transform.position;
        gameLevelManager = FindObjectOfType<LevelManager>();
        setPlayerActivity(true);
        frameCounter = 0;
        checkedCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingJumpLayer = Physics2D.OverlapCircle(jumpCheckPoint.position, jumpCheckRadius, jumpLayer);
        direction = Input.GetAxis("Horizontal");

        if (isActive)
        {
            if (direction != 0)
            {
                avatarRigidBody.velocity = new Vector2(direction * speed, avatarRigidBody.velocity.y);

                if (direction < 0)
                    transform.localScale = new Vector2(-5.963668f, 6.89132f);
                else
                    transform.localScale = new Vector2(5.963668f, 6.89132f);
            }

            if (Input.GetButtonDown("Jump") && isTouchingJumpLayer)
            {
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
                avatarRigidBody.velocity = new Vector2(avatarRigidBody.velocity.x, jumpSpeed);
            }

            avatarAnimator.SetFloat("Speed", Mathf.Abs(avatarRigidBody.velocity.x));
            avatarAnimator.SetBool("OnGround", isTouchingJumpLayer);
        }

        fallDetector.position = new Vector3(transform.position.x, fallDetector.position.y);
        gameLevelManager.transform.position = new Vector3(transform.position.x, gameLevelManager.transform.position.y);
    }

    public void setPlayerActivity(bool isActive)
    {
        this.isActive = isActive;

        if (!isActive)
        {
            avatarAnimator.SetFloat("Speed", 0);
            avatarAnimator.SetBool("OnGround", true);
        }
    }

    public void setRespawnPosition(Vector3 position)
    {
        respawnPoint = position;
    }

    public void respawn()
    {
        transform.position = respawnPoint;
        checkedCollision = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        Debug.Log("The tag is " + tag + " and the checkedCollision variable is " + checkedCollision);
        
        if (!(tag == "Untagged" || tag == "CheckPoint"))
            checkCollision(tag);
    }

    private void checkCollision(string collisionTag)
    {
        if (!checkedCollision)
        {
            Debug.Log("Inside execution with frame counter: " + frameCounter);
            if (collisionTag == "FallDetector")
                gameLevelManager.respawnFromFall();
            else if (collisionTag == "BombPoint")
                gameLevelManager.respawnFromBomb();
            else if (collisionTag == "FrogEnemy")
                gameLevelManager.respawnFromFrog();

            checkedCollision = true;
        }
    }
}
