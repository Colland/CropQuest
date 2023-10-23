using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCounter : MonoBehaviour, IDataPersistence
{
    public static ItemCounter instance;
    public Quest quest;
    public Text normalharvestCount;
    public int normalharvestCounter;
    public Text questharvestCount;
    public int questharvestCounter;
    public Text questharvestAmountrequired;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //counts the harvest outside of quest
        normalharvestCount.text = "In Bag : " + normalharvestCounter;
        //counts the harvest during the quest
        questharvestCount.text = "Collected : " + questharvestCounter;
        //is the static amount required to finish the quest
        questharvestAmountrequired.text = "Required Amount : " + quest.goal.requiredAmount;
    }

    //updates the counter after pickup outside of quest
    public void increasenormalCount()
    {
        normalharvestCounter++;
        normalharvestCount.text = "In Bag : " + normalharvestCounter;
    }

    //updates the counter during the quest
    public void increasequestCount()
    {
        questharvestCounter++;
        questharvestCount.text = "Collected : " + questharvestCounter;
    }

    public void LoadData(GameData data)
    {
        this.normalharvestCounter = data.normalharvestCounter;
        this.questharvestCounter = data.questharvestCounter;
    }

    public void SaveData(ref GameData data)
    {
        data.normalharvestCounter = this.normalharvestCounter;
        data.questharvestCounter = this.questharvestCounter;
    }
}
