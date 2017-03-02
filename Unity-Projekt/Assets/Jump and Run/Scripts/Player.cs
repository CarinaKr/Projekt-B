using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject[] Element;
	public GameManager hatGameManager;

    private Vector3 spawn;
    private Rigidbody2D rigBody;

    //public float moveSpeed;
	private Vector3 input;
	public float maxXSpeed;
    public float jumpMaxXSpeed;
    public float xAcc; //Beschleunigung; Geschwindigkeit, die der Bewegung in X-Richtung hinzugefügt wird.
    public float jumpXAcc; //Beschleunigung in der Luft
    public float jumpYAcc;
    public float trampolineYAcc;
    public float ladderSpeed;
    public float airDrag;
    public float startDrag;
    private bool onFloor=false;

	//public float jumpHeight;
    //public float jumpSpeed;
    public float gravity;
	private int zJumpZahl=0;
    private bool zJump;

    public float mudFactor;

    private bool zMunition;
    private int zWait;

    private int zElementZahl = 0; //für vorgeschriebene Reihenfolge der Elemente
	public int zElementGroesse;

    private Vector3 Test, test1;
    


    // Use this for initialization
    void Start () {
        rigBody = GetComponent<Rigidbody2D>();
        rigBody.drag = startDrag;
		spawn = transform.position;
		//Instantiate(Element[zElementZahl],new Vector2(0,0),Quaternion.Euler (0,0,0));
		zElementZahl++;
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<Animator>().speed = 1.5f;

        Physics2D.gravity = new Vector3(0, gravity, 0);
	}
    void Update()
    {
        if (zMunition)
        {
            zWait++;
            if (Input.GetButtonDown("PickUp") &&  zWait > 1)
            {
                zMunition = false;
                zWait = 0;
                GameObject child = transform.GetChild(0).gameObject;
                child.transform.parent = null;

                while (child.transform.position.x > hatGameManager.transform.position.x - zElementGroesse)
                {
                    child.transform.position = new Vector3(child.transform.position.x - 0.001f, child.transform.position.y, 0);
                }
                hatGameManager.slowCamera();

            }
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            this.GetComponent<Animator>().enabled=true;
            if (Input.GetAxisRaw("Horizontal") > 0)
            { this.GetComponent<Animator>().SetInteger("Direction", 3); }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            { this.GetComponent<Animator>().SetInteger("Direction", 1); }
        }
        else if (Input.GetButtonUp("Horizontal"))
        { this.GetComponent<Animator>().enabled = false; }
    }
	// Update is called once per frame
	void FixedUpdate () {

        if (zJumpZahl == 0 )
        {
            rigBody.drag = startDrag;
            if (rigBody.velocity.magnitude < maxXSpeed&&Input.GetAxisRaw("Horizontal")!=0)
            {
                rigBody.AddRelativeForce(new Vector2(Input.GetAxisRaw("Horizontal")*xAcc, 0));
            }
           // Test= new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, GetComponent<Rigidbody2D>().velocity.y, 0);
            //GetComponent<Rigidbody2D>().velocity = new Vector3(Input.GetAxisRaw("Horizontal")*moveSpeed, GetComponent<Rigidbody2D>().velocity.y, 0);  
        }
        else if (zJumpZahl != 0)
        {
            if (rigBody.velocity.magnitude < jumpMaxXSpeed)
            {
                rigBody.AddRelativeForce(new Vector2(Input.GetAxisRaw("Horizontal") * jumpXAcc, 0));
            }
            //test1 = new Vector3(Input.GetAxisRaw("Horizontal") * jumpSpeed, GetComponent<Rigidbody2D>().velocity.y, 0);
            //GetComponent<Rigidbody2D>().velocity = new Vector3(Input.GetAxisRaw("Horizontal") * jumpSpeed, GetComponent<Rigidbody2D>().velocity.y, 0);
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

	void OnCollisionEnter2D(Collision2D other)	
	{
        double unterkanteSpieler = transform.position.y - (transform.localScale.y / 2);
        double oberkantePlatform = other.transform.position.y + other.transform.localScale.y / 2;

        if (other.transform.tag == "floor") 
		{
            if (unterkanteSpieler >= oberkantePlatform)
            {
                zJumpZahl = 0;
                onFloor = true;
            }
		}

        if (other.transform.tag == "mud" || other.transform.tag == "water")
        {
            zJumpZahl = 1;
            onFloor = true;
        }

		if (other.transform.tag == "platform"||other.transform.tag=="moving_platform"||other.transform.tag=="disappearing_platform") 
		{
            //double unterkanteSpieler = transform.position.y - (transform.localScale.y / 2);
            //double oberkantePlatform = other.transform.position.y + other.transform.localScale.y/2;
            if (unterkanteSpieler>=oberkantePlatform) 
			{zJumpZahl = 0;	} 
			//else 
			//{zJumpZahl = 2;	}

            if (other.transform.tag == "moving_platform")
            {
                transform.parent = other.transform;
            }
		}

        if (other.transform.tag == "wall"&&zJumpZahl!=0)
        {
            zJumpZahl = 2;   
        }

		if(other.transform.tag=="enemy"||other.transform.tag=="spikes")
		{
            die();
		}

        /*if (other.transform.tag == "one-way-trigger")
        {
            if (transform.position.y - (GetComponent<Collider2D>().bounds.size.y / 2) > other.transform.position.y)
            {
                other.collider.isTrigger = false;
                zJumpZahl = 0;
            }
            else
            { other.collider.isTrigger = true; }
        }*/
        if (other.transform.tag == "one-way-trigger")
        {
            zJumpZahl = 0; 
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
       /* if (other.transform.tag == "floor")
        {
            zJumpZahl = 0;
        }*/
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "moving_platform")
        {
            transform.parent = null;
        }

    }

   
   
    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.tag == "nextElement") 
		{
            //int pNummer = Random.Range(0, Element.Length);
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
            GetComponent<Rigidbody2D>().gravityScale=0;
            zJumpZahl = 0;
        }

        if (other.transform.tag == "mud"||other.transform.tag=="water")
        {
            //moveSpeed /= mudFactor;
            //jumpSpeed /= mudFactor;
            //jumpHeight /= mudFactor;
            zJumpZahl = 1;

            xAcc /= mudFactor;
            jumpXAcc /= mudFactor;
            jumpYAcc /= mudFactor;
        }

        if (other.transform.tag == "trampoline")
        {
            if (zJumpZahl != 0)
            {
                
                    rigBody.AddForce(new Vector2(0, trampolineYAcc));
                
                //GetComponent<Rigidbody2D>().velocity = new Vector3(GetComponent<Rigidbody2D>().velocity.x, trampolineYAcc, 0);
                 zJumpZahl = 2;
            }
        }

        if (other.transform.tag == "information")
        {
            hatGameManager.setMoveSpeed(0);
        }

        if (other.transform.tag == "coin")
        {
            hatGameManager.zPunkte++;
            Destroy(other.gameObject);
        }

        if (other.transform.tag == "spikes")
        {
            die();
        }

        if (other.transform.tag == "save")
        {
            spawn = transform.position;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "ladder")
        {
            if (zJumpZahl == 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(rigBody.velocity.x, Input.GetAxisRaw("Vertical") * ladderSpeed, 0);//Grab on to ladder by itself
            }
            else
            { GetComponent<Rigidbody2D>().gravityScale = 1; }
        }

        if (other.transform.tag == "apple")
        {
            if (Input.GetButtonDown("PickUp")&&zMunition==false)
            {
                other.transform.parent = transform;
                other.transform.position = new Vector3(other.transform.parent.transform.position.x, other.transform.parent.transform.position.y+ 2, 0);
                zMunition = true;
                zWait = 0;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "ladder")
        {
            GetComponent<Rigidbody2D>().gravityScale=1;

        }

        if (other.transform.tag == "mud"||other.transform.tag=="water")
        {
            /* moveSpeed *= mudFactor;
             jumpSpeed *= mudFactor;
             jumpHeight *= mudFactor;*/

            xAcc *= mudFactor;
            jumpXAcc *= mudFactor;
            jumpYAcc *= mudFactor;

        }

        if (other.transform.tag == "information")
        {
            hatGameManager.resetMoveSpeed();
            other.transform.tag = "Untagged";
        }

        if (other.transform.tag == "one-way-trigger")
        {
            if (transform.position.y > other.transform.position.y)
            { other.isTrigger = false; }
            else if (transform.position.y < other.transform.position.y)
            { other.isTrigger = true; }
        }
    }

	void jump()
	{
        //Vector3 jump = GetComponent<Rigidbody>().velocity;
        /*Vector3 jump = new Vector3(Input.GetAxisRaw("Horizontal")*jumpSpeed, 0, 0);
		jump.y = jumpHeight;
		if (zJumpZahl < 1) 
		{
			GetComponent<Rigidbody2D>().velocity=jump;
		} 
		else if (zJumpZahl == 1) 
		{
			GetComponent<Rigidbody2D>().velocity=jump;
		}*/

        Vector2 jumpAcc = new Vector2(0, jumpYAcc);
        if (zJumpZahl < 1)
        {
            rigBody.AddForce(jumpAcc);
            rigBody.drag = airDrag;
        }
        else if (zJumpZahl == 1)
        {
            rigBody.AddForce(jumpAcc);
            rigBody.drag = airDrag;
        }

		zJumpZahl++;
        onFloor = false;
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
