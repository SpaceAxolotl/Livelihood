using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraShake;

public class CamShakeFromAudio : MonoBehaviour
{
    //made using help from chat GPT as well as the excellent screen shaker from here: https://github.com/gasgiant/Camera-Shake?tab=readme-ov-file , from this video: https://www.youtube.com/watch?v=fn3hIPLbSn8
    public AudioSource AudioSource;
    public AudioClip creaturestomps;
    public AudioClip creaturespawns;
    [SerializeField] float timeBetweenStomps;
    public float timer;

    private void FixedUpdate()
    {
        CamShaker();
    }

    private void CamShaker()
    {
        //let's check if the audio file has the name we want.
        if (AudioSource != null && AudioSource.isPlaying) //first: let's see if the audiosource is playing
        {
            //if so, what audio file is playing?
            if (AudioSource.clip == creaturestomps)
            {
                
                //add some time to the timer, 
                timer = timer + Time.deltaTime;
                if (timer >= timeBetweenStomps)
                {
                    CameraShaker.Presets.ShortShake2D(); //let's shake the camera!
                }
            }
        }
    }
}
