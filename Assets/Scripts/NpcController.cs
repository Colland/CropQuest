using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcController : MonoBehaviour, Interactable
{
    public GameObject playerguiPanel;
    public Questgiver questGiver;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    public float wordSpeed;
    public GameObject contButton;
    public GameObject questButton;
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
            typingRoutine = StartCoroutine(Typing());
            InteractWithPlayer();
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);
        questButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            typingRoutine = StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    public void zeroText()
    {
        StopCoroutine(typingRoutine);
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        playerguiPanel.SetActive(true);          
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
            questButton.SetActive(true);
        }
    }

    public void InteractWithPlayer()
    {
        // Check if the player's inventory is attached to the player GameObject
        Inventory inventory = FindObjectOfType<Inventory>();
        inventory.addToInv(item);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.updateInventory();
    }

}
