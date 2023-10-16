using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal //: IDataPersistence
{
    public GoalType goalType;   

    public int requiredAmount;
    public int currentAmount;

    public bool IsReached() {
        return (currentAmount >= requiredAmount);
    }

    public void Harvested() {
        if (goalType == GoalType.Harvesting)
        currentAmount++;
    }

    public void Planted() {
        if (goalType == GoalType.Planting)
        currentAmount++;
    }

    public void Reset() {
        currentAmount = 0;
    }

    // public void LoadData(GameData data)
    // {
    //     this.currentAmount = data.currentQuestAmount;
    // }

    // public void SaveData(ref GameData data)
    // {
    //     data.currentQuestAmount = this.currentAmount;
    // }
}

public enum GoalType {
    Harvesting,
    Planting,
}