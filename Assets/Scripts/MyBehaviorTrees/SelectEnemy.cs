using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("MyTasks")]
[TaskDescription("Select non targeted enemy turret")]

public class SelectEnemy : Action
{
    IArmyElement m_ArmyElement;
    public SharedTransform target;
    public SharedString Ordre;
    public SharedFloat minRadius;
    public SharedFloat maxRadius;
    
    [SerializeField] private string attack="Attack";
    [SerializeField] private string defense="Defense";

    public override void OnAwake()
    {
        m_ArmyElement =(IArmyElement) GetComponent(typeof(IArmyElement));
    }

    public override TaskStatus OnUpdate()
    {
        if (m_ArmyElement.ArmyManager == null) return TaskStatus.Running; // reference to the ArmyManager has not been injected yet

        int countturret = m_ArmyElement.ArmyManager.GetNumberEnnemies<Turret>();
        Debug.Log(countturret);
        if (Ordre.Value == attack)
        {
            target.Value = countturret != 0 ? m_ArmyElement.ArmyManager.GetClosestEnemy<Turret>(transform.position,minRadius.Value,maxRadius.Value)?.transform : m_ArmyElement.ArmyManager.GetClosestEnemy<Drone>(transform.position,minRadius.Value,maxRadius.Value)?.transform;
        }
        else if (Ordre.Value == defense)
        {
            target.Value = m_ArmyElement.ArmyManager.GetClosestEnemy<Drone>(transform.position,minRadius.Value,maxRadius.Value)?.transform;
        }
       
        if (target.Value != null) 
            return TaskStatus.Success;
        else return TaskStatus.Failure;

    }
}
