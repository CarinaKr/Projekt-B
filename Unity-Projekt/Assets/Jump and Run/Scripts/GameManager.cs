using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Player hatPlayer;
    //public Enemy hatEnemy;
    //public GUISkin hatSkin;

    //private float zXPlayer, zYPlayer;
    //private float zXEnemy, zYEnemy;
    //private float zAbstand;

    private float zMoveSpeed;
    public float zPlayerFactor;
    public float moveSpeed;
	public int zPunkte;
	private bool zGameOver,zGewonnen;


	// Use this for initialization
	void Start () {
        zMoveSpeed = moveSpeed;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
        /*zXPlayer = hatPlayer.transform.position.x;
		zYPlayer = hatPlayer.transform.position.y;
		zXEnemy = hatEnemy.transform.position.x;
		zYEnemy = hatEnemy.transform.position.y;

		zAbstand = zXPlayer - zXEnemy;

		if(zAbstand<=0)
		{
			this.gameOver();
		}
		else if(zAbstand>=100)
		{
			this.gewonnen();
		}*/
        if (transform.position.x < hatPlayer.transform.position.x - zPlayerFactor)
        {
            transform.position = new Vector3(hatPlayer.transform.position.x - zPlayerFactor, -10);
        }
        transform.position = new Vector3( transform.position.x+moveSpeed,hatPlayer.transform.position.y, -10);

    }

	public void gameOver()
	{
		zGameOver = true;
		//hatPlayer.die ();
		//hatEnemy.die ();
		//hatPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		//hatEnemy.GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeAll;
	}
	void gewonnen()
	{
		zGewonnen = true;
		//hatPlayer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		//hatEnemy.GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeAll;
	}

	void OnGUI()
	{
		/*GUI.skin = hatSkin;
		GUI.Label (new Rect (10, 10, 400, 50), "Distanz");

		GUI.HorizontalSlider (new Rect (10, 70, 200, 50), zAbstand, 0, 100);

		GUI.Label (new Rect (10, 100, 400, 50), "Punkte: " + zPunkte);

		if(zGewonnen)
		{
			GUI.Label(new Rect(50,50,100,50),"Gewonnen");

		}
		else if(zGameOver)
		{
			GUI.Label(new Rect(50,50,100,50),"Restart");
		}*/

		//GUI.Box (new Rect (10,70,400,50), "Box");

		/*if(GUI.Button (new Rect(10,190,100,45),"Quit"))
		{Application.Quit();}*/
	}

    public void setMoveSpeed(float pSpeed)
    {
        moveSpeed = pSpeed;
    }

    public void resetMoveSpeed()
    {
        moveSpeed = zMoveSpeed;
    }

    public void die()
    {
        transform.position = hatPlayer.transform.position;
    }
}
