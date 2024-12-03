// written by Evan Linder

using UnityEngine;

public class ShopMenuTrigger : MonoBehaviour
{
    public GameObject shopMenu; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the colliding object is the player
        {
            Debug.Log("Player entered shop area.");
            shopMenu.SetActive(true); // Show the shop menu when the player enters
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the colliding object is the player
        {
            Debug.Log("Player exited shop area.");
            shopMenu.SetActive(false); // Hide the shop menu when the player exits
        }
    }
}


