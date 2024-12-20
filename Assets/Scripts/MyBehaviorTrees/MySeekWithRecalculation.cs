using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Movement;

namespace BehaviorDesigner.Runtime.Tasks.Movement{

[TaskDescription("Seek the target using the Unity NavMesh with periodic recalculations.")]
[TaskCategory("Movement")]
public class MySeekWithRecalculation : NavMeshMovement
{
    [UnityEngine.Tooltip("The GameObject that the agent is seeking.")]
    public SharedTransform target;

    [UnityEngine.Tooltip("The fraction of the distance to travel before recalculating (0 to 1).")]
    public SharedFloat fractionOfDistance = 0.3f;

    private Vector3 lastDestination;

        int index;
        static int nObjects = 0;
    public override void OnAwake()
		{
			base.OnAwake();
            index = nObjects++;
		}

    public override void OnStart()
    {
        base.OnStart();
        navMeshAgent.stoppingDistance = arriveDistance.Value;

        if (target.Value != null)
        {
            Debug.Log($"Task started with target: {target.Value.name}");
            UpdateIntermediateDestination();
        }
        else{
            Debug.LogWarning("Task started but target is null!");
            
        }
    }

    public override TaskStatus OnUpdate()
    {
        // Fail if no target exists or becomes invalid.
        if (target.Value == null)
            return TaskStatus.Failure;

        // Succeed if the agent has reached the final destination.
        if (HasArrived())
            return TaskStatus.Success;

        // Recalculate the intermediate destination if close to the last destination.
        if (Vector3.Distance(transform.position, lastDestination) < arriveDistance.Value)
        {
            UpdateIntermediateDestination();
        }

        return TaskStatus.Running;
    }

    private void UpdateIntermediateDestination()
    {
        if (target.Value == null) return;

        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = target.Value.position;
        Vector3 directionToTarget = (targetPosition - currentPosition).normalized;

        // Calculate the intermediate destination along the path to the target.
        Vector3 intermediatePosition = currentPosition + directionToTarget * fractionOfDistance.Value * Vector3.Distance(currentPosition, targetPosition);

        lastDestination = intermediatePosition;
        SetDestination(lastDestination);
            Debug.Log($"Intermediate Position: {intermediatePosition}, Target: {target.Value.name}");

    }

    public override void OnReset()
    {
        base.OnReset();
        target = null;
        fractionOfDistance = 0.5f;
    }
}
}
