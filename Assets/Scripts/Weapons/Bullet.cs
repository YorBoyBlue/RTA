using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

	public PlayerHealth m_bulletOwner;
	Transform thisTransform;

	void Start() {
		thisTransform = transform;
		Destroy(thisTransform.GetChild(0).gameObject, 5f);
	}

	void Update() {
		if (thisTransform.childCount == 1) {
			DestroyBullet();
		}
	}

	public void DestroyBullet() {
		GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		Destroy(gameObject, 1f);
	}

	[Server]
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Avatar")) {
			GameObject parent = other.gameObject.transform.parent.transform.parent.gameObject;
			if(m_bulletOwner != parent.GetComponent<PlayerHealth>()) {
				if(parent.GetComponent<PlayerHealth>().TakeDamage()) {
					m_bulletOwner.Kills++;
				}
			}
		}
	}
}
