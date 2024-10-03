using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class DropdownSpriteDisplay : MonoBehaviour
{
    public TMP_Dropdown dropdown; 
    public Image displayImage; 
    public Sprite[] optionSprites; 
    public TextMeshProUGUI costLabel; 
    public TextMeshProUGUI ownedLabel;

    // Array to hold the costs corresponding to each option
    public float[] optionCosts;

    // Array to hold the owned quantities corresponding to each option
    private int[] ownedQuantities;

    void Start()
    {
        // nitialize owned quantities array
        ownedQuantities = new int[optionSprites.Length];

        // set initial owned quantities to 0
        for (int i = 0; i < ownedQuantities.Length; i++)
        {
            ownedQuantities[i] = 0;
        }

        // add listener for when the dropdown value changes
        dropdown.onValueChanged.AddListener(delegate {
            UpdateDisplay(dropdown.value);
        });

        // initialize the image, cost label, and owned label with the first option's values
        UpdateDisplay(dropdown.value);
    }

    // method to update the sprite, cost label, and owned label based on the dropdown option
    public void UpdateDisplay(int optionIndex)
    {
        // check if the index is valid for sprites
        if (optionIndex >= 0 && optionIndex < optionSprites.Length)
        {
            displayImage.sprite = optionSprites[optionIndex];
        }

        // check if the index is valid for costs
        if (optionIndex >= 0 && optionIndex < optionCosts.Length)
        {
            costLabel.text = "Cost: " + optionCosts[optionIndex].ToString("F2"); // display cost with 2 decimal points
        }

        // update the owned label
        if (optionIndex >= 0 && optionIndex < ownedQuantities.Length)
        {
            ownedLabel.text = "Owned: " + ownedQuantities[optionIndex]; // display owned count
        }
    }
}



