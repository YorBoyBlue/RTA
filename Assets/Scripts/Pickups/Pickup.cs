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

	[Server]
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Avatar") {
			PickupManager.Instance.DecrementPickupsTotal();
			other.transform.parent.transform.parent.GetComponent<PlayerUpgrades>().AddUpgrade(this);
			Destroy(gameObject);
		}
	}
}
