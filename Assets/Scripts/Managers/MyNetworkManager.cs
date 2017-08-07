using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {
	void Start() {
		if(GameManager.m_singleton.IsFromLobby) {
			this.gameObject.SetActive(false);
		} else {
			this.StartHost();
		}
	}
}
