using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;  // Reference to your Inventory script
    public List<GameObject> inventorySlots;  // List of existing manually placed slots in the InventoryPanel

    void Start()
    {
        // Check if all necessary references are assigned
        if (inventory == null || inventorySlots.Count == 0)
        {
            Debug.LogError("InventoryUI is not set up correctly. Please assign all references in the Inspector.");
            return;
        }

        UpdateInventoryUI();  // Update the UI at the start
    }

    // Call this method whenever you need to update the inventory UI
    public void UpdateInventoryUI()
    {
        // Reset all slots: Hide them initially
        foreach (GameObject slot in inventorySlots)
        {
            slot.SetActive(false);
        }

        // Ensure there are enough slots for the items in the inventory (excluding the first slot)
        if (inventory.items.Count > inventorySlots.Count - 1)
        {
            Debug.LogError("Not enough inventory slots for the number of items.");
            return;
        }

        // Assign the flashlight to the first slot
        if (inventory.items.Count > 0)
        {
            Item flashlightItem = inventory.items.Find(item => item.itemName == "Flashlight");
            if (flashlightItem != null)
            {
                GameObject firstSlot = inventorySlots[0];
                firstSlot.SetActive(true);
                InventorySlot inventorySlot = firstSlot.GetComponent<InventorySlot>();
                inventorySlot.SetItem(flashlightItem, inventory);
            }
        }

        // Start assigning the remaining items from the second slot onward
        int itemIndex = 0;
        for (int i = 0; i < inventory.items.Count; i++)
        {
            Item item = inventory.items[i];

            // Skip the flashlight since it's already assigned
            if (item.itemName == "Flashlight")
                continue;

            // Skip the first slot and assign items to the following slots
            GameObject slot = inventorySlots[itemIndex + 1];
            slot.SetActive(true);
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            inventorySlot.SetItem(item, inventory);

            // Debugging help: Log that a slot was assigned an item
            Debug.Log("Assigned item: " + item.itemName + " to slot " + (itemIndex + 1));

            itemIndex++;
        }
    }
}
