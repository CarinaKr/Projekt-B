using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Player hatPlayer;
	public Enemy hatEnemy;
	public GUISkin hatSkin;

	private float zXPlayer, zYPlayer;
	private float zXEnemy, zYEnemy;
	private float zAbstand;
	public int zPunkte;
	private bool zGameOver,zGewonnen;


	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () 
	{
		zXPlayer = hatPlayer.transform.position.x;
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
		}
	
	}

	public void gameOver()
	{
		zGameOver = true;
		//hatPlayer.die ();
		//hatEnemy.die ();
		hatPlayer.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		hatEnemy.rigidbody.constraints=RigidbodyConstraints.FreezeAll;
	}
	void gewonnen()
	{
		zGewonnen = true;
		hatPlayer.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		hatEnemy.rigidbody.constraints=RigidbodyConstraints.FreezeAll;
	}

	void OnGUI()
	{
		GUI.skin = hatSkin;
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
		}

		//GUI.Box (new Rect (10,70,400,50), "Box");

		/*if(GUI.Button (new Rect(10,190,100,45),"Quit"))
		{Application.Quit();}*/
	}
}
