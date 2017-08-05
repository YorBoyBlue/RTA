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

    Camera m_mainCamera;

    public bool GetLocalPlayer { get { return isLocalPlayer; }}
	
    void Start() {
        if(isLocalPlayer) {
            Camera.main.GetComponent<CameraFollow>().SetPlayer = this.gameObject;
            ActivatePlayer();
        } else if(!isLocalPlayer) {
            m_toggleRemote.Invoke(false);
        }
    }

    void ActivatePlayer() {
        if(isLocalPlayer) {

        }
    }
}
