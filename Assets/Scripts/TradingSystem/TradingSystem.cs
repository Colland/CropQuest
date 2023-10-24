using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingSystem : MonoBehaviour
{
    public Player player;
    public static TradingSystem instance;
    public GameObject npc;
    public GameObject tradeWindow;
    public Questgiver questgiver;
    public int playergold;
    public int npcrequirement;
    public int newamount;
    public Quest quest;

    void Awake()
    {
        instance = this;
        // quest.description = "Congrats you have saved the cows";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        this.quest = player.quest;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                tradeWindow.SetActive(true);
            }
            else
            {
                tradeWindow.SetActive(false);
            }
        }

        Trade();
        

        playergold = Rewards.instance.gold;
        npcrequirement = quest.goal.requiredAmount;
        

        newamount = playergold - npcrequirement;
        Rewards.instance.goldCounter.text = "" + newamount;
        playergold = newamount;
        
        
    }

    void Trade()
    {
        if (quest.isActive)
        {
            quest.goal.Traded();
                
                quest.goal.isCompleted = true;
                Debug.Log("Gold given");
                quest.Complete();
                questgiver.hideObjective();
                questgiver.QuestCompletePopup();
                npc.SetActive(false);
                // Rewards.instance.gold += player.quest.goldReward; 
                // Rewards.instance.increaseGold();
                // //increase xp
                // ExpController.instance.currentExp += player.quest.expReward;
            
        }
        quest.isActive = false;
    }

    public void UpdateGold()
    {
        Rewards.instance.gold -= Rewards.instance.quest.goldReward;
        Rewards.instance.goldCounter.text = "" + Rewards.instance.gold;
    }


}
