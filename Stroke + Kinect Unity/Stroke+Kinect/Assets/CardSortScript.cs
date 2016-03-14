using UnityEngine;
using System.Collections;

public class CardSortScript : MonoBehaviour {
			
		public bool isPicking = false;
		private Vector3 tempPos;
		GameObject levelManager;
		
		void Start () {
			levelManager = GameObject.Find("LevelManager");
		}
		
		void Update () {
			if(isPicking) {
				tempPos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
				transform.position = new Vector3(tempPos.x, tempPos.y, 0f);
			}
		}
		
		void OnMouseDown() {
			isPicking = true;
		}
		
		void OnMouseUp() {
			isPicking = false;
		}
		
		void OnTriggerEnter2D(Collider2D col)
		{
			//Debug.Log(col.gameObject.GetInstanceID());
			
			if (gameObject.tag == "Lv4Colors" && col.gameObject.name == "Colors") {
				levelManager.GetComponent<CardLevelManager> ().CorrectMatch ();
				Destroy (gameObject);
			}

			if (gameObject.tag == "Lv4Nums" && col.gameObject.name == "Numbers") {
				levelManager.GetComponent<CardLevelManager> ().CorrectMatch ();
				Destroy (gameObject);
			}

			if (gameObject.tag == "Lv4Pets" && col.gameObject.name == "Pets") {
				levelManager.GetComponent<CardLevelManager> ().CorrectMatch ();
				Destroy (gameObject);
			}
			
			if (gameObject.tag == "Lv4Flowers" && col.gameObject.name == "Flowers") {
				levelManager.GetComponent<CardLevelManager> ().CorrectMatch ();
				Destroy (gameObject);
			}
		}
	}
	
