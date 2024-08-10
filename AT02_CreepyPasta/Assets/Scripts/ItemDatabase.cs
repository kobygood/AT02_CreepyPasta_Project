using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public Inventory inventory; // Reference to the Inventory script
    public Item[] items;        // Array to hold items that will be added to the inventory

    private void Start()
    {
        // Check if inventory and items are assigned
        if (inventory == null)
        {
            Debug.LogError("Inventory not assigned in ItemDatabase.");
            return;
        }

        if (items.Length == 0)
        {
            Debug.LogWarning("No items assigned to the ItemDatabase.");
            return;
        }

        // Add each item in the items array to the inventory
        foreach (Item item in items)
        {
            inventory.AddItem(item);
        }
    }
}
