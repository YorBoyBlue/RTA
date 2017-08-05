using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	
	public GameObject m_player;
	public float m_deadSpace;
	public float m_speed;
	private bool m_follow;

	public GameObject SetPlayer {set { m_player = value; }}

	void Update() {
		if(Mathf.Abs(transform.position.x - m_player.transform.position.x) >  m_deadSpace && Mathf.Abs(transform.position.x - m_player.transform.position.x) <  (m_deadSpace + 0.5f) || Mathf.Abs(transform.position.y - m_player.transform.position.y) >  m_deadSpace && Mathf.Abs(transform.position.x - m_player.transform.position.x) <  (m_deadSpace + 0.5f) ) {
			m_follow = true;			
		} else if(Mathf.Abs(transform.position.x - m_player.transform.position.x) >  (m_deadSpace + 0.5f) || Mathf.Abs(transform.position.y - m_player.transform.position.y) >  (m_deadSpace + 0.5f)) {
			//transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y, -70);
			m_follow = true;			
		} else {
			m_follow = false;
		}
	}

	void FixedUpdate() {
		if(m_follow) {
			Vector3 target = new Vector3(m_player.transform.position.x, m_player.transform.position.y, -70);
			Vector3 currentPos = new Vector3(transform.position.x, transform.position.y, -70);
			this.transform.position = Vector3.MoveTowards(currentPos, target, (m_speed * Time.fixedDeltaTime));
		}
	}
}
