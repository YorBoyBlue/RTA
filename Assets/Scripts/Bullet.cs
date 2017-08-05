using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Vector2 velocity;

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
}
