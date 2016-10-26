using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject[] Element;
	public GameManager hatGameManager;

    private Vector3 spawn;

    public float moveSpeed;
	private Vector3 input;
	private float maxSpeed=5f;

	public float jumpHeight;
    public float jumpSpeed;
    public float gravity;
	private int zJumpZahl=0;
    private bool zJump;

    public float climpSpeed;

    public float mudFactor;

    public float trampolineHight;

    private int zElementZahl = 0; //für vorgeschriebene Reihenfolge der Elemente
	public int zElementGroesse;
    


    // Use this for initialization
    void Start () {
		spawn = transform.position;
		//Instantiate(Element[zElementZahl],new Vector2(0,0),Quaternion.Euler (0,0,0));
		zElementZahl++;

        Physics.gravity = new Vector3(0, gravity, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, 0);

        if (zJumpZahl == 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(Input.GetAxisRaw("Horizontal")*moveSpeed, GetComponent<Rigidbody>().velocity.y, 0);
        }
        else if (zJumpZahl != 0 && Input.GetAxisRaw("Horizontal") != 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(Input.GetAxisRaw("Horizontal") * jumpSpeed, GetComponent<Rigidbody>().velocity.y, 0);
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

		if (transform.position.y < -20) 
		{  this.die ();	}
	}

	void OnCollisionEnter(Collision other)	
	{
		if (other.transform.tag == "floor") 
		{
			zJumpZahl=0;
		}

		if (other.transform.tag == "platform"||other.transform.tag=="moving_platform"||other.transform.tag=="disappearing_platform") 
		{

            if (transform.position.y-(GetComponent<Collider>().bounds.size.y/2) > other.transform.position.y+0.4) 
			{zJumpZahl = 0;	} 
			else 
			{zJumpZahl = 2;	}

            if (other.transform.tag == "moving_platform")
            {
                transform.parent = other.transform;
            }
		}

        if (other.transform.tag == "wall"&&zJumpZahl!=0)
        {
            zJumpZahl = 2;   
        }

		if(other.transform.tag=="enemy")
		{
			gameOver();
		}
	}

    void OnCollisionExit(Collision other)
    {
        if (other.transform.tag == "moving_platform")
        {
            transform.parent = null;
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
            //(Element[pNummer],new Vector2(zElementGroesse*zElementZahl,0),Quaternion.Euler (0,0,0));
            //zElementZahl++;
            //Destroy(other.gameObject);
            other.GetComponent<Renderer>().material.color = Color.cyan;
            other.transform.tag = "Untagged";
		}

        if(other.transform.tag=="item")
		{
			hatGameManager.zPunkte++;
			Destroy(other.gameObject);
		}

        if (other.transform.tag == "ladder")
        {
            GetComponent<Rigidbody>().useGravity = false;
            zJumpZahl = 0;
        }

        if (other.transform.tag == "mud")
        {
            moveSpeed /= mudFactor;
            jumpSpeed /= mudFactor;
            jumpHeight /= mudFactor;
            zJumpZahl = 1;
        }

        if (other.transform.tag == "trampoline")
        {
            if (zJumpZahl != 0)
            {
                 GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, trampolineHight, 0);
                 zJumpZahl = 2;
            }
        }

        if (other.transform.tag == "information")
        {
            hatGameManager.setMoveSpeed(0);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "ladder")
        {
            if (zJumpZahl == 0)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, Input.GetAxisRaw("Vertical") * climpSpeed, 0);//Grab on to ladder by itself
            }
            else
            { GetComponent<Rigidbody>().useGravity = true; }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "ladder")
        {
            GetComponent<Rigidbody>().useGravity = true;

        }

        if (other.transform.tag == "mud")
        {
            moveSpeed *= mudFactor;
            jumpSpeed *= mudFactor;
            jumpHeight *= mudFactor;
        }

        if (other.transform.tag == "information")
        {
            hatGameManager.resetMoveSpeed();
            other.transform.tag = "Untagged";
        }
    }

	void jump()
	{
        //Vector3 jump = GetComponent<Rigidbody>().velocity;
        Vector3 jump = new Vector3(Input.GetAxisRaw("Horizontal")*jumpSpeed, 0, 0);
		jump.y = jumpHeight;
		if (zJumpZahl < 1) 
		{
			GetComponent<Rigidbody>().velocity=jump;
		} 
		else if (zJumpZahl == 1) 
		{
			GetComponent<Rigidbody>().velocity=jump;
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
