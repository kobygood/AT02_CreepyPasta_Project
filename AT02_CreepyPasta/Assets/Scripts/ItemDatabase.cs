using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public Inventory inventory;
    public Item[] items;

    private void Start()
    {
        // Add some items to the inventory for testing
        foreach (Item item in items)
        {
            inventory.AddItem(item);
        }
    }
}
