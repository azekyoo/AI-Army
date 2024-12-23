using UnityEngine;

public class ArmyManagerBrown : ArmyManager
{
    public override void ArmyElementHasBeenKilled(GameObject go)
	{
		base.ArmyElementHasBeenKilled(go);
		if (m_ArmyElements.Count == 0)
		{
			int nDrones = 0, nTurrets = 0, health = 0;
			ComputeStatistics(ref nDrones, ref nTurrets, ref health);
			GUIUtility.systemCopyBuffer =  "Brown\t" + ((int)Timer.Value).ToString() + "\t"+nDrones.ToString()+"\t"+nTurrets.ToString()+"\t"+health.ToString();
		
			RefreshHudDisplay(); //pour une derni�re mise � jou
		}
	}
	public void GreenArmyIsDead(string deadArmyTag)
    {
        int nDrones = 0, nTurrets = 0, health = 0;
        ComputeStatistics(ref nDrones, ref nTurrets, ref health);
		GUIUtility.systemCopyBuffer = deadArmyTag + "\t" + ((int)Timer.Value).ToString() + "\t"+nDrones.ToString()+"\t"+nTurrets.ToString()+"\t"+health.ToString();
		
		RefreshHudDisplay(); //pour une derni�re mise � jour en cas de victoire
	}
}
