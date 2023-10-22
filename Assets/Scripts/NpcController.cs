using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcController : MonoBehaviour, Interactable
{
    public GameObject Gold;
    public GameObject XPSystem;
    public GameObject playerguiPanel;
    public Questgiver questGiver;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    public float wordSpeed;
    public GameObject contButton;
    private Coroutine typingRoutine;
    public GameObject item;

    private Player player;

    public void Interact()
    {
        if (dialoguePanel.activeInHierarchy)
        {
            zeroText();
        }
        else
        {
            dialoguePanel.SetActive(true);
            playerguiPanel.SetActive(false);
            Gold.SetActive(false);
            XPSystem.SetActive(false);
            typingRoutine = StartCoroutine(Typing());
            InteractWithPlayer();
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            typingRoutine = StartCoroutine(Typing());
        }
        else
        {
            zeroText();
            questGiver.OpenQuestPopup();
        }
    }

    public void zeroText()
    {
        StopCoroutine(typingRoutine);
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        playerguiPanel.SetActive(true);
        Gold.SetActive(true);   
        XPSystem.SetActive(true);       
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
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
