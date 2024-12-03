// written by Evan Linder


using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip enemyHitSound;
    public AudioClip playerHitSound;
    public AudioClip enemyDieSound;
    public AudioClip goldPickupSound;
    public AudioClip backgroundMusic;
    public AudioClip mineSound;
    public AudioClip punchSound;
    public AudioClip swingSound;
    public AudioClip fishingSound;
    public AudioClip batNoise;
    public AudioClip batDie;
    public AudioClip bossDie;
    public AudioClip bossHit;
    public AudioClip eatFood;
   
    // functions to play sounds.
    public void PlayBackgrondMusic()
    {
        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }
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

    public void PlayMineSound()
    {
        audioSource.clip = mineSound;
        audioSource.Play();
    }
    public void PlayPunchSound()
    {
        audioSource.clip = punchSound;
        audioSource.Play();
    }
    public void PlaySwingSound()
    {
        audioSource.clip = swingSound;
        audioSource.Play();
    }
    public void PlayFishingSound()
    {
        audioSource.clip = fishingSound;
        audioSource.Play();
    }
    public void PlayZone1EnemySound()
    {
        audioSource.clip = batNoise;
        audioSource.Play();
    }
    public void PlayZone1EnemyDie()
    {
        audioSource.clip = batDie;
        audioSource.Play();
    }

    public void PlayBossHitSound()
    {
        audioSource.clip = bossHit;
        audioSource.Play();
    }
    public void PlayBossDieSound()
    {
        audioSource.clip = bossDie;
        audioSource.Play();
    }
    public void PlayEatSound()
    {
        audioSource.clip = eatFood;
        audioSource.Play();
    }
}
