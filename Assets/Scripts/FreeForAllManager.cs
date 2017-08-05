using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FreeForAllManager : NetworkBehaviour {

	float m_warmUpTime = 5f;

	bool m_hasStartedGame = false;
	[SerializeField] List<GameObject> m_players;

	[ServerCallback]
	void Start() {
		
	}

	[Server]
	void Update() {
		if(m_warmUpTime > 0) {
			m_warmUpTime -= Time.deltaTime;
		} else {
			if(!m_hasStartedGame) {
				m_hasStartedGame = true;
				GetPlayers();
				StartGame();
			}
		}
	}

	[Server]
	void GetPlayers() {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject g in players) {
			m_players.Add(g);
		}
	}

	[Server]
	void StartGame() {
		foreach(GameObject g in m_players) {
			g.GetComponent<PlayerManager>().RpcSetCanMove(true);
		}
	}
}
