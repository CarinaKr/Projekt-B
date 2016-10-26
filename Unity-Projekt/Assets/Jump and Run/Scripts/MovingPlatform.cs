using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public Transform[] patrolPoints;
    public float moveSpeed;
    private int nextPoint;

    // Use this for initialization
    void Start () {

       // transform.position = patrolPoints[0].position;
        nextPoint = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position == patrolPoints[nextPoint].position)
        {
            nextPoint++;
            if (nextPoint > patrolPoints.Length-1)
            { nextPoint = 0; }
        }
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[nextPoint].position, moveSpeed * Time.deltaTime);

    }
}
