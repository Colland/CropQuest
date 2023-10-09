using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
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
}

public enum GoalType {
    Harvesting,
    Planting,
}