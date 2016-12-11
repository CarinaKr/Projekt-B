using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

    public Sprite defaultSprite;
    public Sprite triggerSprite
;

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<SpriteRenderer>().sprite = triggerSprite;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }
}
