using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeMusicScript : MonoBehaviour
{
    public static AudioSource themeMusic;

    // Start is called before the first frame update
    void Start()
    {
        themeMusic = GetComponent<AudioSource>();
    }

    public static void setThemeActivity(bool activity)
    {
        if (activity)
            themeMusic.Play();
        else
            themeMusic.Pause();
    }

    //public static void change
}
