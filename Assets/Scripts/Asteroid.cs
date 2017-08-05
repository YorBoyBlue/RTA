using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {	

	public const float size_min = .5f;
	public const float size_decrement = .5f;

	public float size;

	public Vector2 direction;

	// Use this for initialization
	void Start () {
		transform.localScale = Vector3.one * size;
	}

	void OnTriggerEnter2D(Collider2D other) {
		size -= size_decrement;
		if (size < size_min) {
			Destroy(this.gameObject);
		} else {
			Split();
		}
	}

	void Update() {
		
	}

	public void Split() {
		
	}
}
