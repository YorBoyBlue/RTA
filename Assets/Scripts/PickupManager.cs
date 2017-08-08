using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PickupManager : NetworkBehaviour {
	//	SINGLETON ACCESSOR
	public static PickupManager Instance { get; private set; }

	public MapBoundary boundary;

	/*
	Prefabs bruh...
	 */
	public GameObject[] pickupPrefabs;

	int pickupsTotal;
	[SerializeField] int pickupsMax;

	public void DecrementPickupsTotal() { pickupsTotal --; }

	[ServerCallback]
	void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
	}

	[Server] 
	void Update() {
		if (pickupsTotal < pickupsMax) {
			MaintainPickupCount();
		}
	}

	[Server]
	void MaintainPickupCount() {
		Spawn(
			Random.Range(0, pickupPrefabs.Length),
			new Vector3(Random.Range(-boundary.X, boundary.X), Random.Range(-boundary.Y, boundary.Y))
		);
	}

	[Server]
	void Spawn(int pickupType, Vector3 location) {
		pickupsTotal ++;
		GameObject objectInstance = Instantiate(pickupPrefabs[pickupType], location, Quaternion.identity);
		NetworkServer.Spawn(objectInstance);
	}
}
