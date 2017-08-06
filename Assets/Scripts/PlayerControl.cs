using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public bool CanMove { get; set; }

	public PlayerUpgrades upgrades;

	public float max_speed;
	/*
	The base speed the ship will be using while thrusting
	 */
	public float thrust_speed;
	/*
	VECTOR2 = Ship Velocity
	*/
	public Vector2 ship_velocity;

	/*
	The base speed the ship will be using while rotating
	 */
	public float currentRotation;

	/*
	Reference to the attached rigidbody2D
	 */
	public Rigidbody2D rb2d;

	/*
	We need to cache the transform of this object
	 */
	 public Transform thisTransform;

	// Use this for initialization
	void Start () {
		CanMove = true;
		/*
		speed = 10f;
		max_horizontalVelocity = 10f;
		max_verticalVelocity = 10f;
		 */
		 thisTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (CanMove) {
			RotateShip();

			ReadyValues();
			ApplyValues();
		}
	}

	/*
	Rotates the ship's direction using the mouse position
	 */
	void RotateShip() {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
		Vector3 newMousePos = Camera.main.ScreenToWorldPoint(mousePos);
		// Debug.Log(newMousePos);
		thisTransform.LookAt(newMousePos, Vector3.back);
		// rotation_velocity.x = Vector2.Angle(thisTransform.position, newMousePos);
		// Debug.Log(rotation_velocity.x);
	}

	//	Receive player inputs
	void ReadyValues() {
		float v = Input.GetAxisRaw("Jump");
		if (v > 0) {
			float speed = v * thrust_speed * Time.deltaTime;
			
			ship_velocity = (Vector2)(thisTransform.forward * speed) + rb2d.velocity;

			if (Vector2.SqrMagnitude(ship_velocity) > Mathf.Pow(max_speed, 2)) {
				ship_velocity = Vector2.ClampMagnitude(ship_velocity, max_speed);
			}
		}
	}

	void ApplyValues() {
		// rb2d.AddForce(thisTransform.forward * thrust_speedometer.x);
		rb2d.velocity = ship_velocity;
		// rb2d.rotation = thisTransform.eulerAngles.x;
	}
}
