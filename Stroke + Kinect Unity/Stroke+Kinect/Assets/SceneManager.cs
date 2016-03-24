using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public GameObject avatarController;
    public GameObject kinectManager;
    public int focusSide = 0; //1 for left and 2 for right
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(kinectManager);
       // DontDestroyOnLoad(avatarController);
      //  DontDestroyOnLoad(Camera.main.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ActivateApp(int appNumber)
    {
        switch (appNumber)
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
