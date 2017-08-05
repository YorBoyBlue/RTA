using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidManager : MonoBehaviour {




	public GameObject[] AsteroidPrefabs;
	// Use this for initialization
	void Start () {
		Spawn(2, this.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
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
