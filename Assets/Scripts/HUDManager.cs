using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
    [SerializeField] GameObject m_ShipSelectPanel;
	[SerializeField] Image[] m_healthIcons;
	[SerializeField] Text m_kills;
	Color m_hitColor = Color.red;
	Color m_healthColor = Color.green;

	void Start() {
		UpdateHealthIcons(m_healthIcons.Length);
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
}
