using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private InventoryDescription itemDescription;

    [SerializeField]
    private MouseFollower mouseFollower;

    List<InventoryItem> listOfItems = new List<InventoryItem>();

    private int currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequested,
                             OnItemActionRequested,
                             OnStartDragging;
    
    public event Action<int, int> OnSwapItems;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
    }

    public void InitializeInventoryUI(int inventorySize)
    {
        for(int i = 0; i < inventorySize; i++)
        {
            InventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentPanel);
            listOfItems.Add(item);
            item.OnItemClicked += HandleItemSelection;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemEndDrag += HandleEndDrag;
            item.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }

    public void HandleItemSelection(InventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if(index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);
    }

    public void HandleBeginDrag(InventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if(index == -1)
            return;
        currentlyDraggedItemIndex = index;
        HandleItemSelection(inventoryItem);
        OnStartDragging?.Invoke(index);
    }

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }

    public void HandleSwap(InventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if(index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    public void HandleEndDrag(InventoryItem inventoryItem)
    {
        ResetDraggedItem();
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if(listOfItems.Count > itemIndex)
        {
            listOfItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    public void HandleShowItemActions(InventoryItem inventoryItem)
    {
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach(InventoryItem item in listOfItems)
        {
            item.Deselect();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

    internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        itemDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        listOfItems[itemIndex].Select();
    }
}
