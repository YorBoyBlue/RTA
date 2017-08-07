using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FreeForAllManager : NetworkBehaviour {

	float m_warmUpTime = 3f;
	float m_gameTime = 2 * 60; // 5 Minutes
	[SyncVar (hook = "OnSecondChanged")] int m_currentGameTime;
	float m_gameTimer;
	bool m_hasStartedGame = false;
	bool m_hasDisplayedScore = false;
	[SerializeField] List<GameObject> m_players;

	[ServerCallback]
	void Start() {
		m_gameTimer = m_gameTime;
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
		if(m_hasStartedGame) {
			UpdateGameTimer();
		}
	}

	[Server]
	void UpdateGameTimer() {
		if (m_gameTimer > 0) {
			m_gameTimer -= Time.deltaTime;
			if(m_gameTimer != (int)m_gameTimer) {
				int tmpSecond = (int)m_gameTimer;
				if(tmpSecond != m_currentGameTime) {
					m_currentGameTime = (int)m_gameTimer;
				}
			}
		}else {
			if(!m_hasDisplayedScore) {
				ScoreboardManager.m_singleton.RpcOpenScoreBoard();
				foreach(GameObject g in m_players) {
					g.GetComponent<PlayerManager>().LoadPlayerScoreInfo();
				}
				m_hasDisplayedScore = true;
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
			PlayerManager pm = g.GetComponent<PlayerManager>();
			pm.RpcSetCanMove(true);
			pm.RpcSetBounds(AsteroidManager.singleton.GetBoundary().x, AsteroidManager.singleton.GetBoundary().y);
			//pm.RpcSetName();
		}
	}

	void OnSecondChanged(int currentSecond) {
		foreach(GameObject g in m_players) {
			g.GetComponent<PlayerHealth>().RpcSetTimer(currentSecond);
		}
	}
}
