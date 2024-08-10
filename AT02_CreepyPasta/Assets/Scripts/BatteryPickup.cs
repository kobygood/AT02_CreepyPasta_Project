using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public Item batteryItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddItem(batteryItem); // Add battery to the inventory
                Destroy(gameObject); // Destroy the battery item in the scene
            }
        }
    }
}
