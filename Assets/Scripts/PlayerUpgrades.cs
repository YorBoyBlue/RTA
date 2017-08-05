using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Upgrade {
	public PickupType type;
	public float value;
}

public class PlayerUpgrades : MonoBehaviour {

	[SerializeField]
	List<Upgrade> upgradeList;

	void Start() {
		upgradeList = new List<Upgrade>();
	}
	
	public void AddUpgrade(Pickup pickup) {
		Upgrade item = new Upgrade();
		item.type = pickup.type;
		item.value = pickup.value;
		upgradeList.Add(item);

		Debug.Log("Picked up a powerup, Value: " + upgradeList[upgradeList.Count -1].value + " And Type: " + upgradeList[upgradeList.Count - 1].type);
	}
}
