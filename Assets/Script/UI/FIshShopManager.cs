// written by Evan Linder

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishShopManager : MonoBehaviour
{
    public Button sellBtn;
    public TextMeshProUGUI messageText;
    public TMP_InputField sellAmountInput;

    private playerScript playerController;
    private FishingController fishingCon;
    private string selectedFish = "";

    // Coin values for each fish
    private int blobFishValue = 10;
    private int nemoFishValue = 10;
    private int deathVaderValue = 25;
    private int zebrillaValue = 20;
    private int philValue = 20;
    private int guppieValue = 15;
    private int goldieValue = 10;
    private int greenCatfishValue = 20;
    public Button goldieBtn;
    public Button deathVaderBtn;
    public Button guppieBtn;
    public Button greenCatfishBtn;
    public Button zebrillaBtn;
    public Button blobBtn;
    public Button philBtn;
    public Button nemoBtn;

    private void Start()
    {
        playerController = FindObjectOfType<playerScript>(); // Access the playerScript properly
        fishingCon = GetComponent<FishingController>();

        // Set button listeners for each mineral
        goldieBtn.onClick.AddListener(() => SelectMineral("Goldie"));
        deathVaderBtn.onClick.AddListener(() => SelectMineral("DeathVader"));
        guppieBtn.onClick.AddListener(() => SelectMineral("Guppie"));
        greenCatfishBtn.onClick.AddListener(() => SelectMineral("GreenCatFish"));
        zebrillaBtn.onClick.AddListener(() => SelectMineral("Zebrilla"));
        blobBtn.onClick.AddListener(() => SelectMineral("BlobFish"));
        philBtn.onClick.AddListener(() => SelectMineral("Phil"));
        nemoBtn.onClick.AddListener(() => SelectMineral("Nemo"));

        // Add listener for the sell button
        sellBtn.onClick.AddListener(SellFish);
    }

    // Method to handle mineral selection
    private void SelectMineral(string fish)
    {
        selectedFish = fish;
        DisplayMessage(fish + " selected");
    }

    // Method to handle selling the selected mineral
    private void SellFish()
    {
        int amountToSell = 0;
        if (!int.TryParse(sellAmountInput.text, out amountToSell) || amountToSell <= 0)
        {
            DisplayMessage("Invalid amount entered");
            return;
        }

        switch (selectedFish)
        {
            case "BlobFish":
                if (amountToSell <= fishingCon.blobCount)
                {
                    fishingCon.blobCount -= amountToSell;
                    playerController.coinCount += amountToSell * blobFishValue;
                    DisplayMessage("Sold " + amountToSell + " Blob fish for " + (amountToSell * blobFishValue) + " coins");
                    playerController.UpdateCoinDisplay();
                    fishingCon.UpdateBlobDisplay();
                }
                else
                {
                    DisplayMessage("Not enough blob fish to sell");
                }
                break;

            case "GreenCatFish":
                if (amountToSell <= fishingCon.greenCatfishCount)  // Corrected to check ironCount
                {
                    fishingCon.greenCatfishCount -= amountToSell;
                    playerController.coinCount += amountToSell * greenCatfishValue;
                    playerController.UpdateCoinDisplay();
                    fishingCon.UpdateGreenCatFishDisplay();
                    DisplayMessage("Sold " + amountToSell + " Green Catfish for " + (amountToSell * greenCatfishValue) + " coins");
                }
                else
                {
                    DisplayMessage("Not enough green catfish to sell");
                }
                break;

            case "Nemo":
                if (amountToSell <= fishingCon.nemoCount)  // Corrected to check crystalCount
                {
                    fishingCon.nemoCount -= amountToSell;
                    playerController.coinCount += amountToSell * nemoFishValue;
                    DisplayMessage("Sold " + amountToSell + " Nemo for " + (amountToSell * nemoFishValue) + " coins");
                    playerController.UpdateCoinDisplay();
                    fishingCon.UpdateNemoDisplay();
                }
                else
                {
                    DisplayMessage("Not enough nemo's to sell");
                }
                break;

            case "Zebrilla":
                if (amountToSell <= fishingCon.zebrillaCount)  // Corrected to check crystalCount
                {
                    fishingCon.zebrillaCount -= amountToSell;
                    playerController.coinCount += amountToSell * zebrillaValue;
                    DisplayMessage("Sold " + amountToSell + " Zebrilla for " + (amountToSell * zebrillaValue) + " coins");
                    playerController.UpdateCoinDisplay();
                    fishingCon.UpdateZebrillaDisplay();
                }
                else
                {
                    DisplayMessage("Not enough zebrilla's to sell");
                }
                break;

            case "Phil":
                if (amountToSell <= fishingCon.philCount)  // Corrected to check crystalCount
                {
                    fishingCon.philCount -= amountToSell;
                    playerController.coinCount += amountToSell * philValue;
                    DisplayMessage("Sold " + amountToSell + " Nemo for " + (amountToSell * philValue) + " coins");
                    playerController.UpdateCoinDisplay();
                    fishingCon.UpdatePhilDisplay();
                }
                else
                {
                    DisplayMessage("Not enough phil's to sell");
                }
                break;

            case "Guppie":
                if (amountToSell <= fishingCon.guppieCount)  // Corrected to check crystalCount
                {
                    fishingCon.guppieCount -= amountToSell;
                    playerController.coinCount += amountToSell * guppieValue;
                    DisplayMessage("Sold " + amountToSell + " Guppies for " + (amountToSell * guppieValue) + " coins");
                    playerController.UpdateCoinDisplay();
                    fishingCon.UpdateGuppieDisplay();
                }
                else
                {
                    DisplayMessage("Not enough guppie's to sell");
                }
                break;

            case "DeathVader":
                if (amountToSell <= fishingCon.deathVaderCount)  // Corrected to check crystalCount
                {
                    fishingCon.deathVaderCount -= amountToSell;
                    playerController.coinCount += amountToSell * deathVaderValue;
                    DisplayMessage("Sold " + amountToSell + " Death Vader for " + (amountToSell * deathVaderValue) + " coins");
                    playerController.UpdateCoinDisplay();
                    fishingCon.UpdateDeathVaderDisplay();
                }
                else
                {
                    DisplayMessage("Not enough death vaders to sell");
                }
                break;

            default:
                DisplayMessage("No fish selected");
                break;

            case "Goldie":
                if (amountToSell <= fishingCon.goldieCount)  // Corrected to check crystalCount
                {
                    fishingCon.goldieCount -= amountToSell;
                    playerController.coinCount += amountToSell * goldieValue;
                    DisplayMessage("Sold " + amountToSell + " goldies for " + (amountToSell * goldieValue) + " coins");
                    playerController.UpdateCoinDisplay();
                    fishingCon.UpdateGoldieDisplay();
                }
                else
                {
                    DisplayMessage("Not enough goldie's to sell");
                }
                break;
        }
    }

    // Method to display messages in the UI
    private void DisplayMessage(string message)
    {

        messageText.text = message;

    }
}