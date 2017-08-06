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

    [SerializeField] PlayerControl m_playerControl;

    float m_respawnDelay = 5f;

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

    [ClientRpc]
    public void RpcSetCanMove(bool value) {
        if(isLocalPlayer) {
            m_playerControl.CanMove = value;
        }
    }

    [ClientRpc]
    public void RpcSetBounds(float x, float y) {
        if(isLocalPlayer) {
            m_playerControl.max_X = x;
            m_playerControl.max_Y = y;
        }
    }

    public void GameOver() {
        DeactivatePlayer();
        Invoke("Respawn", m_respawnDelay);
    }

    void ActivatePlayer() {
        if(isLocalPlayer) {
            m_toggleLocal.Invoke(true);
        }
        m_toggleShared.Invoke(true);
    }

    void DeactivatePlayer() {        
        if(isLocalPlayer) {
            m_toggleLocal.Invoke(false);
        }
        m_toggleShared.Invoke(false);
    }

    void Respawn() {
        if(isLocalPlayer) {
            Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
            this.transform.position = spawnPoint.position;
            this.transform.rotation = spawnPoint.rotation;
        }
        ActivatePlayer();
    }
}
