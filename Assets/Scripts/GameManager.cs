using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager m_singleton = null;

	bool m_fromLobby = false;

	public bool IsFromLobby { get { return m_fromLobby; } set { m_fromLobby = value; }}

	void Awake() {
		if(m_singleton == null) {
			m_singleton = this;
		} else if(m_singleton != this) {
			Destroy(this.gameObject);
		}
	}
}
