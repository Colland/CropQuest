using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : ScriptableObject
{
    [System.Serializable]
    public struct Info {
        public string Name;
        public Sprite Icon;
        public string Description;
    }

    [Header("Info")] public Info Information;


    [System.Serializable]
    public struct Stat {
        public int Currency;
        public int XP;
    }

    [Header("Reward")] public Stat_Reward = new Stat{Currency = 10; XP = 10};

    public bool Completed { get; protected set; }
    public QuestCompletedEvent QuestCompleted;

    public abstract class QuestGoal : ScriptableObject {
        protected string_Description;

        public int CurrentAmount { get; protected set; }
        public int RequiredAmount = 1;

        public bool Completed { get; protected set;}
        [HideInInspector] public UnityEven GoalCompleted;

        public virtual string GetDescription() {
            return GetDescription;
        }

        public virtual void Initialize() {
            Completed = false;
            GoalCompleted = new UnityEvent;
        }

        protected void Evaluate() {
            if(CurrentAmount >= RequiredAmount) {
                Completed();
            }
        }

        private void Complete() {
            Completed = true;
            GoalCompleted.Invoke();
            GoalCompleted.RemoveAllListeners();
        }

    }

    public List<QuestGoal> Goals;

    public void Initialize() {
        Completed = false;
        QuestCompleted = new QuestCompletedEvent();

        foreach (var goal in Goals) {
            goal.Initialize();
            goal.GoalCompleted.AddListener(call: delegate { CheckGoals(); });
        }
    }

    private void CheckGoals() {
        Completed = Goals.All(g:QuestGoal => g.Completed);
        if (Completed) {
            //give reward
            QuestCompleted.Invoke( this);
            QuestCompleted.RemoveAllListeners();
        }
    }
}

public class QuestCompletedEvent : UnityEvent<Quest> { }