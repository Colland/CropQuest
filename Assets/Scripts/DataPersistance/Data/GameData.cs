using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    public int cash;
    public Vector2 playerPosition;
    public int questitemCounter;
    public GameData()
    {
        this.cash = 0;
        this.playerPosition = Vector2.zero;
        this.questitemCounter = 0;
    }
}
