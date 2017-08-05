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
	
		// size -= size_decrement;
		// if (size < size_min) {
		// 	Destroy(this.gameObject);
			
		// } else {
		// 	Split();
		// }
			if(this.tag == "Large"){
				
				m_Manager.Spawn(1, other.transform.position);
				m_Manager.Spawn(1, other.transform.position);
				Destroy(this.gameObject);
			}
			if(this.tag == "Medium"){
				m_Manager.Spawn(0, other.transform.position);
				m_Manager.Spawn(0, other.transform.position);
				Destroy(this.gameObject);
			}
			if(this.tag == "Small"){
				Destroy(this.gameObject);
			}
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
		if(tag == "Small"){
			int newAmount = GetComponentInParent<AsteroidManager>().getSmallAsteroids() - 1;
			
			GetComponentInParent<AsteroidManager>().setSmallAsteroids(newAmount);
		}
		if(tag == "Medium"){
			int newAmount = GetComponentInParent<AsteroidManager>().getMedAsteroids() - 1;
			GetComponentInParent<AsteroidManager>().setMedAsteroids(newAmount);
		}
		if(tag == "Large"){
			int newAmount = GetComponentInParent<AsteroidManager>().getLargeAsteroids() - 1;
			GetComponentInParent<AsteroidManager>().setLargeAsteroids(newAmount);
		}
		// if(GetComponentInParent<AsteroidManager>()){
		// 	GetComponentInParent<AsteroidManager>().Spawn(1);
		// }
	}
}
