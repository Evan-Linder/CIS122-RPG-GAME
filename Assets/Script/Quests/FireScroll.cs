using UnityEngine;

public class FireScroll : MonoBehaviour
{
    QuestController questController;


    void Start()
    {
        // Find the QuestController on the player or another object
        questController = FindObjectOfType<QuestController>(); 
    }

    // handle collisions with quest object
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            questController.questObject.SetActive(false);
            questController.hasItem = true;
        }
    }
}


