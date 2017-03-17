using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgidScript : MonoBehaviour {

    private Rigidbody theRigid;
    public Vector3 force;

	// Use this for initialization
	void Start () {
        theRigid = GetComponent<Rigidbody>();
        theRigid.AddForce(force);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        theRigid.AddForce(force, ForceMode.Force);
    }

   
}
