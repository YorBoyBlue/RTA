using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType {
	Shield,
	Weapon,
	Speed
}

public class Pickup : MonoBehaviour {

	public PickupType type;
	public float value;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			other.GetComponent<PlayerUpgrades>().AddUpgrade(this);
			Destroy(gameObject);
		}
		Debug.Log(other.name);
	}
}
