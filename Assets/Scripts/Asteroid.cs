using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {	

	public const float size_min = 0.5f;
	public const float size_decrement = 0.5f;
	public GameObject[] m_Asteroid;
	private Vector3 velocity;


	public float size;	

	// Use this for initialization
	void Start () {
		//transform.localScale = Vector3.one * size;
		velocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Collision");
		size -= size_decrement;
		if (size < size_min) {
			Destroy(this.gameObject);
			
		} else {
			Split();
		}
	}

	void Update() {
		GetComponent<Rigidbody2D>().AddRelativeForce(velocity);
		transform.localScale = Vector3.one * size;
	}

	public void Split() {
		
	}

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy()
	{
		
	}
}
