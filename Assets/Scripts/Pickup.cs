using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum PickupType {
	Shield,
	WeaponDouble,
	WeaponBoost,
	Speed,
	Count
}

public class Pickup : NetworkBehaviour {

	public PickupType type;
	public float value;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Avatar") {
			other.transform.parent.GetComponent<PlayerUpgrades>().AddUpgrade(this);
			Destroy(gameObject);
		}
	}

	[Server]
	void OnDestroy() {
		AsteroidManager.singleton.DecrementTotalPickups();
	}
}
