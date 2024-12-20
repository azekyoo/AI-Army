using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("MyTasks")]
[TaskDescription("Créer deux escouades de drones")]

public class CreatSquad : Action
{
    ArmyManager _armyManager;
    private List<ArmyElement> allies;

    
    public override void OnAwake()
    {
        _armyManager =(ArmyManager) GetComponent(typeof(ArmyManager));
    }

    public override TaskStatus OnUpdate()
    {
        if (_armyManager == null) {
            Debug.Log("ArmyManager is null");
            return TaskStatus.Running; // reference to the ArmyManager has not been injected yet
        }
        allies = _armyManager.GetAllAllies(false);
         var nbrAllies = allies.Count;
         int moitier = nbrAllies / 2;
        if (nbrAllies > 0)
        {
            _armyManager.squad1 = allies.GetRange(0, moitier);
            _armyManager.squad2 = allies.GetRange(moitier, moitier);
            Debug.Log("Squad 1: " + _armyManager.squad1.Count + " Squad 2: " + _armyManager.squad2.Count);
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
