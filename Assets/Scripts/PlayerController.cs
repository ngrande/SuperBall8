using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

public class PlayerController : MonoBehaviour {
	
	public float speed = 10;
	public Text countText;
	public Text popupText;
	public float pickupBoost = 2;
	
	private Rigidbody rb;
	private int count;
	private Stopwatch watch = new Stopwatch();
	private bool loose = false;
	private bool gameOver = false;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		count = 0;
		EvaluateProgress();
		popupText.text = string.Empty;

		watch.Start ();
	}
	
	void FixedUpdate() {
		if (!gameOver) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (movement * speed);
			transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		}
		
		EvaluateProgress();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pickup")) {
			other.gameObject.SetActive (false);
			count++;
			speed += pickupBoost;
			EvaluateProgress ();
		} 
	}
	
	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.CompareTag ("Enemy")) {
			loose = true;
		}
	}
	
	void EvaluateProgress()
	{
		countText.text = string.Format ("{0} :: {1}", "Picked up: " + count.ToString (), "Time: " + watch.Elapsed);
		if (loose || count >= 8) {
			gameOver = true;
		}

		if (loose) {
			watch.Stop ();
			popupText.text = "You loose! Press <ENTER> to restart";
		}else if (count >= 8) {
			watch.Stop ();
			popupText.text = "You win!";
		}

		if (gameOver) {
			// Restart the level (generate new map, etc).
			if (Input.GetKeyDown(KeyCode.Return)) {
				Application.LoadLevel("SuperBall8_3D");
			}
		}
	}
}
