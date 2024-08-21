using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public int quantity;
    public bool isConsumable;

    // Constructor
    public Item(string name, Sprite icon, int quantity, bool isConsumable)
    {
        this.itemName = name;
        this.icon = icon;
        this.quantity = quantity;
        this.isConsumable = isConsumable;
    }
}
