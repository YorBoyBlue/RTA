using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidManager : MonoBehaviour {




	public GameObject[] AsteroidPrefabs;
	// Use this for initialization
	void Start () {
		Spawn("Large");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void Spawn(AsteroidSize size){
		GameObject asteroid = Instantiate(AsteroidPrefabs[Random.Range(0,2)], this.transform.position, Quaternion.identity);
		asteroid.transform.SetParent(this.transform);
		
		switch(size){
			
			case "Small":
				
				asteroid.transform.localScale = Vector3.one * 0.75f;
				
				break;

			case "Medium":
				
				asteroid.transform.localScale = Vector3.one * 0.5f;
				
				break;
			
			case "Large":
				
				asteroid.transform.localScale = Vector3.one * 1f;
				
				break;


			default:
				break;
		}
		
		
	}

	
}
