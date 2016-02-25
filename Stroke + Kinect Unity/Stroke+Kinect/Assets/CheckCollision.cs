using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("collision");
    }

    void OnTriggerEnter(Collider col)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("collision");
    }
}
