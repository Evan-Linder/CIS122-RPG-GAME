// written by Evan Linder


using UnityEngine;

public class MathCollider : MonoBehaviour
{
    public GameObject shopMenu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Debug.Log("Player entered math area.");
            shopMenu.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Debug.Log("Player exited math area.");
        shopMenu.SetActive(false);
    
        }
    }

