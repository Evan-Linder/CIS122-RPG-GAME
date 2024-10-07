using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Make sure to include this for TextMeshPro

public class FoodManager : MonoBehaviour
{
    public int foodCount = 0; 
    public playerScript player;
    public TextMeshProUGUI foodCountText;
    public Button purchaseFoodBtn;
    public int foodCost = 5;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<playerScript>();
        UpdateFoodCountDisplay();
        purchaseFoodBtn.onClick.AddListener(BuyFood);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EatFood();
        }
    }

    // Method to handle eating food
    void EatFood()
    {
        // check if player is at max health 
        if (player.playerHealth < 5)
        {
            // Check if player has food
            if (foodCount > 0)
            {
                player.playerHealth += 1;
                foodCount--;
                UpdateFoodCountDisplay();
            }
            else
            {
                Debug.Log("No food in inventory to eat!");
            }
        }
        else
        {
            Debug.Log("Your already at max health");
        }
    }

    void BuyFood()
    {
        if (player.coinCount >= foodCost)
        {
            foodCount++;
            player.coinCount -= foodCost;  // deduct coins for potion refill
            Debug.Log("Food Purchased! Remaining gold: " + player.coinCount);
            player.UpdateCoinDisplay();  // update coin display
            UpdateFoodCountDisplay();
        }
    }

    // Method to update the food count display
    void UpdateFoodCountDisplay()
    {
        foodCountText.text = foodCount.ToString(); 
    }
}

