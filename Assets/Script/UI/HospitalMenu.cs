// written by Evan Linder

using UnityEngine;

public class HospitalMenu : MonoBehaviour
{
    public GameObject hospitalMenu;

    // handle shop menu trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Debug.Log("Player entered shop area.");
            hospitalMenu.SetActive(true); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited shop area.");
            hospitalMenu.SetActive(false); 
        }
    }
}