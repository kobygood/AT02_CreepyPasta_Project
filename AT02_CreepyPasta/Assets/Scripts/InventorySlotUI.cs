using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Make sure to include the TextMeshPro namespace

public class InventorySlot : MonoBehaviour
{
    public Image icon;  // Reference to the Image component for the item icon
    public TextMeshProUGUI itemNameText;  // Reference to the TextMeshProUGUI for the item name
    public TextMeshProUGUI quantityText;  // Reference to the TextMeshProUGUI for the item quantity
    public Button useButton;  // Reference to the Button component for the "Use" button

    private Item currentItem;
    private Inventory inventory;

    // Set up the slot with the item information
    public void SetItem(Item item, Inventory inventory)
    {
        currentItem = item;
        this.inventory = inventory;

        icon.sprite = item.icon;
        itemNameText.text = item.itemName;
        quantityText.text = item.quantity.ToString();
        useButton.interactable = item.isConsumable || item.itemName == "Flashlight";
    }

    // Called when the "Use" button is pressed
    public void OnUseButtonPressed()
    {
        if (currentItem != null && inventory != null)
        {
            if (currentItem.itemName == "Flashlight")
            {
                ToggleFlashlight();  // Toggle the flashlight on/off
            }
            else
            {
                inventory.UseItem(currentItem);  // Handle other items normally
                UpdateSlot();
            }
        }
    }

    // Toggles the flashlight on and off
    private void ToggleFlashlight()
    {
        Flashlight flashlight = FindObjectOfType<Flashlight>();
        if (flashlight != null)
        {
            flashlight.ToggleLight();  // Make sure this method exists in the Flashlight script
        }
    }

    // Updates the slot UI after an item is used
    public void UpdateSlot()
    {
        if (currentItem.quantity > 0)
        {
            quantityText.text = currentItem.quantity.ToString();
        }
        else
        {
            icon.sprite = null;
            itemNameText.text = "";
            quantityText.text = "";
            useButton.interactable = false;
        }
    }
}
