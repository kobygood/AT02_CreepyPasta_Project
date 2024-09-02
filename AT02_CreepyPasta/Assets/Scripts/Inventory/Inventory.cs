using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public GameObject inventoryUI;  // Reference to the inventory UI panel
    public Sprite flashlightIcon;  // Assign this in the Inspector with an appropriate sprite

    private bool isInventoryOpen = false;

    void Start()
    {
        // Ensure the inventory UI is closed at the start
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(false);
        }

        // Add the flashlight to the inventory at the start
        AddFlashlightToInventory();
    }

    void Update()
    {
        // Toggle inventory visibility with 'I'
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    // Method to add an item to the inventory
    public void AddItem(Item newItem)
    {
        Item existingItem = items.Find(item => item.itemName == newItem.itemName);

        if (existingItem != null)
        {
            existingItem.quantity += newItem.quantity;
        }
        else
        {
            items.Add(newItem);
        }
    }

    // Method to use an item from the inventory
    public void UseItem(Item item)
    {
        if (item.isConsumable && item.quantity > 0)
        {
            item.quantity--;

            if (item.quantity <= 0)
            {
                items.Remove(item);
            }

            Debug.Log($"Used {item.itemName}");
        }
        else
        {
            Debug.Log($"{item.itemName} cannot be used or is out of stock.");
        }
    }

    // Method to get an item by its name
    public Item GetItemByName(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }

    // Method to toggle the inventory UI
    private void ToggleInventory()
    {
        if (inventoryUI != null)
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryUI.SetActive(isInventoryOpen);

            // Optional: Lock cursor when inventory is open
            Cursor.lockState = isInventoryOpen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isInventoryOpen;
        }
    }

    // Method to add the flashlight to the inventory at the start
    private void AddFlashlightToInventory()
    {
        // Assuming you have a flashlight icon and other necessary data
        Item flashlightItem = new Item("Flashlight", flashlightIcon, 1, false);
        AddItem(flashlightItem);

        Debug.Log("Flashlight added to inventory");
    }
}
