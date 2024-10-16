using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MineralShopManager : MonoBehaviour
{
    public Button sellBtn;
    public Button diamondBtn;
    public Button ironBtn;
    public Button crystalBtn;

    public TextMeshProUGUI messageText;
    public TMP_InputField sellAmountInput;

    private playerScript playerController;
    private string selectedMineral = "";

    // Coin values for each mineral
    private int diamondValue = 20;
    private int ironValue = 15;
    private int crystalValue = 10;

    private void Start()
    {
        playerController = FindObjectOfType<playerScript>(); // Access the playerScript properly

        // Set button listeners for each mineral
        diamondBtn.onClick.AddListener(() => SelectMineral("Diamond"));
        ironBtn.onClick.AddListener(() => SelectMineral("Iron"));
        crystalBtn.onClick.AddListener(() => SelectMineral("Crystal"));

        // Add listener for the sell button
        sellBtn.onClick.AddListener(SellMineral);
    }

    // Method to handle mineral selection
    private void SelectMineral(string mineral)
    {
        selectedMineral = mineral;
        DisplayMessage(mineral + " selected");
    }

    // Method to handle selling the selected mineral
    private void SellMineral()
    {
        int amountToSell = 0;
        if (!int.TryParse(sellAmountInput.text, out amountToSell) || amountToSell <= 0)
        {
            DisplayMessage("Invalid amount entered");
            return;
        }

        switch (selectedMineral)
        {
            case "Diamond":
                if (amountToSell <= playerController.diamondCount)
                {
                    playerController.diamondCount -= amountToSell;
                    playerController.coinCount += amountToSell * diamondValue;
                    DisplayMessage("Sold " + amountToSell + " Diamond(s) for " + (amountToSell * diamondValue) + " coins");
                    playerController.UpdateCoinDisplay();
                    playerController.UpdateDiamondDisplay();
                }
                else
                {
                    DisplayMessage("Not enough diamonds to sell");
                }
                break;

            case "Iron":
                if (amountToSell <= playerController.ironCount)  // Corrected to check ironCount
                {
                    playerController.ironCount -= amountToSell;
                    playerController.coinCount += amountToSell * ironValue;
                    playerController.UpdateCoinDisplay();
                    playerController.UpdateIronOreDisplay();
                    DisplayMessage("Sold " + amountToSell + " Iron(s) for " + (amountToSell * ironValue) + " coins");
                }
                else
                {
                    DisplayMessage("Not enough iron to sell");
                }
                break;

            case "Crystal":
                if (amountToSell <= playerController.crystalCount)  // Corrected to check crystalCount
                {
                    playerController.crystalCount -= amountToSell;
                    playerController.coinCount += amountToSell * crystalValue;
                    DisplayMessage("Sold " + amountToSell + " Crystal(s) for " + (amountToSell * crystalValue) + " coins");
                    playerController.UpdateCoinDisplay();
                    playerController.UpdateCrystalDisplay();
                }
                else
                {
                    DisplayMessage("Not enough crystals to sell");
                }
                break;

            default:
                DisplayMessage("No mineral selected");
                break;
        }
    }

    // Method to display messages in the UI
    private void DisplayMessage(string message)
    {

        messageText.text = message;
        
    }
}


