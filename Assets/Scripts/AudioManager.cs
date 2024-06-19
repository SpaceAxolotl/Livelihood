using UnityEngine;
using System.Collections;

public class BackgroundMusicSwitcher : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource1; // First AudioSource
    [SerializeField] private AudioSource audioSource2; // Second AudioSource
    [SerializeField] private AudioClip backgroundTrack1; // First background track
    [SerializeField] private AudioClip backgroundTrack2; // Second background track
    [SerializeField] private float fadeDuration = 1.0f; // Duration of the fade in/out

    private bool isTrack1Playing = true; // To track which track is currently playing
    private AudioSource currentAudioSource;
    private AudioSource nextAudioSource;

    private void Start()
    {
        if (audioSource1 == null || audioSource2 == null)
        {
            Debug.LogError("Both AudioSources must be assigned.");
            return;
        }

        // Preload audio clips to ensure they're ready to play
        audioSource1.clip = backgroundTrack1;
        audioSource2.clip = backgroundTrack2;

        // Warm-up the audio sources by playing and stopping them immediately
        audioSource1.volume = 0.0f;
        audioSource1.Play();
        audioSource1.Stop();

        audioSource2.volume = 0.0f;
        audioSource2.Play();
        audioSource2.Stop();

        // Start by playing the first track
        currentAudioSource = audioSource1;
        nextAudioSource = audioSource2;

        currentAudioSource.volume = 0.4f; // Set initial volume
        currentAudioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeOutAndSwitchTrack());
        }
    }

    private IEnumerator FadeOutAndSwitchTrack()
    {
        yield return StartCoroutine(FadeOut(currentAudioSource, fadeDuration));

        // Swap the current and next audio sources
        AudioSource temp = currentAudioSource;
        currentAudioSource = nextAudioSource;
        nextAudioSource = temp;

        // Swap the tracks
        isTrack1Playing = !isTrack1Playing;

        yield return StartCoroutine(FadeIn(currentAudioSource, fadeDuration));
    }

    private IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0, time / duration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }

    private IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float targetVolume = isTrack1Playing ? 0.4f : 0.6f;
        audioSource.volume = 0;
        audioSource.Play();
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, targetVolume, time / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}
