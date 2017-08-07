using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable] public class ToggleEvent : UnityEvent<bool>{}

public class PlayerManager : NetworkBehaviour {

    [SerializeField] ToggleEvent m_toggleLocal;
    [SerializeField] ToggleEvent m_toggleShared;
    [SerializeField] ToggleEvent m_toggleRemote;

    [SerializeField] PlayerControl m_playerControl;
    [SerializeField] PlayerAudioManager m_playerAudioManager;
    [SerializeField] GameObject m_playerScoreInfoPrefab;

    [SerializeField] Text m_nameText;
    [SyncVar] public string m_name = "Player";

    float m_respawnDelay = 5f;

    Camera m_mainCamera;

    public bool GetLocalPlayer { get { return isLocalPlayer; }}
	
    void Start() {
        if(isLocalPlayer) {
            Camera.main.GetComponent<CameraFollow>().SetTargetTransform = transform;
            ActivatePlayer();
            AudioManager.Instance.PlayMusic(Camera.main.GetComponent<AudioSource>());
            m_nameText.text = m_name;
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

    public void LoadPlayerScoreInfo() {
        RpcLoadPlayerScoreInfo(m_name, GetComponent<PlayerHealth>().Kills, GetComponent<PlayerHealth>().Deaths);
    }

    [ClientRpc]
    public void RpcLoadPlayerScoreInfo(string name, int kills, int deaths) {
        GameObject myScoreInfo = Instantiate(m_playerScoreInfoPrefab, ScoreboardManager.m_singleton.ScoreBoard.transform);
        myScoreInfo.GetComponent<PlayerScoreInfo>().Name = name;
        myScoreInfo.GetComponent<PlayerScoreInfo>().Kills = kills.ToString();
        myScoreInfo.GetComponent<PlayerScoreInfo>().Deaths = deaths.ToString();
    }

    [ClientRpc]
    public void RpcSetName() {
        if(isLocalPlayer) {
            m_nameText.text = m_name;
        }
    }
}
