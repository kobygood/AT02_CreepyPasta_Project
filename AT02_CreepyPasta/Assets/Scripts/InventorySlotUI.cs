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

       // Ensure the icon is set and enabled
       if (item.icon != null)
       {
           icon.sprite = item.icon;
           icon.enabled = true;  // Ensure the icon is enabled
       }
       else
       {
           icon.enabled = false;  // Disable the icon if no sprite is assigned
       }

       // Ensure the item name and quantity text are set and enabled
       itemNameText.text = item.itemName;
       itemNameText.enabled = true;  // Ensure the item name text is enabled

       quantityText.text = item.quantity.ToString();
       quantityText.enabled = true;  // Ensure the quantity text is enabled

       // Show or hide the button based on whether the item is consumable
       if (item.isConsumable)
       {
           useButton.gameObject.SetActive(true);  // Show the button
           useButton.interactable = true;  // Make the button interactable
       }
       else
       {
           useButton.gameObject.SetActive(false);  // Hide the button
       }

       // Debugging statements to verify the component states
       Debug.Log("Setting item: " + item.itemName);
       Debug.Log("Icon enabled: " + icon.enabled);
       Debug.Log("ItemNameText enabled: " + itemNameText.enabled);
       Debug.Log("QuantityText enabled: " + quantityText.enabled);
       Debug.Log("UseButton active: " + useButton.gameObject.activeSelf);
       Debug.Log("UseButton interactable: " + useButton.interactable);
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
