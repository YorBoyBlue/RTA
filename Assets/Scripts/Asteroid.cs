using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum AsteroidSize
{
	SMALL,
	MEDIUM,
	LARGE
}


public class Asteroid : NetworkBehaviour {	
	
	public float max_X = 0;
	public float max_Y = 0;
	float m_offset = 50f;
	public const float size_min = 0.5f;
	public const float size_decrement = 0.5f;
	[SerializeField] Rigidbody2D m_rb2d;

	private bool ApplicationClosing = false;
	public float size = 1;	

	// Use this for initialization
	void Start () {
		// max_X = AsteroidManager.singleton.GetBoundary().x;
		// max_Y = AsteroidManager.singleton.GetBoundary().y;
		// transform.localScale = Vector3.one * size;
		// velocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
	}

	[Server]
	void OnTriggerEnter2D(Collider2D other) {
		bool split = false;

		if(other.tag == "Bullet"){
			Destroy(other.gameObject);
			split = true;
		// size -= size_decrement;
		// if (size < size_min) {
		// 	Destroy(this.gameObject);
			
		// } else {
		// 	Split();
		// }
		} else if (other.gameObject.tag == "Avatar") {
			split = true;
			other.transform.parent.GetComponent<PlayerHealth>().TakeDamage();
		}
		if (split) {			
			if(this.tag == "Large"){				
				// int newLargeAmount = m_AsteroidManager.getLargeAsteroids() - 1;
				// m_AsteroidManager.setLargeAsteroids(newLargeAmount);
				int newMedAmount = AsteroidManager.singleton.getMedAsteroids() + 2;
				AsteroidManager.singleton.setMedAsteroids(newMedAmount);
				AsteroidManager.singleton.Spawn(1, other.transform.position);
				AsteroidManager.singleton.Spawn(1, other.transform.position);
<<<<<<< HEAD
=======
				GetComponent<SpriteRenderer>().color = new Color32(0,0,0,0);
				GetComponent<ParticleSystem>().Play();
				Destroy(this.gameObject, 0.4f);				
>>>>>>> 010f65114e2c6a5ebe4debe63edfa9db068ffcf1
			}
			if(this.tag == "Medium"){				
				// int newMedAmount = m_AsteroidManager.getMedAsteroids() - 1;
				//m_AsteroidManager.setMedAsteroids(newMedAmount);
				GetComponent<SpriteRenderer>().color = new Color32(0,0,0,0);
				GetComponent<ParticleSystem>().Play();
				
				int newSmallAmount = AsteroidManager.singleton.getSmallAsteroids() + 2;
				AsteroidManager.singleton.setSmallAsteroids(newSmallAmount);
				AsteroidManager.singleton.Spawn(0, other.transform.position);
<<<<<<< HEAD
				AsteroidManager.singleton.Spawn(0, other.transform.position);			
=======
				AsteroidManager.singleton.Spawn(0, other.transform.position);				
				Destroy(this.gameObject, 0.4f);
			}
			if(this.tag == "Small"){
				// int newSmallAmount = GetComponentInParent<AsteroidManager>().getSmallAsteroids() - 1;
				// GetComponentInParent<AsteroidManager>().setSmallAsteroids(newSmallAmount);
				GetComponent<SpriteRenderer>().color = new Color32(0,0,0,0);
				GetComponent<ParticleSystem>().Play();
				Destroy(this.gameObject, 0.4f);
>>>>>>> 010f65114e2c6a5ebe4debe63edfa9db068ffcf1
			}
			
			GetComponent<SpriteRenderer>().color = new Color32(0,0,0,0);
			GetComponent<ParticleSystem>().Play();
			Destroy(this.gameObject, 0.4f);
		}

	}

	[Server]
	void Update() {
		// if(Mathf.Abs(transform.position.x) >  (max_X + (m_offset * 0.5f)) && Mathf.Abs(transform.position.y) > (max_Y + (m_offset * 0.5f))) {
		// 	AsteroidManager.singleton.Spawn(Random.Range(0, AsteroidManager.singleton.AsteroidPrefabs.Length), new Vector2(transform.position.x * -1 + (m_offset * 0.5f), transform.position.y));
		// 	NetworkServer.Destroy(this.gameObject);
		// } else {
			if(Mathf.Abs(transform.position.x) >  max_X + m_offset){
				AsteroidManager.singleton.Spawn(Random.Range(0, AsteroidManager.singleton.AsteroidPrefabs.Length), new Vector2((m_rb2d.velocity.x > 0 ? -max_X : max_X), transform.position.y), m_rb2d.velocity.x, m_rb2d.velocity.y);
				NetworkServer.Destroy(this.gameObject);		
			} else if(Mathf.Abs(transform.position.y) > max_Y + m_offset){
				AsteroidManager.singleton.Spawn(Random.Range(0, AsteroidManager.singleton.AsteroidPrefabs.Length), new Vector2(transform.position.x, (m_rb2d.velocity.y > 0 ? -max_Y : max_Y)), m_rb2d.velocity.x, m_rb2d.velocity.y);
				NetworkServer.Destroy(this.gameObject);		
			}
		//}
	}

	public void Split() {
		
	}

	[Server]
	void OnDestroy() {
		if(!ApplicationClosing && this.tag == "Small"){
			int newAmount = AsteroidManager.singleton.getSmallAsteroids() - 1;
			AsteroidManager.singleton.setSmallAsteroids(newAmount);
		}
		if(!ApplicationClosing && this.tag == "Medium"){
			int newAmount = AsteroidManager.singleton.getMedAsteroids() - 1;
			AsteroidManager.singleton.setMedAsteroids(newAmount);
		}
		if(!ApplicationClosing && this.tag == "Large"){
			int newAmount = AsteroidManager.singleton.getLargeAsteroids() - 1;
			AsteroidManager.singleton.setLargeAsteroids(newAmount);
		}
	}
	
	[Server]
	void OnApplicationQuit()
	{
		ApplicationClosing = true;
	}
}
