using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

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

    public TextMeshProUGUI guppieText;
    public TextMeshProUGUI nemoText;
    public TextMeshProUGUI zebrillaText;
    public TextMeshProUGUI greenCatfishText;
    public TextMeshProUGUI deathVaderText;
    public TextMeshProUGUI goldieText;
    public TextMeshProUGUI blobText;
    public TextMeshProUGUI philText;

    public int guppieCount;
    public int nemoCount;
    public int zebrillaCount;
    public int greenCatfishCount;
    public int deathVaderCount;
    public int goldieCount;
    public int blobCount;
    public int philCount;



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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FishingZone1"))
        {
            inFishingZone = true;
            currentZone = "FishingZone1";
        }
        else if (collision.CompareTag("FishingZone2"))
        {
            inFishingZone = true;
            currentZone = "FishingZone2";
        }
        else if (collision.CompareTag("FishingZone3"))
        {
            inFishingZone = true;
            currentZone = "FishingZone3";
        }
        if (collision.gameObject.CompareTag("Blob1"))
        {
            collision.gameObject.SetActive(false);
            blobCount++;
            UpdateBlobDisplay();
        }
        if (collision.gameObject.CompareTag("DeathVader"))
        {
            collision.gameObject.SetActive(false);
            deathVaderCount++;
            UpdateDeathVaderDisplay();
        }
        if (collision.gameObject.CompareTag("Goldie"))
        {
            collision.gameObject.SetActive(false);
            goldieCount++;
            UpdateGoldieDisplay();
        }
        if (collision.gameObject.CompareTag("Guppie"))
        {
            collision.gameObject.SetActive(false);
            guppieCount++; 
            UpdateGuppieDisplay();
        }
        if (collision.gameObject.CompareTag("Nemo"))
        {
            collision.gameObject.SetActive(false);
            nemoCount++;
            UpdateNemoDisplay();
        }
        if (collision.gameObject.CompareTag("Phil"))
        {
            collision.gameObject.SetActive(false);
            philCount++;
            UpdatePhilDisplay();
        }
        if (collision.gameObject.CompareTag("Zebrilla"))
        {
            collision.gameObject.SetActive(false);
            zebrillaCount++;
            UpdateZebrillaDisplay();
        }
        if (collision.gameObject.CompareTag("GreenCatfish"))
        {
            collision.gameObject.SetActive(false);
            greenCatfishCount++;
            UpdateGreenCatFishDisplay();
        }
    }

    public void UpdateNemoDisplay()
    {
        nemoText.text = "" + nemoCount;
    }
    public void UpdateGuppieDisplay()
    {
        guppieText.text = "" + guppieCount;
    }
    public void UpdateZebrillaDisplay()
    {
        zebrillaText.text = "" + zebrillaCount;
    }
    public void UpdateCrystalDisplay()
    {
        greenCatfishText.text = "" + greenCatfishCount;
    }
    public void UpdateBlobDisplay()
    {
        blobText.text = "" + blobCount;
    }
    public void UpdatePhilDisplay()
    {
        philText.text = "" + philCount;
    }
    public void UpdateDeathVaderDisplay()
    {
        deathVaderText.text = "" + deathVaderCount;
    }
    public void UpdateGoldieDisplay()
    {
        goldieText.text = "" + goldieCount;
    }
    public void UpdateGreenCatFishDisplay()
    {
        greenCatfishText.text = "" + greenCatfishCount;
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

