using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {
	[SerializeField] PlayerManager m_playerManager;
	[SerializeField] HUDManager m_hud;
	[SerializeField] int m_maxHealth;
	bool m_canTakeDamage = true;
	[SyncVar (hook = "OnHealthChanged")] int m_health;
	[SyncVar (hook = "OnShieldChanged")] int m_shield;
	[SyncVar (hook = "OnDeathsChanged")] int m_deaths;
    [SyncVar (hook = "OnKillsChanged")] int m_kills;

	public int Kills { get { return m_kills;}set { m_kills = value; }}
	public int Deaths { get { return m_deaths;}set { m_deaths = value; }}
	public HUDManager HUD { get { return m_hud; } }

	[ServerCallback]
	void OnEnable() {
		m_health = m_maxHealth;
	}
	
	[ServerCallback]
	void Start() {
		m_health = m_maxHealth;
		m_deaths = 0;
	}
	
	[Command]
	void CmdTakeDamage(bool died) {
		RpcTakeDamage(died);
	}
	
	[Server]
	public bool TakeDamage() {
		bool died = false;
		if (GetComponent<PlayerUpgrades>().HasShieldUpgrade) {
			died = GetComponent<PlayerUpgrades>().ConsumeUpgrade(PickupType.Shield);
			m_shield--;
		} else  if(m_canTakeDamage) {
			if(m_health <= 0) {
				m_deaths++;
				died = true;
			} else {
				m_health--;
				died = m_health <= 0;
				if(died) {
					m_deaths++;
				}
				RpcTakeDamage(died);
			}
		}
		return died;
	}

	[ClientRpc]
	void RpcTakeDamage(bool died) {
		if(died) {
			AudioManager.Instance.PlayOneShot(GetComponent<AudioSource>(), AudioClip_Enum.EXPLOSION_Alien, 0.1f);
			m_playerManager.GameOver();
		}
	}

	void OnHealthChanged(int value) {
		m_health = value;
		if(isLocalPlayer) {
			m_hud.UpdateHealthIcons(m_health);
		}
	}

	void OnShieldChanged(int value) {
		m_shield = value;
		if (isLocalPlayer) {
			m_hud.UpdateShieldIcons(m_shield);
		}
	}

	void OnDeathsChanged(int value) {
		m_deaths = value;
	}

    void OnKillsChanged(int value) {
        m_kills = value;
        if (isLocalPlayer)
            m_hud.SetKills(value);
    }

    [ClientRpc]
    public void RpcSetTimer(int currentSeconds) {
        m_hud.SetTimer(currentSeconds);
    }
}
