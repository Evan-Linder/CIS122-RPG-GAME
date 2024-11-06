using UnityEngine;

public class CheckPointMarker : MonoBehaviour
{
    public GameObject checkpointPrefab;  
    public Transform scenesParent;      
    private GameObject currentCheckpointMarker; 
    private GameObject lastActiveScene; 
    private GameObject currentActiveScene;
    public Animator sceneChangeAnim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MarkCheckpoint();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TeleportToCheckpoint();
        }
    }

    // Marks the player's current position and saves the active scene
    void MarkCheckpoint()
    {
        currentCheckpointMarker = Instantiate(checkpointPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);

        foreach (Transform scene in scenesParent)
        {
            if (scene.gameObject.activeSelf)
            {
                currentActiveScene = scene.gameObject;
                break;
            }
        }
        lastActiveScene = currentActiveScene;
    }

    // Teleports the player back to the marked position and switches the scenes
    void TeleportToCheckpoint()
    {
        sceneChangeAnim.Play("sceneChange");
        
        // Teleport the player to the marked position
        transform.position = currentCheckpointMarker.transform.position;
        foreach (Transform scene in scenesParent)
        {
            if (scene.gameObject.activeSelf)
            {
                currentActiveScene = scene.gameObject;
                break;
            }
        }
        currentActiveScene.SetActive(false);
        lastActiveScene.SetActive(true);


    }
}
