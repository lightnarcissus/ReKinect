using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public KinectManager kinManager;
	public GameObject gamePage;
	public GameObject titlePage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (kinManager.avatarControllers.Count > 0) {
			titlePage.SetActive (false);
			gamePage.SetActive (true);
		} else {
			titlePage.SetActive (true);
			gamePage.SetActive (false);
		}
	
	}
}
