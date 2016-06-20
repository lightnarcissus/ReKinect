using UnityEngine;
using System.Collections;

public class BoxScript : MonoBehaviour {

	public Sprite BoxOpened, BoxClosed;

	int receivedCard;
	int goalNumber;

	// Use this for initialization
	void OnEnable () {
		gameObject.GetComponent<SpriteRenderer> ().sprite = BoxOpened;
		receivedCard = 0;
		if (gameObject.name == "Colors")
			goalNumber = 3;
		else
			goalNumber = 2; 

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//Debug.Log(col.gameObject.GetInstanceID());
		if (col.gameObject.tag == gameObject.tag) {
			receivedCard++; 
		}

		if (receivedCard == goalNumber) { 
			Debug.Log ("CLOSE");
			gameObject.GetComponent<SpriteRenderer>().sprite = BoxClosed;
		}
	}
}
