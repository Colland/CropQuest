using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryPage inventoryUI;

    [SerializeField]
    private Inventory inventoryData;

    private void Start()
    {
        PrepareUI();
        //inventoryData.Initialize();
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        this.inventoryUI.OnSwapItems += HandleSwapitems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }

    private void HandleItemActionRequest(int itemIndex)
    {
        
    }

    private void HandleDragging(int itemIndex)
    {
        
    }

    private void HandleSwapitems(int itemIndex_1, int itemIndex_2)
    {
        
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        ModelInventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if(inventoryItem.IsEmpty)
        {
            inventoryUI.ResetSelection();
            return;
        }
        Item item = inventoryItem.item;
        inventoryUI.UpdateDescription(itemIndex, item.itemImage, item.name, item.Description);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                foreach(var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key,
                        item.Value.item.itemImage,
                        item.Value.quantity);
                }
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }
}
