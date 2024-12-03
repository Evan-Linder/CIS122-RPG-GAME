// written by Evan Linder

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionManager : MonoBehaviour
{
    public GameObject fullPotion;
    public GameObject halfPotion;
    public GameObject emptyPotion;

    public Button refillPotionButton;
    public Button refillHealthButton;

    public TextMeshProUGUI messageText;

    public int potionRefillCost = 10;
    public int healthRefillCostPerMissingHeart = 10;

    private enum PotionState { Full, Half, Empty }
    private PotionState currentPotionState = PotionState.Full;

    private playerScript playerController;

    private void Start()
    {
        // Initialize the potion to the full state (fullPotion is active, others inactive)
        SetPotionState(PotionState.Full);

        playerController = GetComponent<playerScript>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController script not found!");
        }

        // Add listeners to buttons for refilling potion and health
        refillPotionButton.onClick.AddListener(RefillPotion);
        refillHealthButton.onClick.AddListener(RefillHealth);
    }

    // Check if the "Q" key is pressed
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UsePotion();
        }
    }

    public void UsePotion()
    {
        if (currentPotionState == PotionState.Empty)
        {
            DisplayMessage("Potion already empty!");
            return;
        }

        // Prevent using potion if health is already at max (5)
        if (playerController.playerHealth >= 5)
        {
            DisplayMessage("Health already full!");
            return; // Cannot use potion if health is full
        }

        // Increment player health via PlayerController script
        playerController.playerHealth += 1;
        Debug.Log("Player health: " + playerController.playerHealth);

        // Update potion state and set active/inactive accordingly
        UpdatePotionState();
    }

    private void UpdatePotionState()
    {
        switch (currentPotionState)
        {
            case PotionState.Full:
                SetPotionState(PotionState.Half);
                break;

            case PotionState.Half:
                SetPotionState(PotionState.Empty);
                break;

            case PotionState.Empty:
                // Potion already empty, no change needed
                break;
        }
    }

    // Function to set the active potion stage
    private void SetPotionState(PotionState state)
    {
        currentPotionState = state;

        // Activate or deactivate potion sprites based on current state
        fullPotion.SetActive(state == PotionState.Full);
        halfPotion.SetActive(state == PotionState.Half);
        emptyPotion.SetActive(state == PotionState.Empty);
    }

    // Method to refill the potion
    public void RefillPotion()
    {
        if (currentPotionState == PotionState.Full)
        {
            DisplayMessage("Potion already full!");
            return;
        }

        if (playerController.coinCount >= potionRefillCost)
        {
            playerController.coinCount -= potionRefillCost;  
            Debug.Log("Potion refilled! Remaining gold: " + playerController.coinCount);
            playerController.UpdateCoinDisplay();  
            SetPotionState(PotionState.Full);
            DisplayMessage("Potion refilled!");
        }
        else
        {
            DisplayMessage("Not enough gold!");
        }
    }

    // Method to refill health
    public void RefillHealth()
    {
        if (playerController.playerHealth >= 5)
        {
            DisplayMessage("Health already full!");
            return;
        }

        int missingHealth = 5 - playerController.playerHealth;
        int totalRefillCost = missingHealth * healthRefillCostPerMissingHeart;

        if (playerController.coinCount >= totalRefillCost)
        {
            playerController.coinCount -= totalRefillCost;  
            Debug.Log("Health refilled to max! Remaining gold: " + playerController.coinCount);
            playerController.UpdateCoinDisplay();  
            playerController.playerHealth = 5;  
            DisplayMessage("Health refilled!");
        }
        else
        {
            DisplayMessage("Not enough gold!");
        }
    }

    // Method to display messages
    private void DisplayMessage(string message)
    {
        messageText.text = message;
    }
}

