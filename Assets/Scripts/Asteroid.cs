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
	
	private int max_X = 30;
	private int max_Y = 30;
	public const float size_min = 0.5f;
	public const float size_decrement = 0.5f;
	private AsteroidManager m_Manager;

	private Vector3 velocity;

	private bool ApplicationClosing = false;
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
				AsteroidManager m_AsteroidManager = GetComponentInParent<AsteroidManager>();
				
				// int newLargeAmount = m_AsteroidManager.getLargeAsteroids() - 1;
				// m_AsteroidManager.setLargeAsteroids(newLargeAmount);
				if(GetComponentInParent<AsteroidManager>().GetTotalAstroids() < 10){
					int newMedAmount = m_AsteroidManager.getMedAsteroids() + 2;
					m_AsteroidManager.setMedAsteroids(newMedAmount);
					m_Manager.Spawn(1, other.transform.position);
					m_Manager.Spawn(1, other.transform.position);
				}
				Destroy(this.gameObject);
				
			}
			if(this.tag == "Medium"){
				AsteroidManager m_AsteroidManager = GetComponentInParent<AsteroidManager>();
				
				// int newMedAmount = m_AsteroidManager.getMedAsteroids() - 1;
				//m_AsteroidManager.setMedAsteroids(newMedAmount);
				if(GetComponentInParent<AsteroidManager>().GetTotalAstroids() < 10){
					int newSmallAmount = m_AsteroidManager.getSmallAsteroids() + 2;
					m_AsteroidManager.setSmallAsteroids(newSmallAmount);
					m_Manager.Spawn(0, other.transform.position);
					m_Manager.Spawn(0, other.transform.position);
				}
				
				Destroy(this.gameObject);
			}
			if(this.tag == "Small"){
				// int newSmallAmount = GetComponentInParent<AsteroidManager>().getSmallAsteroids() - 1;
				// GetComponentInParent<AsteroidManager>().setSmallAsteroids(newSmallAmount);
				GetComponent<SpriteRenderer>().color = new Color32(0,0,0,0);
				GetComponent<ParticleSystem>().Play();
				Destroy(this.gameObject, 0.4f);
			}
		}

	}

	void Update() {
		GetComponent<Rigidbody2D>().AddRelativeForce(velocity);
		if(transform.position.x > max_X ){
			//GetComponent<Rigidbody2D>().isKinematic = true;
			transform.position = new Vector2(-max_X, transform.position.y);
		
		}
		if(transform.position.x < -max_X){
			//GetComponent<Rigidbody2D>().isKinematic = true;
			transform.position = new Vector2(max_X, transform.position.y);
			
		}
		if(transform.position.y > max_Y ){
			//GetComponent<Rigidbody2D>().isKinematic = true;
			transform.position = new Vector2(transform.position.x, -max_Y);
		
		}
		if(transform.position.y < -max_Y){
			//GetComponent<Rigidbody2D>().isKinematic = true;
			transform.position = new Vector2(transform.position.x, max_Y);
		}
		transform.localScale = Vector3.one * size;
	}

	public void Split() {
		
	}

	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy()
	{
		if(!ApplicationClosing && this.tag == "Small"){
			int newAmount = GetComponentInParent<AsteroidManager>().getSmallAsteroids() - 1;
			GetComponentInParent<AsteroidManager>().setSmallAsteroids(newAmount);
		}
		if(!ApplicationClosing && this.tag == "Medium"){
			int newAmount = GetComponentInParent<AsteroidManager>().getMedAsteroids() - 1;
			GetComponentInParent<AsteroidManager>().setMedAsteroids(newAmount);
		}
		if(!ApplicationClosing && this.tag == "Large"){
			int newAmount = GetComponentInParent<AsteroidManager>().getLargeAsteroids() - 1;
			GetComponentInParent<AsteroidManager>().setLargeAsteroids(newAmount);
		}
		// if(GetComponentInParent<AsteroidManager>()){
		// 	GetComponentInParent<AsteroidManager>().Spawn(1);
		// }
	}

	/// <summary>
	/// Callback sent to all game objects before the application is quit.
	/// </summary>
	void OnApplicationQuit()
	{
		ApplicationClosing = true;
	}
}
