using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drone : ArmyElement,IShoot
{
    [SerializeField] GameObject m_MissilePrefab;
    [SerializeField] Transform[] m_MissileSpawnPos;
	NavMeshAgent m_NavMeshAgent;

	Transform m_Transform;

	private void Awake()
	{
		m_Transform = transform;
		m_NavMeshAgent = GetComponent<NavMeshAgent>();
	}

	public void Shoot()
	{
		//adjust spawn height so i doesn't spawn in the ground
		Vector3 spawnOffset = Vector3.up * 0.5f;
		//Debug.Break();
		for (int i = 0; i < m_MissileSpawnPos.Length; i++)
		{
			Transform missileSpawnPos = m_MissileSpawnPos[i];
			Vector3 adjustedPosition = missileSpawnPos.position + spawnOffset; // Add the height offset
			GameObject newMissileGO = Instantiate(m_MissilePrefab, adjustedPosition, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized));
			newMissileGO.tag = gameObject.tag;
			Missile missile = newMissileGO.GetComponent<Missile>();
			missile.SetStartSpeed(m_NavMeshAgent.speed);
		}
	}

	public void Die()
	{
		ArmyManager.ArmyElementHasBeenKilled(gameObject);
		Destroy(gameObject);
	}

}
