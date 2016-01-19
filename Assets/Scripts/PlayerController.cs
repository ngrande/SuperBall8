using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	public Text countText;
	public Text popupText;
	
	private Rigidbody rb;
	private int count;
	private Stopwatch watch = new Stopwatch();
	private bool loose = false;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		count = 0;
		UpdateCountTxt();
		popupText.text = string.Empty;

		watch.Start ();
	}
	
	void FixedUpdate() {
		if (!loose) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (movement * speed);
			transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		}
		
		UpdateCountTxt();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pickup")) {
			other.gameObject.SetActive (false);
			count++;
			UpdateCountTxt ();
		} 
	}
	
	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.CompareTag ("Enemy")) {
			loose = true;
		}
	}
	
	void UpdateCountTxt()
	{
		countText.text = string.Format ("{0} :: {1}", "Picked up: " + count.ToString (), "Time: " + watch.Elapsed);
		if (loose) {
			watch.Stop ();
			popupText.text = "You loose!";
		}else if (count >= 8) {
			watch.Stop ();
			popupText.text = "You win!";
		}
	}
}
