using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

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
		/*
		speed = 10f;
		max_horizontalVelocity = 10f;
		max_verticalVelocity = 10f;
		 */
		 thrust_velocity = Vector2.one;
		 thisTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		RotateShip();
		Shoot();

		ReadyValues();
		ApplyValues();
		 if (Input.GetAxis("Jump") > 0){
			 for(int i=1; i < 2; i++){	
				 transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
				 transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
			 }
		 }else{
			 for(int i=1; i < 2; i++){	
				 transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();
				 transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
			 }
		 }
		 
	}

	/*
	Rotates the ship's direction using the mouse position
	 */
	void RotateShip() {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 10.0f;
		Vector3 newMousePos = Camera.main.ScreenToWorldPoint(mousePos);
		// Debug.Log(newMousePos);
		// thisTransform.Rotate()
		thisTransform.LookAt(newMousePos, Vector3.back);
	}

	//	Receive player inputs
	void ReadyValues() {
		float v = Input.GetAxis("Jump");

		thrust_velocity.x = v * thrust_speed;

	}

	void ApplyValues() {
		rb2d.AddRelativeForce(new Vector2(0, thrust_velocity.x));
	}

	void Shoot() {
		if (shootCooldown.x > 0) {
			shootCooldown.x -= Time.deltaTime;
		} else {
			if (Input.GetAxis("Fire1") > 0) {
				shootCooldown.x = shootCooldown.y;
				GameObject newBullet = Instantiate(bullet, thisTransform.position, thisTransform.rotation);
				newBullet.transform.rotation = thisTransform.rotation;
				newBullet.GetComponent<Bullet>().speed =  1000f;
				Debug.Log(shootCooldown.x);
				Debug.Log(shootCooldown.y);
			}
		}
	}
}
