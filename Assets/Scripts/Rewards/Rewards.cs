using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Rewards : MonoBehaviour, IDataPersistence
{
    public static Rewards instance;
    public Quest quest;
    public int gold;
    public Text goldCounter;

    void Awake() 
    {
        instance = this;
    }

    void Start() 
    {
        goldCounter.text = "" + gold;

    }

    public void increaseGold() {
        gold += quest.goldReward;
        goldCounter.text = "" + gold;
    }

    public void LoadData(GameData data)
    {
        this.gold = data.cash;
    }

    public void SaveData(ref GameData data)
    {
        data.cash = this.gold;
    }
}
