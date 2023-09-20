using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcController : MonoBehaviour, Interactable
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    public float wordSpeed;
    public GameObject contButton;
    private Coroutine typingRoutine;
    public void Interact()
    {
        if(dialoguePanel.activeInHierarchy)
        {
            zeroText();
        }
        else
        {
            dialoguePanel.SetActive(true);
            typingRoutine = StartCoroutine(Typing());
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);

        if(index < dialogue.Length - 1)
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
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        if(dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }
}
