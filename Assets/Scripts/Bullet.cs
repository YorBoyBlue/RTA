using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

	public Vector2 velocity;
	public PlayerHealth m_bulletOwner;
	Transform thisTransform;

	void Start() {
		thisTransform = transform;
		Destroy(thisTransform.GetChild(0).gameObject, 5f);
		GetComponent<Rigidbody2D>().AddRelativeForce(velocity);
	}

	void Update() {
		if (thisTransform.childCount == 1) {
			DestroyBullet();
		}
	}

	void DestroyBullet() {
		GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		Destroy(gameObject, 1f);
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Player")) {
			if(other.gameObject.GetComponent<PlayerHealth>().TakeDamage()) {
				m_bulletOwner.Kills++;
			}
		}
	}
}
