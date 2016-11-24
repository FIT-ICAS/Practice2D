using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controler : NetworkBehaviour {
	private BoxCollider2D car;
	private Rigidbody2D rb;
	//private Transform thisTransform;
	// Use this for initialization
	void Start () {
		car = this.GetComponentInChildren<BoxCollider2D>();
		//thisTransform = this.transform;
		rb = this.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!isLocalPlayer)
		{
			return;
		}

		float v = Input.GetAxis ("Vertical");
		float h = Input.GetAxis ("Horizontal");
		//float mass = 2f;
		float engineF = 5000f;
		float rot = 10f;
		float drag = 0.5f * 0.30f * 2.2f * rb.velocity.sqrMagnitude;
		float rolling = 30f * drag;
		float spd = 50f;

		this.transform.Translate (Vector3.up * spd * v * Time.deltaTime);
		this.transform.Rotate (-Vector3.forward * rot * h * v * rot * Time.deltaTime);
		Vector2 force = engineF * v * Vector2.up - drag * rb.velocity.normalized - rolling * rb.velocity.normalized;
		//Debug.Log (force);
		//Debug.Log (engineF * v * Vector2.up);
		//Debug.Log(rb.velocity);
		//rb.AddRelativeForce(force);
		//rb.AddTorque (5f * v * h);
		//rb.AddForce (Vector3.up * (spd * v - 0.5f * 0.3f * 2.2f * rb.velocity.magnitude * rb.velocity.magnitude));//0.5 * 0.30 * 2.2 * v^2
	}
	void OnCollisionStay2D(Collision2D col){ // col has the other collider
		Debug.Log("Colided with: " + col.gameObject.tag);
		GameObject.Find("Road").SendMessage("saySomething", "something");
		//Vector2 point = col.contacts [0].normal;
		//thisTransform.transform.Translate ((Vector3)point * 200 * Time.deltaTime);
		//Destroy(this.gameObject);
	}
}
