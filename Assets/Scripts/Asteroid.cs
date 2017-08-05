using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AsteroidSize
{
	SMALL,
	MEDIUM,
	LARGE
}


public class Asteroid : MonoBehaviour {	
	

	public const float size_min = 0.5f;
	public const float size_decrement = 0.5f;
	private AsteroidManager m_Manager;

	private Vector3 velocity;


	public float size = 1;	

	// Use this for initialization
	void Start () {
		m_Manager = GetComponentInParent<AsteroidManager>();
		//transform.localScale = Vector3.one * size;
		velocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Bullet"){
			Destroy(other.gameObject);
		}
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
		if(GetComponentInParent<AsteroidManager>()){
			GetComponentInParent<AsteroidManager>().Spawn(AsteroidSize.LARGE);
		}
	}
}
