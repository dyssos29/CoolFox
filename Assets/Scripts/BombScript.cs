using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public AudioClip explosionSound;

    private Animator bombAnimator;
    private bool avatarContacted;

    // Start is called before the first frame update
    void Start()
    {
        bombAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bombAnimator.SetBool("AvatarContacted", avatarContacted);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            avatarContacted = true;
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            Destroy(gameObject, 0.5f);
        }
    }
}
