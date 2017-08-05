using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {

	void Start() {
		GameManager.m_singleton.IsFromLobby = true;
	}
}
