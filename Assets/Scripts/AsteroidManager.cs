using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

	public GameObject[] AsteroidPrefabs;
	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Spawn(){
		Instantiate(AsteroidPrefabs[Random.Range(0,1)], this.transform, Quaternion.identity);
	}

	
}
