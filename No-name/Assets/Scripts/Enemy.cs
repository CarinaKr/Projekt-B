using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameManager hatGameManager;

	public float moveSpeed;
	private Vector3 input;
	private float maxSpeed=5f;
	private Vector3 spawn;
	public float jumpHeight;
	private int zJumpZahl;

	
	// Use this for initialization
	void Start () {
		spawn = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(transform.position.y<-10)
		{
			die ();
		}
	}
	
	void OnCollisionEnter(Collision other)	
	{
		if (other.transform.tag == "floor") 
		{
			zJumpZahl=0;
			laufe();
		}

		if(other.transform.tag=="player")
		{
			hatGameManager.gameOver();
		}
	}

	void OnCollisionStay(Collision other)
	{
		laufe ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "jump") 
		{
			jump ();
		}
		else if(other.transform.tag=="doppleJump")
		{
			jump();jump();
		}

		if(other.transform.tag=="floor")
		{
			laufe();
		}
	}

	void laufe()
	{
		input = new Vector3 (1, 0, 0);
		if (rigidbody.velocity.magnitude < maxSpeed) 
		{rigidbody.AddRelativeForce (input * moveSpeed);
		}

		Debug.Log ("Enemy läuft");
	}

	void jump()
	{
		Vector3 jump = rigidbody.velocity;
		jump.y = jumpHeight;
		if (zJumpZahl < 1) 
		{
			rigidbody.velocity=jump;
		} 
		else if (zJumpZahl == 1) 
		{
			rigidbody.velocity=jump;
		}
		zJumpZahl++;
	}
	
	
	public void die()
	{  
		transform.position=spawn;
	}
}
