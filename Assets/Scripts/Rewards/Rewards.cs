using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Rewards : MonoBehaviour
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
}
