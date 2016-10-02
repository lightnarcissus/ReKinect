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

      //  tempPos = new Vector3(InteractionManager.Instance.cursorScreenPos.x * Screen.width, (1f - InteractionManager.Instance.cursorScreenPos.y) * Screen.height);
      //  tempPos = Input.mousePosition;
		    tempPos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(tempPos.x, tempPos.y, 0f);
	}
}
