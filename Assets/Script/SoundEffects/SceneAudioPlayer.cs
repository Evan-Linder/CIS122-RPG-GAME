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

    void OnDisable() 
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}


