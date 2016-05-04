using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public GameObject avatarController;
    public GameObject kinectPrefab;
    public GameObject kinectManager;
    public static bool created = false;
    public static int currentApp = 0;
    public static int focusSide = 0; //1 for left and 2 for right
	// Use this for initialization
	void Start () {
        if (!created)
        {
            kinectManager = Instantiate(kinectPrefab, transform.position, Quaternion.identity) as GameObject;
            created = true;
        }
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(kinectManager);
       // DontDestroyOnLoad(avatarController);
      //  DontDestroyOnLoad(Camera.main.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //   Destroy(kinectManager);
            ActivateApp(1);
         //   GameManager.activeApp = 1;
            currentApp = 1;
            //Application.LoadLevel("MainMenu");
          //  Destroy(gameObject);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateApp(2);
            currentApp = 2;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateApp(3);
            currentApp = 3;
        }
	}

    public void ActivateApp(int appNumber)
    {
        switch (appNumber)
        {

            case 1:
                GameManager.activeApp = 1;
                Application.LoadLevel("DrawingChallenge");
                break;
            case 2:
                GameManager.activeApp = 2;
                Application.LoadLevel("CardMatching");
                break;
            case 3:
                GameManager.activeApp = 3;
                Application.LoadLevel("ConductorMusic");
                break;
        }
    }


}
