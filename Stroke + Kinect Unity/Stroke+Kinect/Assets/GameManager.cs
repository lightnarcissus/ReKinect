using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public InteractionManager intManager;
	public GameObject gamePage;
	public GameObject titlePage;
	public AlternateController altControl;
	// Use this for initialization
	void Start () {
	
		InvokeRepeating ("CheckController", 0.1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {

//        Debug.Log(intManager.userActive);
	
	
	}

	void CheckController()
	{
		if (!altControl.noKinect) {
			if (intManager.userActive) {
				titlePage.SetActive (false);
				gamePage.SetActive (true);
			} else {
				titlePage.SetActive (true);
				gamePage.SetActive (false);
			}
		} else {
			titlePage.SetActive (false);
			gamePage.SetActive (true);
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
