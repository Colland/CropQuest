using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    public GameObject itemToGive; // The item the NPC will give to the player

    public void InteractWithPlayer()
    {
        // Check if the player's inventory is attached to the player GameObject
       // Inventory inventory = FindObjectOfType<Inventory>();

        //if (inventory != null)
       // {
        //    inventory.addToInv(itemToGive);
       //     Debug.Log("Player's inventory is full.");
      //  }
    }
}

