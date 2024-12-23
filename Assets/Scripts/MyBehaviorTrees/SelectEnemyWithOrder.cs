using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


[TaskCategory("MyTasks")]
[TaskDescription("Select enemy Drone With Order")]

public class SelectEnemyWithOrder : Action
{
    IArmyElement m_ArmyElement;
    public SharedTransform target;
    public SharedFloat minRadius;
    public SharedFloat maxRadius;
    public SharedString ordre;
    /* Vector3 m_Position;
    public Vector3 Position
    { get { return m_Position; } set { m_Position = value; }
    }*/

    public override void OnAwake()
    {
        m_ArmyElement =(IArmyElement) GetComponent(typeof(IArmyElement));
    }

    public override TaskStatus OnUpdate()
    {
        if (m_ArmyElement.ArmyManager == null) return TaskStatus.Running; // la r�f�rence � l'arm�e n'a pas encore �t� inject�e
        
        if(ordre.Value == "Attack")
            target.Value = m_ArmyElement.ArmyManager.GetFurthestEnemy<Drone>(transform.position,minRadius.Value,maxRadius.Value)?.transform;
        else if (ordre.Value == "Defense")
        {
            target.Value = m_ArmyElement.ArmyManager.GetClosestEnemy<Drone>(transform.position,minRadius.Value,maxRadius.Value)?.transform;
        }
        
        if (target.Value != null) return TaskStatus.Success;
        else return TaskStatus.Failure;

    }
}
