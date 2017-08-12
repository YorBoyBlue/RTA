using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
    [SerializeField] GameObject m_ShipSelectPanel;
	[SerializeField] Image[] m_healthIcons;
	[SerializeField] Image[] m_shieldIcons;
	[SerializeField] Text m_kills;
	[SerializeField] Text m_gameTimer;
	Color m_hitColor = Color.black;
	Color m_healthColor = Color.magenta;
	Color m_shieldColor = Color.cyan;

	public Text GameTimer { get { return m_gameTimer;} set { m_gameTimer = value; }}

	void Start() {
		UpdateHealthIcons(m_healthIcons.Length);
		UpdateShieldIcons(0);
	}

	public void UpdateHealthIcons(int health) {
		for(int i = 0; i < m_healthIcons.Length; i++) {
			if(i < health) {
				m_healthIcons[i].color = m_healthColor;
			} else {
				m_healthIcons[i].color = m_hitColor;
			}
		}
	}

	public void UpdateShieldIcons(int shield) {
		for(int i = 0; i < m_shieldIcons.Length; i++) {
			if(i < shield) {
				m_shieldIcons[i].color = m_shieldColor;
			} else {
				m_shieldIcons[i].color = m_hitColor;
			}
		}
	}

	public void SetKills(int value) {
		m_kills.text = value.ToString();
	}
	
	public void SetShip1() {
    	m_ShipSelectPanel.SetActive(false);	
	}
	public void SetShip2() {
    	m_ShipSelectPanel.SetActive(false);		
	}
	public void SetShip3() {
    	m_ShipSelectPanel.SetActive(false);		
	}
	public void SetShip4() {
    	m_ShipSelectPanel.SetActive(false);		
	}

	public void SetTimer(int currentSeconds) {
		float seconds = currentSeconds % 60;
		float minutes = (currentSeconds - seconds) / 60;
		m_gameTimer.text = ((int)minutes).ToString() + ":" + ((int)seconds).ToString();
		//Debug.Log(((int)minutes).ToString() + ":" + ((int)seconds).ToString());
	}
}
