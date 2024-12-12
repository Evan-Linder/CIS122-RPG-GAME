// written by Evan Linder

using UnityEngine;

public class newAreaSpawn : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject previousScene;
    public GameObject newScene;
    public GameObject mainCamera;
    public Animator sceneChangeAnim;

    // triggered when another collider enters the trigger area
    // teleports the player from 1 box collider to the next.
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sceneChangeAnim.Play("sceneChange");
            mainCamera.transform.position = spawnPoint.transform.position + new Vector3(0, 0, -10);
            other.transform.position = spawnPoint.transform.position;
            previousScene.SetActive(false);
            newScene.SetActive(true);
        }
    }
}

