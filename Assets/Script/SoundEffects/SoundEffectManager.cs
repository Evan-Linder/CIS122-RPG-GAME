using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip enemyHitSound;
    public AudioClip playerHitSound;
    public AudioClip enemyDieSound;
    public AudioClip goldPickupSound;

    // functions to play sounds.
    public void PlayEnemyHitSound()
    {
        audioSource.clip = enemyHitSound;
        audioSource.Play();
    }


    public void PlayPlayerHitSound()
    {
        audioSource.clip = playerHitSound;
        audioSource.Play();
    }


    public void PlayEnemyDieSound()
    {
        audioSource.clip = enemyDieSound;
        audioSource.Play();
    }

    public void PlayGoldPickUpSound()
    {
        audioSource.clip = goldPickupSound;
        audioSource.Play();
    }
}
