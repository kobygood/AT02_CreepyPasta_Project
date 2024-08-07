using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite itemIcon;
}

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public GameObject inventoryPanel;
    public GameObject inventorySlotPrefab;
    public Transform slotParent;

    private bool isInventoryOpen = false;
    private int selectedItemIndex = -1;

    private void Start()
    {
        // Start with inventory closed
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Inventory Panel is not assigned in the Inspector");
        }
    }

    private void Update()
    {
        // Toggle inventory visibility when the I key is pressed
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        // Change selected item based on number keys 1-5
        if (isInventoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) SelectItem(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) SelectItem(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) SelectItem(2);
            if (Input.GetKeyDown(KeyCode.Alpha4)) SelectItem(3);
            if (Input.GetKeyDown(KeyCode.Alpha5)) SelectItem(4);
        }
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(isInventoryOpen);
        }

        if (isInventoryOpen)
        {
            DisplayInventory();
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        if (isInventoryOpen)
        {
            DisplayInventory();
        }
    }

    private void DisplayInventory()
    {
        if (slotParent == null)
        {
            Debug.LogError("Slot Parent is not assigned in the Inspector");
            return;
        }

        // Clear existing slots
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        // Create new slots
        for (int i = 0; i < items.Count; i++)
        {
            Item item = items[i];
            GameObject slot = Instantiate(inventorySlotPrefab, slotParent);
            slot.GetComponentInChildren<UnityEngine.UI.Image>().sprite = item.itemIcon;
            slot.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName;

            // Optionally, you can add some indication of selection here
            if (i == selectedItemIndex)
            {
                slot.GetComponentInChildren<UnityEngine.UI.Image>().color = Color.yellow; // Highlight selected item
            }
        }
    }

    private void SelectItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            selectedItemIndex = index;
        }
    }
}
