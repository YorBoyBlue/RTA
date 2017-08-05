using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidManager : MonoBehaviour {


	private int smallAsteroids = 0;
	private int medAsteroids = 0;
	private int largeAsteroids = 0;
	
	public Vector3[] AsteroidSpawn;


	public GameObject[] AsteroidPrefabs;
	// Use this for initialization
	void Start () {
		smallAsteroids = 0;
		medAsteroids = 0;
		largeAsteroids = 0;
		for (int i = 0; i < 15; i++){
			
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
		Debug.Log("Large Asteroids: " + largeAsteroids + " Med Asteroids: " + medAsteroids + " Small Asteroids: " + smallAsteroids);
	}
	
	// Update is called once per frame
	void Update () {
		MaintainAstroids();
	}

	private void MaintainAstroids(){
		int totalAsteroids = smallAsteroids + medAsteroids + largeAsteroids;
		if(totalAsteroids < 15){
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
		smallAsteroids = newAmount;
	}

	public void setMedAsteroids(int newAmount){
		medAsteroids = newAmount;
	}

	public void setLargeAsteroids(int newAmount){
		largeAsteroids = newAmount;
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
