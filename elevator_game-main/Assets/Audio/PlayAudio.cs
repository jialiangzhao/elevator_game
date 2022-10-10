using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//audio.PlayOneShot(AudioClip audioClip, Float volumeScale);

public class PlayAudio : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
      
    }

    void Update() {


        if (Input.GetAxis("Horizontal") >= 0.1 || Input.GetAxis("Horizontal") <= -0.1)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();

            }
           

        }
        else
        {
            audioSource.Pause();
        }

    }
}