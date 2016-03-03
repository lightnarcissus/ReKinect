using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public InteractionManager intManager;
	public GameObject gamePage;
	public GameObject titlePage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(intManager.userActive);

		if (intManager.userActive) {
			titlePage.SetActive (false);
			gamePage.SetActive (true);
		} else {
			titlePage.SetActive (true);
			gamePage.SetActive (false);
		}
	
	}

    public void ActivateApp(int appNumber)
    {
        switch(appNumber)
        { 

            case 1:
                Application.LoadLevel("DrawingChallenge");
                break;
            case 2:
                Application.LoadLevel("CardMatching");
                break;
            case 3:
                Application.LoadLevel("ConductorMusic");
                break;
        }
    }
}
