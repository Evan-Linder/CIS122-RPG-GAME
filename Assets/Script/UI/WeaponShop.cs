using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponShop : MonoBehaviour
{
    public Button buyButton;
    public Button sellButton;
    public Button swordButton;
    public Button axeButton;
    public Button bigSwordButton;

    public TextMeshProUGUI weaponStatusText;
    public TextMeshProUGUI coinText;

    public GameObject sword; // Reference to the sword GameObject
    public GameObject axe; // Reference to the axe GameObject
    public GameObject bigSword; // Reference to the big sword GameObject

    public int swordCost = 10;
    public int axeCost = 20;
    public int bigSwordCost = 30;

    public string currentWeapon = "Hands";
    public bool hasWeapon = false;
    private playerScript player;

    void Start()
    {
        // Assuming playerScript is attached to the player GameObject
        player = GameObject.FindWithTag("Player").GetComponent<playerScript>();

        // Initialize shop UI
        player.UpdateWeaponDisplay("Hands");
        player.UpdateCoinDisplay();

        // Assign button listeners
        buyButton.onClick.AddListener(BuyWeapon);
        sellButton.onClick.AddListener(SellWeapon);

        swordButton.onClick.AddListener(() => SelectWeapon("Sword", swordCost));
        axeButton.onClick.AddListener(() => SelectWeapon("Axe", axeCost));
        bigSwordButton.onClick.AddListener(() => SelectWeapon("BigSword", bigSwordCost));
    }

    void BuyWeapon()
    {
        if (hasWeapon)
        {
            weaponStatusText.text = "Sell current weapon first!";
            return;
        }

        int currentWeaponCost = GetCurrentWeaponCost();
        if (player.coinCount >= currentWeaponCost)
        {
            player.coinCount -= currentWeaponCost; // Update the player's coin count
            player.weaponInUse = currentWeapon == "Sword" ? 0 : currentWeapon == "Axe" ? 1 : 2; // set current weapon
            player.UpdateCoinDisplay(); // Update the coin display in the player script
            hasWeapon = true;

            weaponStatusText.text = currentWeapon + " purchased!";
            player.UpdateCoinDisplay(); // Update the UI coin display
        }
        else
        {
            weaponStatusText.text = "Not enough coins!";
        }
    }

    void SellWeapon()
    {
        if (!hasWeapon)
        {
            weaponStatusText.text = "No weapon to sell!";
            return;
        }

        int currentWeaponCost = GetCurrentWeaponCost();
        player.coinCount += currentWeaponCost; // Update the player's coin count
        player.weaponInUse = -1; // Set to hands
        player.UpdateWeaponDisplay("Hands");
        player.UpdateCoinDisplay(); // Update the coin display in the player script

        // Deactivate all weapons
        sword.SetActive(false);
        axe.SetActive(false);
        bigSword.SetActive(false);

        hasWeapon = false;

        weaponStatusText.text = currentWeapon + " sold!";
        player.UpdateCoinDisplay(); // Update the UI coin display
    }

    void SelectWeapon(string weapon, int cost)
    {
        if (hasWeapon)
        {
            weaponStatusText.text = "Sell your current weapon first!";
        }
        else
        {
            currentWeapon = weapon;
            weaponStatusText.text = "Selected " + weapon + " for " + cost + " coins.";
        }
    }

    int GetCurrentWeaponCost()
    {
        if (currentWeapon == "Sword") return swordCost;
        if (currentWeapon == "Axe") return axeCost;
        if (currentWeapon == "BigSword") return bigSwordCost;
        return 0;
    }
}


