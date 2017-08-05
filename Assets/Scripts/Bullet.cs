using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

	public Vector2 velocity;

	public PlayerHealth m_bulletOwner;

	void Start() {
		Destroy(this.gameObject, 10f);
		GetComponent<Rigidbody2D>().AddRelativeForce(velocity);
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Player")) {
			if(other.gameObject.GetComponent<PlayerHealth>().TakeDamage()) {
				m_bulletOwner.Kills++;
			}
		}
	}
}
