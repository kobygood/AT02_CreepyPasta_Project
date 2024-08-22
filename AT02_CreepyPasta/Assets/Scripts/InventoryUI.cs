using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;  // Reference to your Inventory script
    public GameObject inventorySlotPrefab;  // Prefab for inventory slot
    public Transform inventoryPanel;  // Panel where slots will be added

    void Start()
    {
        // Check if all necessary references are assigned
        if (inventory == null || inventorySlotPrefab == null || inventoryPanel == null)
        {
            Debug.LogError("InventoryUI is not set up correctly. Please assign all references in the Inspector.");
            return;
        }

        UpdateInventoryUI();  // Update the UI at the start
    }

    // Call this method whenever you need to update the inventory UI
    public void UpdateInventoryUI()
    {
        // Clear existing slots
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        // Create a new slot for each item in the inventory
        foreach (Item item in inventory.items)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            inventorySlot.SetItem(item, inventory);

            // Debugging help: Log that a slot was created for an item
            Debug.Log("Created inventory slot for item: " + item.itemName);
        }
    }
}
