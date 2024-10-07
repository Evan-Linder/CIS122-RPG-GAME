using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMenuTrigger : MonoBehaviour
{
    public GameObject foodMenu;

    // handle shop menu trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered shop area.");
            foodMenu.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited shop area.");
            foodMenu.SetActive(false);
        }
    }
}
