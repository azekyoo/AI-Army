using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


[TaskCategory("MyTasks")]
[TaskDescription("Récuperer l'ordre de l'element")]

public class ElementGetOrder : Action
{
   
    public ArmyElement armyElement;
    public SharedString Ordre;
    
    public override void OnAwake()
    {
        armyElement =(ArmyElement) GetComponent(typeof(ArmyElement));
    }

    
    
    public override TaskStatus OnUpdate()
    {
        if (armyElement == null)
        {
            Debug.Log("ArmyElement is null");
            return TaskStatus.Running; // reference to the ArmyElement has not been injected yet
        }

        Ordre.Value = armyElement.ordre;
        Debug.Log("ElementGetOrder: " + armyElement.name +" ordre " + armyElement.ordre);
        return TaskStatus.Success;
    }
}
