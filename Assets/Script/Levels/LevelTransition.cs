using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public string targetLevel; 
    public Vector3 playerStartPosition; 
    public CameraSwitcher cameraSwitcher; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Move the player to the specified start position
            collision.transform.position = playerStartPosition;

            // Switch to the appropriate camera based on the target level
            if (targetLevel == "Farm")
            {
                cameraSwitcher.SwitchCamera(0); 
            }
            else if (targetLevel == "Store")
            {
                cameraSwitcher.SwitchCamera(1); 
            }

            else if (targetLevel == "Battlefield")
            {
                cameraSwitcher.SwitchCamera(2);
            }

            else if (targetLevel == "InsideStore")
            {
                cameraSwitcher.SwitchCamera(3);
            }

        }
    }
}

