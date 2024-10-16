using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;  
    public AudioClip sceneAudio;       

    void Start()
    {
        PlaySceneAudio();
    }
    void PlaySceneAudio()
    {
        if (audioSource != null && sceneAudio != null)
        {
            audioSource.clip = sceneAudio;
            audioSource.Play();
        }
    }
}
