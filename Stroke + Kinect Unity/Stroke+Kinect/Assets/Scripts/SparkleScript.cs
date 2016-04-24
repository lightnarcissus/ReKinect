using UnityEngine;
using System;
using System.Collections;


public class SparkleScript : MonoBehaviour {

	Vector3 tempPos;

	// Use this for initialization
	void Start () {
//		Screen.showCursor = false;

	}
	
	// Update is called once per frame
	void Update () {

		tempPos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(tempPos.x, tempPos.y, 0f);
	}
}
