using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public GameObject playerguiPanel;
    public TextMeshProUGUI dialogueText;
    public GameObject Gold;
    public GameObject XPSystem;
    private string[] npcDialogue;
    public Questgiver quest;
    private int index;
    private GameObject dialoguePanel;

    public float wordSpeed;
    public GameObject contButton;
    private Coroutine typingRoutine;
    private Player player;

    public void startInteraction(string[] npcLines, Questgiver quest)
    {
        npcDialogue = npcLines;
        this.quest = quest;
        dialoguePanel = this.gameObject;

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
        }
    }
    public void NextLine()
    {
        contButton.SetActive(false);

        if (index < npcDialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            typingRoutine = StartCoroutine(Typing());
        }
        else
        {
            zeroText();
            quest.OpenQuestPopup();
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
        foreach (char letter in npcDialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        if (dialogueText.text == npcDialogue[index])
        {
            contButton.SetActive(true);
        }
    }

    public void AcceptQuest()
    {
        this.quest.AcceptQuest();
    }
}
