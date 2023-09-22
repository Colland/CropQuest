using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryDisplay;

    public GameObject invItem1;
    public int invItemNum1;
    public Sprite newSprite;
    private SpriteRenderer invItem1Image;

    private void Start()
    {
        invItemNum1 = 0;
        invItem1 = null;
    }

    private void Update()
    {
        /*
        Need to add code to fetch and store items here for full and proper inventory.
        */


        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryVisibility();
        }

        if(invItem1 != null)
        {   
            invItem1Image.sprite = newSprite;
        }
    }

    private void ToggleInventoryVisibility()
    {
        // Invert the current visibility state of inventoryDisplay
        inventoryDisplay.SetActive(!inventoryDisplay.activeSelf);
    }

    public void addToInv(GameObject item)
    {
        if(invItem1 == null)
        {
            invItem1 = item;
            invItemNum1++;
            Destroy(item);


        }
        else if(item == invItem1)
        {
            invItemNum1++;
            Destroy(item);
        }
    }
}