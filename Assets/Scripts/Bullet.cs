﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

	public Vector2 velocity;
	public PlayerHealth m_bulletOwner;
	Transform thisTransform;

	void Start() {
		thisTransform = transform;
		//Destroy(thisTransform.GetChild(0).gameObject, 5f);
	}

	void Update() {
		// if (thisTransform.childCount == 1) {
		// 	DestroyBullet();
		// }
	}

	void DestroyBullet() {
		GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		Destroy(gameObject, 1f);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Avatar")) {
			GameObject parent = other.gameObject.transform.parent.gameObject;
			Debug.Log(parent);
			if(m_bulletOwner != parent.GetComponent<PlayerHealth>() && parent.GetComponent<PlayerHealth>().TakeDamage()) {
				Debug.Log("kjsdfhksldjfslfjd");
				m_bulletOwner.Kills++;
			}
		}
	}
}
