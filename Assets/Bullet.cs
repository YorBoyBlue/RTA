using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed;

	void Start() {
		Destroy(this.gameObject, 5f);
		GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, speed));
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
	}
}
