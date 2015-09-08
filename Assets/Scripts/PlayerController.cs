using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int speed;
	public Text counterText;
	public GameObject pickup;
	
	private Rigidbody rb;
	private int score = 0;
	private int totalPickups;


	// Use this for initialization
	void Start () {
		this.totalPickups = 10;
		this.rb = GetComponent<Rigidbody> ();
		this.UpdateScoreText ();
		this.RenderPickups ();

	}


	// FixedUpdate is used to update frame on physic objects, and is called once per frame
	void FixedUpdate () {
		float horizontalPosition = Input.GetAxis ("Horizontal");
		float verticalPosition   = Input.GetAxis ("Vertical");

		Vector3 position = new Vector3 (horizontalPosition, 0.0f, verticalPosition);

		this.rb.AddForce (position * speed);
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("PickUp")) {
			Destroy(other.gameObject);
			
			score++;
			this.UpdateScoreText();

			if(this.score == this.totalPickups) {
				this.RestartGame();
			}
		}

	}


	void UpdateScoreText() {
		this.counterText.text = "Score: " + score.ToString ();
	}


	void RenderPickups() {
		int pickupRadius = 4;
		int pickupSpace  = 360 / this.totalPickups;

		float pickupY = 0.6f;
		
		
		for(int i = 0; i < this.totalPickups; i++) {
			Instantiate(
				this.pickup, 
				new Vector3(
					Mathf.Round(pickupRadius*Mathf.Cos(pickupSpace*i*Mathf.PI/180)), 
					pickupY, 
					Mathf.Round(pickupRadius*Mathf.Sin(pickupSpace*i*Mathf.PI/180)) 
				), 
				Quaternion.identity
			);
		}
	}


	void RestartGame() {
		this.RenderPickups ();

		transform.position = new Vector3 (0.0f, 0.5f, 0.0f);

		this.score = 0;
		this.UpdateScoreText ();
	}	
}
