using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryDisplay;

    public GameObject invItem1;
    public GameObject invItemNum1;

    private void Update()
    {
        /*
        Need to add code to fetch and store items here.
        */

        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryVisibility();
        }
    }

    private void ToggleInventoryVisibility()
    {
        // Invert the current visibility state of inventoryDisplay
        inventoryDisplay.SetActive(!inventoryDisplay.activeSelf);
    }
}