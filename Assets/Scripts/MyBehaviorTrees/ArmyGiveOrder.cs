using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("MyTasks")]
[TaskDescription("Donner les ordres aux escouades")]


public class ArmyGiveOrder : Action
{
     ArmyManager _armyManager;
     public List<ArmyElement> squad1;
     public List<ArmyElement> squad2;
     
     public override void OnAwake()
     {
          _armyManager =(ArmyManager) GetComponent(typeof(ArmyManager));
     }
     
     public override TaskStatus OnUpdate()
     {
          if (_armyManager.squad1.Count == 0 || _armyManager.squad2.Count == 0)
          {
               return TaskStatus.Failure; 
          }

          foreach (ArmyElement armyElement in _armyManager.squad1)
          {
               armyElement.ordre = "Attack";
          }

          foreach (ArmyElement armyElement in _armyManager.squad2)
          {
               armyElement.ordre = "Defense";
          }
          return TaskStatus.Success;
     }
}
