using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    //made by chatGPT
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found. Please attach an AudioSource to this GameObject.");
        }
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
        if (audioSource != null && audioClip != null && !audioSource.isPlaying)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}

