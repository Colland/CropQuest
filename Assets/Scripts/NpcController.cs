using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcController : MonoBehaviour, Interactable
{
    public DialogueController dialogueController;
    public string[] dialogue;
    public Questgiver quest;

    public void Interact()
    {
        dialogueController.startInteraction(dialogue, quest);
    }

    public void InteractWithPlayer()
    {
        //IM WORKING ON INVENTORY SO THIS GOTTA GO FOR A BIT - Tristan
        // Check if the player's inventory is attached to the player GameObject
       // Inventory inventory = FindObjectOfType<Inventory>();
        //inventory.addToInv(item);
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //player.updateInventory();
    }

}
