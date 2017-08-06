﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {
	[SerializeField] PlayerManager m_playerManager;
	[SerializeField] HUDManager m_hud;
	[SerializeField] int m_maxHealth;
	bool m_canTakeDamage = true;
	[SyncVar (hook = "OnHealthChanged")] int m_health;
	[SyncVar (hook = "OnDeathsChanged")] int m_deaths;
    [SyncVar (hook = "OnKillsChanged")] int m_kills;

	public int Kills { get { return m_kills;}set { m_kills = value; }}
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
			m_hud.UpdateShieldIcons((int)GetComponent<PlayerUpgrades>().GetUpgrade(PickupType.Shield).value);
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
			m_playerManager.GameOver();
		}
	}

	void OnHealthChanged(int value) {
		m_health = value;
		if(isLocalPlayer) {
			m_hud.UpdateHealthIcons(m_health);
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
}
