using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("collided");
    }
}
