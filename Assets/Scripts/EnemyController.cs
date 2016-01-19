using UnityEngine;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour {

	public int speed = 10;

	Rigidbody rb;
	System.Random rnd = new System.Random();

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate () {
		rb.AddRelativeForce (new Vector3(rnd.Next (-5, 5) * speed, 0, rnd.Next (-5, 5) * speed));
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
	}

	void OnCollisionEnter (Collision collision) {

	}
}
