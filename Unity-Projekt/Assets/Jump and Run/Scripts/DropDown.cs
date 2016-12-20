using UnityEngine;
using System.Collections;

public class DropDown : MonoBehaviour {

   public Rigidbody2D drop;

	// Use this for initialization
	void Start () {
        drop.gravityScale = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "player")
        {
            drop.gravityScale = 1f;
        }
    }
   
}
