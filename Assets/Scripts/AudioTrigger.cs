using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//made by chatGPT
public class AudioTrigger : MonoBehaviour
{

    [SerializeField] private AudioSource newAudioSource;   
    [SerializeField] private AudioClip newAudioClip;
    [SerializeField] public float audioVolume = 0.5f;

    private void Start()
    {
        if (newAudioSource == null)
        {
            newAudioSource = GetComponent<AudioSource>();
        }
        

        if (newAudioSource == null)
        {
            Debug.LogError("AudioSource component not found. Please attach an AudioSource to this GameObject.");
        }
        newAudioSource.clip = newAudioClip;

        newAudioSource.volume = 0.0f;
        newAudioSource.Play();
        newAudioSource.Stop();
        newAudioSource.volume = audioVolume;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the collider has the "Player" tag
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (newAudioSource != null && newAudioClip != null)
        {
            
            newAudioSource.clip = newAudioClip;
            newAudioSource.Play();
            
        }
        else
        {
            Debug.LogError("New AudioSource or AudioClip is not assigned.");
        }
    }
}

