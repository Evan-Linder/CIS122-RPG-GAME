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
            // move the player to the specified start position
            collision.transform.position = playerStartPosition;

            // Switch to the appropriate camera based on the target level
            if (targetLevel == "Home")
            {
                cameraSwitcher.SwitchCamera(0); 
            }
            else if (targetLevel == "DownStairsHouse")
            {
                cameraSwitcher.SwitchCamera(1); 
            }

            else if (targetLevel == "UpStairsHouse")
            {
                cameraSwitcher.SwitchCamera(2);
            }

            else if (targetLevel == "Farm")
            {
                cameraSwitcher.SwitchCamera(3);
            }
            else if (targetLevel == "Store")
            {
                cameraSwitcher.SwitchCamera(4);
            }
            else if (targetLevel == "InsideStore")
            {
                cameraSwitcher.SwitchCamera(5);
            }
            else if (targetLevel == "Battlefield")
            {
                cameraSwitcher.SwitchCamera(6);
            }

        }
    }
}

