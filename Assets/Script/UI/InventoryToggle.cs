// written by Evan Linder

using UnityEngine;

// used to toggle the inventory on and off.
public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPanel;
    private bool isInventoryOpen = false;

    void Start()
    { 
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);
    }
}

