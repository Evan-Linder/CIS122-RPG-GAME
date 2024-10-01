using UnityEngine;
using UnityEngine.UI; // Keep this for Image component
using TMPro; // Import TextMesh Pro namespace

public class DropdownSpriteDisplay : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Reference to the TMP_Dropdown component
    public Image displayImage; // Reference to the UI Image component
    public Sprite[] optionSprites; // Array to hold sprites for each option
    public TextMeshProUGUI costLabel; // Reference to the TextMesh Pro text component for displaying cost
    public TextMeshProUGUI ownedLabel; // Reference to the TextMesh Pro text component for displaying owned count

    // Array to hold the costs corresponding to each option
    public float[] optionCosts;

    // Array to hold the owned quantities corresponding to each option
    private int[] ownedQuantities;

    void Start()
    {
        // Initialize owned quantities array
        ownedQuantities = new int[optionSprites.Length]; // Assuming each option has a corresponding sprite

        // Set initial owned quantities to 0
        for (int i = 0; i < ownedQuantities.Length; i++)
        {
            ownedQuantities[i] = 0;
        }

        // Add listener for when the dropdown value changes
        dropdown.onValueChanged.AddListener(delegate {
            UpdateDisplay(dropdown.value);
        });

        // Initialize the image, cost label, and owned label with the first option's values
        UpdateDisplay(dropdown.value);
    }

    // Method to update the sprite, cost label, and owned label based on the dropdown option
    public void UpdateDisplay(int optionIndex)
    {
        // Check if the index is valid for sprites
        if (optionIndex >= 0 && optionIndex < optionSprites.Length)
        {
            displayImage.sprite = optionSprites[optionIndex];
        }

        // Check if the index is valid for costs
        if (optionIndex >= 0 && optionIndex < optionCosts.Length)
        {
            costLabel.text = "Cost: " + optionCosts[optionIndex].ToString("F2"); // Display cost with 2 decimal points
        }

        // Update the owned label
        if (optionIndex >= 0 && optionIndex < ownedQuantities.Length)
        {
            ownedLabel.text = "Owned: " + ownedQuantities[optionIndex]; // Display owned count
        }
    }
}



