using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmyElement : MonoBehaviour, IArmyElement
{
	public ArmyManager ArmyManager { get; set; }
	[SerializeField] Health m_Health;
	public float Health { get => m_Health.Value; }

	public string ordre { get; set; }
	public void Die()
	{
		ArmyManager.ArmyElementHasBeenKilled(gameObject);
		Destroy(gameObject);
	}
	
}
