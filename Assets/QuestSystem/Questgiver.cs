using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questgiver : MonoBehaviour
{
    public Quest quest;
    public Player player;

    public GameObject questPopup;
    public Text titleText;
    public Text descriptionText;
    public Text goldText;

    public void OpenQuestPopup() {
        questPopup.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        goldText.text = quest.goldReward.ToString();
    }

    public void AcceptQuest() {
        questPopup.SetActive(false);
        quest.isActive = true;
        player.quest = quest;
    }

    public void DeclineQuest() {
        questPopup.SetActive(false);
        quest.isActive = false;
        //player.quest = quest;
    }
}
