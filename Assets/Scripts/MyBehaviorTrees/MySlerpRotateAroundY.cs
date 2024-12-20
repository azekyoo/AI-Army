using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Rotate slowly towards target.")]
    [TaskCategory("MyTasks")]
    public class MySlerpRotateAroundY : Action
    {
        [Tooltip("The Transform towards which the agent is rotating")]
        public SharedTransform target;
        [Tooltip("The Rotation slerping coef")]
        public SharedFloat slerpCoef;

        [Tooltip("The Arrival Angle magnitude")]
        public SharedFloat arrivalAngle;

        [Tooltip("The maximum distance allowed for rotation")]
        public SharedFloat maxRotationDistance = 15.0f;  // Added max distance for rotation


        Quaternion qEndOrient;
        public override void OnStart()
        {
        }

        // Seek the destination. Return success once the agent has reached the destination.
        // Return running if the agent hasn't reached the destination yet
        public override TaskStatus OnUpdate()
        {
            if (!target.Value) return TaskStatus.Failure;

            // Calculate the distance to the target
            float distanceToTarget = Vector3.Distance(transform.position, target.Value.position);

            if (distanceToTarget > maxRotationDistance.Value)
            {
                // If the target is out of range, don't rotate and do nothing
                Debug.Log("Target out of range for rotation.");
                return TaskStatus.Success;  // No action, just return success
            }

            Vector3 dir = Vector3.ProjectOnPlane(target.Value.position - transform.position, Vector3.up).normalized;
            qEndOrient = Quaternion.LookRotation(dir);

            Debug.DrawLine(transform.position, transform.position+dir*2, Color.red);
            //Debug.DrawLine(transform.position, transform.position + dir*4, Color.red);

            transform.rotation = Quaternion.Slerp(transform.rotation, qEndOrient, Time.deltaTime * slerpCoef.Value);
            if(Quaternion.Angle(transform.rotation,qEndOrient)< arrivalAngle.Value)
                    return TaskStatus.Success;

            return TaskStatus.Running;
        }
        

        public override void OnReset()
        {
            base.OnReset();
            target = null;
        }
    }
}