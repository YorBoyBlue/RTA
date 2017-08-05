using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FreeForAllManager : NetworkBehaviour {

	float m_warmUpTime = 20f;

	[ServerCallback]
	void Start() {
		
	}
}
