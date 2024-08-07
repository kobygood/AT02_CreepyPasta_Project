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
                inventory.AddItem(batteryItem);
                Destroy(gameObject); // Remove battery from the scene
            }
        }
    }
}
