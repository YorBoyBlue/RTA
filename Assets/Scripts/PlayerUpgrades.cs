using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Upgrade {
	public PickupType type;
	public float value;
}

public class PlayerUpgrades : MonoBehaviour {

	Upgrade[] upgrades;

	public bool HasWeaponUpgrade { get { return upgrades[(int)PickupType.WeaponBoost].value > 0 ; } }
	public bool HasShieldUpgrade { get { return upgrades[(int)PickupType.Shield].value > 0 ; } }

	void Start() {
		upgrades = new Upgrade[(int)PickupType.Count];
		
		for (int i =0; i < upgrades.Length; i++) {
			upgrades[i].value = 0;
		}
	}

	public bool HasUpgrade(PickupType type) {
		return upgrades[(int)type].value > 0;
	}
	
	public void AddUpgrade(Pickup pickup) {
		upgrades[(int)pickup.type].type = pickup.type;
		upgrades[(int)pickup.type].value += pickup.value;
	}

	public bool ConsumeUpgrade(PickupType type) {
		if (HasUpgrade(type)) {
			upgrades[(int)type].value -= 1;
			return true;
		} else {
			return false; 
		}
	}
}
