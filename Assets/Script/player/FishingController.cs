using System.Collections;
using UnityEngine;
using TMPro;

public class FishingController : MonoBehaviour
{
    public bool isFishing = false;
    private Animator playerAnim;
    private bool inFishingZone = false;
    private string currentZone = "";
    public TextMeshProUGUI fishingZoneMessage;

    // Fish arrays for each fishing zone
    public GameObject[] zone1Fish;
    public GameObject[] zone2Fish;
    public GameObject[] zone3Fish;

    playerScript playerCon;

    void Start()
    {
        playerAnim = GetComponentInChildren<Animator>();
        fishingZoneMessage.gameObject.SetActive(false);
        playerCon = GetComponent<playerScript>();
    }

    void Update()
    {
        // Start fishing animation when F key is pressed, only if player is in the fishing zone
        if (Input.GetKeyDown(KeyCode.F) && !isFishing)
        {
            if (inFishingZone)
            {
                StartFishing();
            }
            else
            {
                DisplayFishingZoneMessage();
            }
        }
    }

    private void StartFishing()
    {
        isFishing = true;
        playerAnim.Play("playerCast");
        StartCoroutine(EndFishingAnimation());
    }

    private IEnumerator EndFishingAnimation()
    {
        yield return new WaitForSeconds(12.15f);

        // Spawn a random fish depending on the current zone
        SpawnFish();

        isFishing = false;
    }

    // Display a message when the player tries to fish outside the fishing zone
    private void DisplayFishingZoneMessage()
    {
        fishingZoneMessage.gameObject.SetActive(true);
        fishingZoneMessage.text = "You must be in a fishing zone to fish!";
        StartCoroutine(HideFishingZoneMessage());
    }

    private IEnumerator HideFishingZoneMessage()
    {
        yield return new WaitForSeconds(2.0f);
        fishingZoneMessage.gameObject.SetActive(false);
    }

    // Trigger detection to check if player enters or exits the fishing zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FishingZone1"))
        {
            inFishingZone = true;
            currentZone = "FishingZone1";
        }
        else if (other.CompareTag("FishingZone2"))
        {
            inFishingZone = true;
            currentZone = "FishingZone2";
        }
        else if (other.CompareTag("FishingZone3"))
        {
            inFishingZone = true;
            currentZone = "FishingZone3";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FishingZone1") || other.CompareTag("FishingZone2") || other.CompareTag("FishingZone3"))
        {
            inFishingZone = false;
            currentZone = "";
        }
    }

    private void SpawnFish()
    {
        GameObject[] fishArray = null;

        // Select the fish array based on the current zone
        switch (currentZone)
        {
            case "FishingZone1":
                fishArray = zone1Fish;
                break;
            case "FishingZone2":
                fishArray = zone2Fish;
                break;
            case "FishingZone3":
                fishArray = zone3Fish;
                break;
        }

        // Spawn a random fish from the selected array if it's not null
        if (fishArray != null && fishArray.Length > 0)
        {
            int randomIndex = Random.Range(0, fishArray.Length);
            GameObject fishToSpawn = fishArray[randomIndex];

            // Determine the direction the player is facing
            Vector3 spawnPosition;

            // Assuming the player's facing direction is stored in the player's transform scale or direction
            if (playerCon.direction == 1) // Player is facing right
            {
                spawnPosition = transform.position - new Vector3(2f, 0f, 0f); // 2 meters behind
            }
            else // Player is facing left
            {
                spawnPosition = transform.position + new Vector3(2f, 0f, 0f); // 2 meters behind
            }

            Instantiate(fishToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}

