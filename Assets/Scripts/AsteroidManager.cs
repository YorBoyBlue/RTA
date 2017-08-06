using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class AsteroidManager : NetworkBehaviour {

	public static AsteroidManager singleton = null;

	public float bounday_X;
	public float boundary_Y;
	private int smallAsteroids = 0;
	private int medAsteroids = 0;
	private int largeAsteroids = 0;
	private int totalAsteroids = 0;
	private int totalPickups = 0;

	Vector2 asteroidVelocity = new Vector2(0, 0);
	public Vector3[] AsteroidSpawn;

	public GameObject[] PickupPrefabs;


	public GameObject[] AsteroidPrefabs;
	
	public override void OnStartServer() {
		for (int i = 0; i < 10; i++) {			
			int randomSize = Random.Range(0,AsteroidPrefabs.Length);
			Spawn(randomSize, new Vector3(Random.Range(-bounday_X, bounday_X), Random.Range(-boundary_Y, boundary_Y), 0));
			if(randomSize == 0){
				smallAsteroids += 1;
			} 
			if(randomSize == 1){
				medAsteroids += 1;
			}
			if(randomSize == 2){
				largeAsteroids += 1;
			}			
		}		
	}
	
	[ServerCallback]
	void Awake()
	{
		if(singleton == null){
			singleton = this;
		}else if(singleton != this){
			Destroy(this.gameObject);
		}
	}

	[Server]
	void Update () {
		totalAsteroids = smallAsteroids + medAsteroids + largeAsteroids;

		if(totalAsteroids < 5){

			MaintainAstroids();
		}

		if (totalPickups <= 15) {
			MaintainPickups();
		}
		
		//Debug.Log("Large Asteroids: " + largeAsteroids + " Med Asteroids: " + medAsteroids + " Small Asteroids: " + smallAsteroids + " TOTAL== " + totalAsteroids);
	}
	public void DecrementTotalPickups() { totalPickups -= 1; }

	[Server]
	void MaintainPickups() {
		Vector2 randomSpot = new Vector2(Random.Range(-bounday_X, bounday_X), Random.Range(-boundary_Y, boundary_Y));
		GameObject pickup = Instantiate(PickupPrefabs[Random.Range(0, PickupPrefabs.Length)], randomSpot, Quaternion.identity);		
		totalPickups += 1;
		// Debug.Log(location);
		//asteroid.transform.SetParent(this.transform);
		NetworkServer.Spawn(pickup);
	}



	[Server]
	private void MaintainAstroids(){		
		int randomSize = Random.Range(0,AsteroidPrefabs.Length);
		Spawn(2 , new Vector3(Random.Range(-bounday_X, bounday_X), Random.Range(-boundary_Y, boundary_Y), 0));	
	}
	
	[Server]
	public int getSmallAsteroids(){
		return smallAsteroids;
	}
	[Server]
	public int getMedAsteroids(){
		return medAsteroids;
	}
	[Server]
	public int getLargeAsteroids(){
		return largeAsteroids;
	}

	[Server]
	public void setSmallAsteroids(int newAmount){
		smallAsteroids -= newAmount;
	}

	[Server]
	public void setMedAsteroids(int newAmount){
		medAsteroids = newAmount;
	}

	[Server]
	public void setLargeAsteroids(int newAmount){
		largeAsteroids = newAmount;
	}

	[Server]
	public int GetTotalAstroids(){
		return totalAsteroids;
	}

	[Server]
	public Vector2 GetBoundary() {
		Vector2 boundary = new Vector2(bounday_X, boundary_Y);
		return boundary;
	}	

	[Server]
	public void Spawn(int size, Vector3 location, float x = 0, float y = 0) {
		Vector2 velocity = new Vector2(x, y);
		switch(size) {
			case 0:
				smallAsteroids += 1;
				break;
			case 1:
				medAsteroids += 1;
				break;
			case 2:
				largeAsteroids += 1;
				break;
			default:
				break;

		}
		GameObject asteroid = Instantiate(AsteroidPrefabs[size], location, Quaternion.identity);		
		// Debug.Log(location);
		//asteroid.transform.SetParent(this.transform);
		asteroid.GetComponent<Asteroid>().max_X = bounday_X;
		asteroid.GetComponent<Asteroid>().max_Y = boundary_Y;
		if(velocity == Vector2.zero) {
			asteroidVelocity = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
			asteroid.GetComponent<Rigidbody2D>().velocity = asteroidVelocity;
		} else {
			asteroid.GetComponent<Rigidbody2D>().velocity = velocity;
		}
		NetworkServer.Spawn(asteroid);
	}
}
