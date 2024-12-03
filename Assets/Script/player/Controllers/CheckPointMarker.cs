// written by Evan linder

using UnityEngine;
using TMPro;

public class CheckPointMarker : MonoBehaviour
{
    public GameObject checkpointPrefab;
    public Transform scenesParent;
    private GameObject currentCheckpointMarker;
    private GameObject lastActiveScene;
    private GameObject currentActiveScene;
    public Animator sceneChangeAnim;

    public TextMeshProUGUI locationMarkedText;
    private float textDisplayTime = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
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
        if (currentCheckpointMarker != null)
        {
            Destroy(currentCheckpointMarker);
        }

        currentCheckpointMarker = Instantiate(checkpointPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);

        locationMarkedText.text = "Location Marked";
        locationMarkedText.gameObject.SetActive(true);

        Invoke("HideLocationMarkedPrompt", textDisplayTime);

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
        transform.position = currentCheckpointMarker.transform.position;

        // Switch the scenes
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

    // Hides the "Location Marked" prompt
    void HideLocationMarkedPrompt()
    {
        locationMarkedText.gameObject.SetActive(false);
    }
}


