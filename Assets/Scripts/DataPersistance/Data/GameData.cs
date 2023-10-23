using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int cash;
    public Vector3 playerPosition;
    public int questitemCounter;
    public bool itemCounter;
    public int currentQuestAmount;
    public Boolean isQuestActive;
    public float currentExp;
    public float level;
    public int normalharvestCounter;
    public int questharvestCounter;

    public GameData()
    {
        this.cash = 0;
        this.questitemCounter = 0;
        this.itemCounter = false;
        this.isQuestActive = false;
        this.normalharvestCounter = 0;
        this.questharvestCounter = 0;
        this.currentExp = 0;
        this.level = 0;
        this.playerPosition = Vector3.zero;
    }
}
