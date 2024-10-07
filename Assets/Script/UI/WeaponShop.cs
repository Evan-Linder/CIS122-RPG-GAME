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
    public Button lanceButton;
    public Button katanaButton;

    public TextMeshProUGUI weaponStatusText;
    public TextMeshProUGUI coinText;

    public GameObject sword; 
    public GameObject axe; 
    public GameObject bigSword;
    public GameObject katana;
    public GameObject lance;

    public int swordCost = 10;
    public int axeCost = 20;
    public int bigSwordCost = 30;
    public int katanaCost = 40;
    public int lanceCost = 50;

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
        lanceButton.onClick.AddListener(() => SelectWeapon("Lance", lanceCost));
        katanaButton.onClick.AddListener(() => SelectWeapon("Katana", katanaCost));
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
            player.weaponInUse = currentWeapon == "Sword" ? 0 : currentWeapon == "Axe" ? 1 : currentWeapon == "BigSword" ? 2 : 
                currentWeapon == "Lance" ? 3 : 4; // set current weapon

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
        katana.SetActive(false);
        lance.SetActive(false);

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
        if (currentWeapon == "Katana") return katanaCost;
        if (currentWeapon == "Lance") return lanceCost;
        return 0;
    }
}


