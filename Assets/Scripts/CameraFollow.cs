using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	Transform thisTransform;
	Transform target = null;
	public float speed;
	public float deadSpace;

	public Transform SetTargetTransform { set { target = value; }}
	bool TargetIsValid { get { return target != null; } }

	void Start() {
		thisTransform = transform;
	}

	void FixedUpdate() {
		if (!TargetIsValid) {
			return;
		}
		float halfDead = deadSpace * 0.5f;
		Rect deadSquare = new Rect(thisTransform.position.x - halfDead, thisTransform.position.y - halfDead, deadSpace, halfDead);
		
		if (!deadSquare.Contains((Vector2)target.position) ) {
			Vector2 tempPos = Vector2.Lerp(thisTransform.position, target.position, (Vector2.Distance(thisTransform.position, target.position) / (deadSpace * 5f)) * Time.fixedDeltaTime);
			thisTransform.position = new Vector3(tempPos.x, tempPos.y, thisTransform.position.z);
		}
	}
	
	// public GameObject m_player;
	// public float m_deadSpace;
	// public float m_speed;
	// private bool m_follow;

	// public GameObject SetPlayer {set { m_player = value; }}

	// void Update() {
	// 	if(Mathf.Abs(transform.position.x - m_player.transform.position.x) >  m_deadSpace && Mathf.Abs(transform.position.x - m_player.transform.position.x) <  (m_deadSpace + 0.5f) || Mathf.Abs(transform.position.y - m_player.transform.position.y) >  m_deadSpace && Mathf.Abs(transform.position.x - m_player.transform.position.x) <  (m_deadSpace + 0.5f) ) {
	// 		m_follow = true;			
	// 	} else if(Mathf.Abs(transform.position.x - m_player.transform.position.x) >  (m_deadSpace + 0.5f) || Mathf.Abs(transform.position.y - m_player.transform.position.y) >  (m_deadSpace + 0.5f)) {
	// 		//transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y, -70);
	// 		m_follow = true;
	// 	} else {
	// 		m_follow = false;
	// 	}
	// }

	// void LateUpdate() {
	// 	if(m_follow) {
	// 		Vector3 target = new Vector3(m_player.transform.position.x, m_player.transform.position.y, -70);
	// 		Vector3 currentPos = new Vector3(transform.position.x, transform.position.y, -70);
	// 		this.transform.position = Vector3.MoveTowards(currentPos, target, (m_speed * Time.fixedDeltaTime));
	// 	}
	// }
}
