using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointControler : MonoBehaviour
{
    public Sprite redFlag;
    public Sprite greenFlag;
    public AudioClip flagReachedSound;

    private bool isReached;
    private SpriteRenderer checkPointSpriteRenderer;
    private PlayerControlScript playerControl;

    // Start is called before the first frame update
    void Start()
    {
        checkPointSpriteRenderer = GetComponent<SpriteRenderer>();
        isReached = false;
        playerControl = FindObjectOfType<PlayerControlScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isReached)
        {
            checkPointSpriteRenderer.sprite = greenFlag;
            AudioSource.PlayClipAtPoint(flagReachedSound, transform.position);
            playerControl.setRespawnPosition(transform.position);
            isReached = true;
        }
    }
}
