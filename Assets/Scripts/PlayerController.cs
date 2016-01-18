﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	public Text countText;
	public Text winText;
	
	private Rigidbody rb;
	private int count;
	private Stopwatch watch = new Stopwatch();
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		count = 0;
		UpdateCountTxt();
		winText.text = string.Empty;

		watch.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);
		
		UpdateCountTxt();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pickup")) {
			other.gameObject.SetActive(false);
			count++;
			UpdateCountTxt();
		}
	}
	
	void UpdateCountTxt()
	{
		countText.text = string.Format ("{0} :: {1}", "Picked up: " + count.ToString (), "Time: " + watch.Elapsed);
		if (count >= 8) {
			watch.Stop ();
			winText.text = "You win the SuperBall8 game!";
		}
	}
}
