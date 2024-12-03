// written by Evan Linder

using System.Collections;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public GameObject fireBallPrefab;  
    public Animator playerAnim;         
    public Transform spawnPointUp;     
    public Transform spawnPointRight;  
    public Transform spawnPointDown;    
    public Transform spawnPointLeft;     
    public bool abilityActive = false; 
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb2d;
    HorseRiding HorseRiding;

    playerScript player;

    void Start()
    {
        player = GetComponent<playerScript>();
        rb2d = GetComponent<Rigidbody2D>();
        HorseRiding = GetComponent<HorseRiding>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !abilityActive && HorseRiding.isRiding != true)
        {
            rb2d.velocity = Vector2.zero; // stop movement
            if (player.direction == 0)
            {
                abilityActive = true; 
                playerAnim.Play("playerAbilityD");
                StartCoroutine(EndAbilityAnimation());
            }

            else if (player.direction == 1)
            {
                abilityActive = true; 
                playerAnim.Play("playerAbilityLR"); 
                StartCoroutine(EndAbilityAnimation());
            }
            else if (player.direction == 2)
            {
                abilityActive = true; 
                playerAnim.Play("playerAbilityLR");
                StartCoroutine(EndAbilityAnimation());
            }
            else if (player.direction == 3)
            {
                abilityActive = true;
                playerAnim.Play("playerAbilityUp");
                StartCoroutine(EndAbilityAnimation());
            }
        }
    }

    private IEnumerator EndAbilityAnimation()
    {
        yield return new WaitForSeconds(1f); 
        ShootFireBall();
        abilityActive = false;
    }

    void ShootFireBall()
    {
        Transform spawnPoint = null; 

        // Determine which spawn point to use based on the direction
        if (player.direction == 0) 
        {
            spawnPoint = spawnPointDown;
        }
        else if (player.direction == 1) 
        {
            spawnPoint = spawnPointRight;
        }
        else if (player.direction == 2) 
        {
            spawnPoint = spawnPointLeft;
        }
        else if (player.direction == 3) 
        {
            spawnPoint = spawnPointUp;
        }
        else
        {
            Debug.LogWarning("Spawn point is not set. Fireball not instantiated.");
            return; // Exit if no valid direction
        }

       
        GameObject fireBall = Instantiate(fireBallPrefab, spawnPoint.position, spawnPoint.rotation);
        fireBall.GetComponent<FireBall>().direction = player.direction;
    }
}



