using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class AsteroidManager : NetworkBehaviour {

	public static AsteroidManager singleton = null;
	public MapBoundary boundary;

	public float boundary_X { get { return boundary.X; } }
	public float boundary_Y { get { return boundary.Y; } }
	
	private int smallAsteroids = 0;
	private int medAsteroids = 0;
	private int largeAsteroids = 0;

	int asteroidsTotal = 0;
	[SerializeField] int asteroidsMax;

	Vector2 asteroidVelocity = new Vector2(0, 0);
	public Vector3[] AsteroidSpawn;


	public GameObject[] AsteroidPrefabs;
	
	public override void OnStartServer() {
		for (int i = 0; i < 10; i++) {			
			int randomSize = Random.Range(0,AsteroidPrefabs.Length);
			Spawn(randomSize, new Vector3(Random.Range(-boundary_X, boundary_X), Random.Range(-boundary_Y, boundary_Y), 0));
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
		asteroidsTotal = smallAsteroids + medAsteroids + largeAsteroids;

		if(asteroidsTotal < asteroidsMax){

			MaintainAstroids();
		}
		
		//Debug.Log("Large Asteroids: " + largeAsteroids + " Med Asteroids: " + medAsteroids + " Small Asteroids: " + smallAsteroids + " TOTAL== " + totalAsteroids);
	}



	[Server]
	private void MaintainAstroids(){		
		int randomSize = Random.Range(0,AsteroidPrefabs.Length);
		Spawn(2 , new Vector3(Random.Range(-boundary_X, boundary_X), Random.Range(-boundary_Y, boundary_Y), 0));	
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
		return asteroidsTotal;
	}

	[Server]
	public Vector2 GetBoundary() {
		Vector2 boundary = new Vector2(boundary_X, boundary_Y);
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
		asteroid.GetComponent<Asteroid>().max_X = boundary_X;
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
