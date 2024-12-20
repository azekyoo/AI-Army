using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


[TaskCategory("MyTasks")]
[TaskDescription("Drone Shoots")]

public class DroneShoot : Action
{
	Drone drone;

	[UnityEngine.Tooltip("The Transform of the target to check distance")]
	public SharedTransform target;
	[UnityEngine.Tooltip("The maximum distance allowed for shooting")]
	public SharedFloat maxShootingDistance = 15.0f;

	public override void OnStart()
	{
		drone = GetComponent<Drone>();
	}

	public override TaskStatus OnUpdate()
	{
		if (drone && target.Value)
		{// Calculate distance to the target
			float distanceToTarget = Vector3.Distance(transform.position, target.Value.position);

			// Debug: Draw a line to the target and log the distance
			Debug.DrawLine(transform.position, target.Value.position, Color.green);
			Debug.Log($"Target is: {target.Value}");
			Debug.Log($"I am: {drone}");
			Debug.Log($"Distance to Target: {distanceToTarget} meters");

			// Check if the target is within the shooting distance
			if (distanceToTarget <= maxShootingDistance.Value)
			{
				Debug.Log("Target within range. Shooting...");
				drone.Shoot();
				return TaskStatus.Success;
			}
			else {
				return TaskStatus.Success;
			}
		}
		else 
		{
			return TaskStatus.Success;
		}
	}
}

//QUARANTINE
/*
 * //drone.ArmyManager.UnlockArmyElement(gameObject);
 */