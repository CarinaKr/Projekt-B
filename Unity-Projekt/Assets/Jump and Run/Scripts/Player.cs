using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject[] Element;
	public GameManager hatGameManager;

	public float moveSpeed;
	private Vector3 input;
	private float maxSpeed=5f;
	private Vector3 spawn;
	public float jumpHeight;
	private int zJumpZahl=0;
	private int zElementZahl = 0; //für vorgeschriebene Reihenfolge der Elemente
	public int zElementGroesse;
	private bool zJump;
	
	
	// Use this for initialization
	void Start () {
		spawn = transform.position;
		//Instantiate(Element[zElementZahl],new Vector2(0,0),Quaternion.Euler (0,0,0));
		zElementZahl++;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, 0);
		if (rigidbody.velocity.magnitude < maxSpeed) 
		{rigidbody.AddRelativeForce (input * moveSpeed);
		}
		
		if (Input.GetAxisRaw("Jump")==1&&zJump==false) 
		{
			zJump=true;
			jump(); 
		}
		else if(Input.GetAxisRaw("Jump")==0)
		{
			zJump=false;
		}

		if (transform.position.y < -10) 
		{  this.die ();	}
	}

	void OnCollisionEnter(Collision other)	
	{
		if (other.transform.tag == "floor") 
		{
			zJumpZahl=0;
		}

		if(other.transform.tag=="enemy")
		{
			gameOver();
		}
	}

	/*void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "nextElement"+zElementZahl) 
		{
			Instantiate(Element[zElementZahl],new Vector2(zElementGroesse*zElementZahl,0),Quaternion.Euler (0,0,0));
			zElementZahl++;
		}
		Debug.Log (other.transform.tag);
	}*/
	void OnTriggerEnter(Collider other)
	{
		int pNummer = Random.Range(0,Element.Length);
		if (other.transform.tag == "nextElement") 
		{
			//Element[pNummer] wird durch Element[zElementZahl] ersetzt (siehe OnTrigger oben)
			Instantiate(Element[pNummer],new Vector2(zElementGroesse*zElementZahl,0),Quaternion.Euler (0,0,0));
			zElementZahl++;
			Destroy(other.gameObject);
		}

		else if(other.transform.tag=="item")
		{
			hatGameManager.zPunkte++;
			Destroy(other.gameObject);
		}
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

	void gameOver()
	{
		hatGameManager.gameOver ();
	}

	public void die()
	{  
		transform.position=spawn;
	}
}
