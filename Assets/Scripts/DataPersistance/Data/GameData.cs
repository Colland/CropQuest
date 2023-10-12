using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GameData
{

    public int cash;
    public Vector2 playerPosition;
    public int questitemCounter;
    public int itemCounter;
    public int currentQuestAmount;
    public Boolean isQuestActive;
    public GameData()
    {
        this.cash = 0;
        this.playerPosition = Vector2.zero;
        this.questitemCounter = 0;
        this.itemCounter = 0;
        this.isQuestActive = false;
        // this.currentQuestAmount = 0;
    }
}
