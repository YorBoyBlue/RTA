using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

[System.Serializable] public class ToggleEvent : UnityEvent<bool>{}

public class PlayerManager : NetworkBehaviour {

    [SerializeField] ToggleEvent m_toggleLocal;
    [SerializeField] ToggleEvent m_toggleShared;
    [SerializeField] ToggleEvent m_toggleRemote;

    bool m_isLocalPlayer = false;

    public bool GetLocalPlayer { get { return m_isLocalPlayer; }}


	
    void Start() {
        if(isLocalPlayer) {
            m_isLocalPlayer = true;
            Camera.main.GetComponent<CameraFollow>().SetPlayer = this.gameObject;
        } else {
            Camera.main.gameObject.SetActive(false);
        }
    }
}
