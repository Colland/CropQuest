using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Questgiver : MonoBehaviour
{
    public Quest quest;
    public Player player;
    
    public GameObject questPopup;
    public GameObject questcompPopup;
    public GameObject objective;
    
    public Text titleText;
    public Text descriptionText;
    public Text goldText;
    public Text expText;
    public TextMeshProUGUI objectiveText;

    public Text compText;
    public Text compDescription;
    public Text compgoldText;
    public Text compexpText;

    public void OpenQuestPopup() {
        questPopup.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        goldText.text = "Gold: " + quest.goldReward.ToString();
        expText.text = "EXP: " + quest.expReward.ToString();
    }

    public void QuestCompletePopup()
    {
        questcompPopup.SetActive(true);
        compText.text = quest.title;
        compDescription.text = quest.description;
        compgoldText.text = "Gold: " + player.quest.goldReward.ToString();
        compexpText.text = "EXP: " + player.quest.expReward.ToString();
    }

    public void showObjective()
    {
        objective.SetActive(true);
        objectiveText.text = quest.description;
    }

    public void hideObjective()
    {
        objective.SetActive(false);
        objectiveText.text = quest.description;
    }

    public void AcceptQuest() {
        questPopup.SetActive(false);
        quest.isActive = true;
        player.quest = quest;
        showObjective();
    }

    public void DeclineQuest() {
        questPopup.SetActive(false);
        quest.isActive = false;
    }

    public void HideUI() {
        questcompPopup.SetActive(false);
    }
}
