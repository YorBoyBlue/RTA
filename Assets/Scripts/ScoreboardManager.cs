using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;


public class ScoreboardManager : NetworkBehaviour {

	public static ScoreboardManager m_singleton = null;
	[SerializeField] GameObject m_scoreboardPanel;

	public GameObject ScoreBoard { get { return m_scoreboardPanel; }}

	void Awake() {
		if(m_singleton == null) {
			m_singleton = this;
		} else if(m_singleton != this) {
			Destroy(this.gameObject);
		}
	}

	[ClientRpc]
	public void RpcOpenScoreBoard() {
		m_scoreboardPanel.SetActive(true);
	}	
}
