using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Vector2 velocity;

	void Start() {
		Destroy(this.gameObject, 5f);
		GetComponent<Rigidbody2D>().AddRelativeForce(velocity);
	}
}
