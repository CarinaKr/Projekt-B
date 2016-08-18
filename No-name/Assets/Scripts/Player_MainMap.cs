using UnityEngine;
using System.Collections;

public class Player_MainMap : MonoBehaviour {

	private Vector3 spawn;
	private Vector3 input;
	private float maxSpeed=5f;
	public float moveSpeed;


	// Use this for initialization
	void Start () {
		spawn = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		if (rigidbody.velocity.magnitude < maxSpeed) 
		{rigidbody.AddRelativeForce (input * moveSpeed);}*/
		if(Input.GetButton("Vertical"))
		{
			transform.Translate(0,0,Input.GetAxisRaw("Vertical")*moveSpeed);
		}

		if (Input.GetButton("Horizontal")) 
		{
			transform.Rotate(Vector3.up,Input.GetAxisRaw("Horizontal"));
		}


		if (transform.position.y < -2) 
		{ this.die(); }
	}

	void die()
	{
		transform.position = spawn;
	}
}
