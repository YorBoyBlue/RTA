using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour {
    [SerializeField] GameObject m_ShipSelectPanel;
	
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
