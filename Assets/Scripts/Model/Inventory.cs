using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    [SerializeField]
    private List<ModelInventoryItem> inventoryItems;

    [field: SerializeField]
    public int Size { get; set; } = 10;

    public void Initialize()
    {
        inventoryItems = new List<ModelInventoryItem>();
        for(int i = 0; i < Size; i++)
        {
            inventoryItems.Add(ModelInventoryItem.GetEmptyItem());
        }
    }

    public void AddItem(Item item, int quantity)
    {
       for(int i = 0; i < inventoryItems.Count; i++)
        {
            if(inventoryItems[i].IsEmpty)
            {
                inventoryItems[i] = new ModelInventoryItem
                {
                    item = item,
                    quantity = quantity,
                };
            }
        } 
    }
    //blah
    public Dictionary<int, ModelInventoryItem> GetCurrentInventoryState()
    {
        Dictionary<int, ModelInventoryItem> returnValue =
                    new Dictionary<int, ModelInventoryItem>();
        
        for(int i = 0; i < inventoryItems.Count; i++)
        {
            if(inventoryItems[i].IsEmpty)
                continue;
            returnValue[i] = inventoryItems[i];
        }
        return returnValue;
    }

    public ModelInventoryItem GetItemAt(int itemIndex)
    {
        return inventoryItems[itemIndex];
    }
}

[Serializable]
public struct ModelInventoryItem
{
    public int quantity;
    public Item item;
    public bool IsEmpty => item == null;

    public ModelInventoryItem ChangeQuantity(int newQuantity)
    {
        return new ModelInventoryItem
        {
            item = this.item,
            quantity = newQuantity,
        };
    }

    public static ModelInventoryItem GetEmptyItem()
        => new ModelInventoryItem
        {
            item = null,
            quantity = 0,
        };
}
