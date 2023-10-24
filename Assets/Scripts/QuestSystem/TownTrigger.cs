using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public GameObject TownTrigger;
    public Questgiver questGiver;
    public Player player;
    public Quest quest;

    void Awake()
    {
        
    }

    void Update()
    {
        this.quest = player.quest;
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if (quest.isActive)
        {
            quest.goal.HasBeenVisited();
            if(other.CompareTag("Player"))
            {
                quest.goal.isCompleted = true;
                Debug.Log("Town visited");
                TownTrigger.SetActive(false);
                quest.Complete();
                questGiver.hideObjective();
                questGiver.QuestCompletePopup();
                Rewards.instance.gold += player.quest.goldReward; 
                Rewards.instance.increaseGold();
                //increase xp
                ExpController.instance.currentExp += player.quest.expReward;

            }
        }
    }
}
