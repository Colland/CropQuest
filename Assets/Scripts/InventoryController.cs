using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryPage inventoryUI;

    [SerializeField]
    private Inventory inventoryData;

    [SerializeField]
    private GameObject hotBar1;
    [SerializeField]
    private GameObject hotBar2;
    [SerializeField]
    private GameObject hotBar3;

    public List<ModelInventoryItem> initialItems = new List<ModelInventoryItem>();

    private void Start()
    {
        PrepareUI();
        PrepareInventoryData();
    }

    private void PrepareInventoryData()
    {
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        foreach(ModelInventoryItem item in initialItems)
        {
            if(item.IsEmpty)
                continue;
            inventoryData.AddItem(item);
        }

        UpdateHotbarItems();
    }

    private void UpdateInventoryUI(Dictionary<int, ModelInventoryItem> inventoryState)
    {
        inventoryUI.ResetAllItems();
        foreach(var item in inventoryState)
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.itemImage, item.Value.quantity);
        }
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
        ModelInventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if(inventoryItem.IsEmpty)
            return;
        inventoryUI.CreateDraggedItem(inventoryItem.item.itemImage, inventoryItem.quantity);
    }

    private void HandleSwapitems(int itemIndex_1, int itemIndex_2)
    {
        inventoryData.SwapItems(itemIndex_1, itemIndex_2);
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

    //this some dirtyyyy last minute code
    private void UpdateHotbarItems()
    {
        ModelInventoryItem hotbarItem1 = inventoryData.GetItemAt(0);
        Image hotBarSprite1 = hotBar1.transform.Find("Hotbar1Image").GetComponent<Image>();

        ModelInventoryItem hotbarItem2 = inventoryData.GetItemAt(1);
        Image hotBarSprite2 = hotBar2.transform.Find("Hotbar2Image").GetComponent<Image>();

        ModelInventoryItem hotbarItem3 = inventoryData.GetItemAt(2);
        Image hotBarSprite3 = hotBar3.transform.Find("Hotbar3Image").GetComponent<Image>();
        
        try
        {
            var sprite1 = hotbarItem1.item.itemImage;
            hotBarSprite1.sprite = sprite1;
            hotBarSprite1.enabled = true;
        }
        catch(NullReferenceException e)
        {
            hotBarSprite1.enabled = false;
        }

        try
        {
            var sprite2 = hotbarItem2.item.itemImage;
            hotBarSprite2.sprite = sprite2;
            hotBarSprite2.enabled = true;
        }
        catch(NullReferenceException e)
        {
            hotBarSprite2.enabled = false;
        }

        try
        {
            var sprite3 = hotbarItem3.item.itemImage;
            hotBarSprite3.sprite = sprite3;
            hotBarSprite3.enabled = true;
        }
        catch(NullReferenceException e)
        {
            hotBarSprite3.enabled = false;
        }
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

                hotBar1.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                inventoryUI.Hide();
                hotBar1.transform.parent.gameObject.SetActive(true);
                UpdateHotbarItems();
            }
        }
    }
}
