using UnityEngine;

public class SilverCup : MonoBehaviour
{
    QuestController2 questController;


    void Start()
    {
        // Find the QuestController on the player or another object
        questController = FindObjectOfType<QuestController2>();
    }

    // handle collisions with quest object
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            questController.hasItem = true;
        }
    }
}
