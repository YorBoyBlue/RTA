using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

<<<<<<< HEAD
	public bool CanMove { get; set; }
=======
	public int max_X = 30;
	public int max_Y = 30;

	public bool CanMove { get; private set; }
>>>>>>> tariq

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

<<<<<<< HEAD
=======
	 [SerializeField]
	 GameObject bullet;

	public Vector2 shootCooldown;
	

>>>>>>> tariq
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

		 if(transform.position.x > max_X ){
			transform.position = new Vector3(-max_X, transform.position.y, 0);
		}else if(transform.position.x < -max_X){
			transform.position = new Vector3(max_X, transform.position.y, 0);
		}
		if(transform.position.y > max_Y ){
			transform.position = new Vector3(transform.position.x, -max_Y, 0);
		}else if(transform.position.y < -max_Y){
			transform.position = new Vector3(transform.position.x, -max_Y, 0);
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

	void MoveShip() {
		
	}

	//	Receive player inputs
	void ReadyValues() {
		float v = Input.GetAxisRaw("Jump");
		if (v > 0) {
			float speed = v * thrust_speed * Time.deltaTime;
			
			ship_velocity = (Vector2)(thisTransform.forward * speed) + rb2d.velocity;
		}
	}

	void ApplyValues() {
		// rb2d.AddForce(thisTransform.forward * thrust_speedometer.x);
		rb2d.velocity = ship_velocity;
		// rb2d.rotation = thisTransform.eulerAngles.x;
	}
}
