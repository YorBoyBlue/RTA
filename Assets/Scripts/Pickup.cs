using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType {
	Shield,
	Weapon,
	Speed,
	Count
}

public class Pickup : MonoBehaviour {

	public PickupType type;
	public float value;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Avatar") {
			other.transform.parent.GetComponent<PlayerUpgrades>().AddUpgrade(this);
			Destroy(gameObject);
		}
	}
}
