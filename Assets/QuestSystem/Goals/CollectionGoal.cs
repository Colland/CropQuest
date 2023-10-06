using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionGoal : Quest.QuestGoal
{
   public string crops;

   public override string GetDescription() {
    return $"Collect 10 {Crops}";
   }

   public override void Initialize() {
      base.Initialize();
      EventManager.Instance.AddListener<CropGameEvent>(OnCrop);
   }
   private void OnCrop(CropGameEvent eventInfo) {
      if (eventInfo.CropName == Crop) {
         CurrentAmount++;
         Evaluate();
      }
   }
}
