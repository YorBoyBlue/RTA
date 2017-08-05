﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public bool CanMove { get; private set; }

	/*
	The base speed the ship will be using while thrusting
	 */
	public float thrust_speed;
	/*
	VECTOR2 = Thurst Velocity
	x - current speed
	y - max speed
	*/
	public Vector2 thrust_velocity;

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

	 [SerializeField]
	 GameObject bullet;

	public Vector2 shootCooldown;

	// Use this for initialization
	void Start () {
		CanMove = true;
		/*
		speed = 10f;
		max_horizontalVelocity = 10f;
		max_verticalVelocity = 10f;
		 */
		 thrust_velocity = Vector2.zero;
		 thisTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (CanMove) {
			RotateShip();
			Shoot();

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
		float v = Input.GetAxis("Jump");
		thrust_velocity.x = v * thrust_speed;

	}

	void ApplyValues() {
		rb2d.AddForce(thisTransform.forward * thrust_velocity.x);
		// rb2d.rotation = thisTransform.eulerAngles.x;
	}

	void Shoot() {
		if (shootCooldown.x > 0) {
			shootCooldown.x -= Time.deltaTime;
		} else {
			if (Input.GetAxis("Fire1") > 0) {
				shootCooldown.x = shootCooldown.y;
				GameObject newBullet = Instantiate(bullet, thisTransform.position, Quaternion.identity);
				newBullet.GetComponent<Bullet>().velocity =  thisTransform.forward * 10000f;
				newBullet.transform.GetChild(0).rotation = thisTransform.rotation;
			}
		}
	}
}
