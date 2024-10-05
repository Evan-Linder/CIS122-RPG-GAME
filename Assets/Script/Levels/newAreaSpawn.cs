using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newAreaSpawn : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject previousScene;
    public GameObject newScene;
    public GameObject mainCamera;

    //public Animator sceneChangeAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //sceneChangeAnim.Play("switchScene");
            mainCamera.transform.position = spawnPoint.transform.position + new Vector3(0, 0, -10);
            other.transform.position = spawnPoint.transform.position;
            previousScene.SetActive(false);
            newScene.SetActive(true);
        }
    }
}
