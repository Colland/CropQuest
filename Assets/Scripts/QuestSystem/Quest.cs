using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{  
    public bool isActive;
    public string title;
    public string description;
    public int goldReward;
    public float expReward;
    public QuestGoal goal;
    public Questgiver questgiver;

    public void Complete() {
        isActive = false;
        description = "Completed";
    }
    
}
