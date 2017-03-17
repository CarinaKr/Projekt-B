using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBehaviour : MonoBehaviour {

    public Vector3 transformVector;
    public Vector3 growVector;

	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {

        //gameObject.transform.Translate(transformVector);
        
        gameObject.transform.localScale = gameObject.transform.localScale+growVector;
	}
}
