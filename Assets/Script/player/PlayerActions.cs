using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerActions : MonoBehaviour
{
    public bool isFishing = false;
    private Animator playerAnim;
    private bool inFishingZone = false;
    public TextMeshProUGUI fishingZoneMessage;

    void Start()
    {
        playerAnim = GetComponentInChildren<Animator>();
        fishingZoneMessage.gameObject.SetActive(false);
    }

    void Update()
    {
        // start fishing animation when F key is pressed, only if player is in the fishing zone
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
        isFishing = false;
    }

    public bool IsFishing()
    {
        return isFishing;
    }

    // display a message when the player tries to fish outside the fishing zone
    private void DisplayFishingZoneMessage()
    {
        fishingZoneMessage.gameObject.SetActive(true);
        fishingZoneMessage.text = "You must be on a fishing dock to fish!";
        StartCoroutine(HideFishingZoneMessage());
    }

    private IEnumerator HideFishingZoneMessage()
    {
        yield return new WaitForSeconds(2.0f); 
        fishingZoneMessage.gameObject.SetActive(false);
    }

    // trigger detection to check if player enters or exits the fishing zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FishingZone"))
        {
            inFishingZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FishingZone"))
        {
            inFishingZone = false;
        }
    }
}
