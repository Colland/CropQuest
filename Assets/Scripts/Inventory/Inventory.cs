using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryDisplay;

    private void Update()
    {
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