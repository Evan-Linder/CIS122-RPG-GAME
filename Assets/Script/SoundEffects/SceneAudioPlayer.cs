// written by Evan Linder

using UnityEngine;

public class SceneAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sceneAudio;

    void OnEnable() 
    {
        PlaySceneAudio();
    }

    // function to play the background music
    void PlaySceneAudio()
    {
        if (audioSource != null && sceneAudio != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            
            audioSource.clip = sceneAudio;
            audioSource.Play();
            Debug.Log("Playing Scene Audio");
        }
    }

    // disable the audio source when I press the mute button.
    void OnDisable() 
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}


