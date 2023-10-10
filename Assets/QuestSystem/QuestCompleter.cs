using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCompleter : MonoBehaviour
{   
    public Quest quest;
    public Player player;

    public GameObject questcompleter;

    public Text titleText;
    public Text descriptionText;
    public Text goldText;

    public void Show() {
        questcompleter.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        goldText.text = quest.goldReward.ToString();
    }
    public void Hide() {
        questcompleter.SetActive(false);
    }
}
