using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidManager : MonoBehaviour {

	public static AsteroidManager singleton = null;

	public float bounday_X;
	public float boundary_Y;
	private int smallAsteroids = 0;
	private int medAsteroids = 0;
	private int largeAsteroids = 0;
	private int totalAsteroids = 0;
	
	public Vector3[] AsteroidSpawn;


	public GameObject[] AsteroidPrefabs;
	// Use this for initialization
	void Start () {

		
		for (int i = 0; i < 10; i++){
			
			int randomSize = Random.Range(0,4);
			Spawn(randomSize, AsteroidSpawn[Random.Range(0,4)]);
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
	
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		if(singleton == null){
			singleton = this;
		}else if(singleton != this){
			Destroy(this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		totalAsteroids = smallAsteroids + medAsteroids + largeAsteroids;

		if(totalAsteroids < 10)
			MaintainAstroids();
		
		Debug.Log("Large Asteroids: " + largeAsteroids + " Med Asteroids: " + medAsteroids + " Small Asteroids: " + smallAsteroids + " TOTAL== " + totalAsteroids);
	}



	private void MaintainAstroids(){
		
		if(totalAsteroids < 10){
			int randomSize = Random.Range(0,4);
			Spawn(randomSize, AsteroidSpawn[Random.Range(0,4)]);
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

	public int getSmallAsteroids(){
		return smallAsteroids;
	}
	public int getMedAsteroids(){
		return medAsteroids;
	}
	public int getLargeAsteroids(){
		return largeAsteroids;
	}

	public void setSmallAsteroids(int newAmount){
		smallAsteroids -= newAmount;
	}

	public void setMedAsteroids(int newAmount){
		medAsteroids = newAmount;
	}

	public void setLargeAsteroids(int newAmount){
		largeAsteroids = newAmount;
	}

	public int GetTotalAstroids(){
		return totalAsteroids;
	}

	public Vector2 GetBoundary() {
		Vector2 boundary = new Vector2(bounday_X, boundary_Y);
		return boundary;
	}	

	public void Spawn(int size, Vector3 location){
		GameObject asteroid = null;

		switch(size){
			
			
			case 0:
				
				asteroid = Instantiate(AsteroidPrefabs[0], location, Quaternion.identity);
				asteroid.transform.SetParent(this.transform);
				break;

			case 1:
				
				asteroid = Instantiate(AsteroidPrefabs[1], location, Quaternion.identity);
				asteroid.transform.SetParent(this.transform);
				
				break;
			
			case 2:
				
				asteroid = Instantiate(AsteroidPrefabs[2], location, Quaternion.identity);
				asteroid.transform.SetParent(this.transform);
				
				break;


			default:
				break;
		}
		
		
	}

	
}
