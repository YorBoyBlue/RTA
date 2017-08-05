﻿using System.Collections;
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
				AsteroidManager m_AsteroidManager = GetComponentInParent<AsteroidManager>();
				
				// int newLargeAmount = m_AsteroidManager.getLargeAsteroids() - 1;
				// m_AsteroidManager.setLargeAsteroids(newLargeAmount);
				if(GetComponentInParent<AsteroidManager>().getTotalAsteroids() < 10){
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
				if(GetComponentInParent<AsteroidManager>().getTotalAsteroids() < 10){
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
		if(this.tag == "Small"){
			int newAmount = GetComponentInParent<AsteroidManager>().getSmallAsteroids() - 1;
			GetComponentInParent<AsteroidManager>().setSmallAsteroids(newAmount);
		}
		if(this.tag == "Medium"){
			int newAmount = GetComponentInParent<AsteroidManager>().getMedAsteroids() - 1;
			GetComponentInParent<AsteroidManager>().setMedAsteroids(newAmount);
		}
		if(this.tag == "Large"){
			int newAmount = GetComponentInParent<AsteroidManager>().getLargeAsteroids() - 1;
			GetComponentInParent<AsteroidManager>().setLargeAsteroids(newAmount);
		}
		// if(GetComponentInParent<AsteroidManager>()){
		// 	GetComponentInParent<AsteroidManager>().Spawn(1);
		// }
	}
}
