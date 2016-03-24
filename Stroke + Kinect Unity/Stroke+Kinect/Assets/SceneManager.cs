using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public GameObject avatarController;
    public int focusSide = 0; //1 for left and 2 for right
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
       // DontDestroyOnLoad(avatarController);
      //  DontDestroyOnLoad(Camera.main.gameObject);
        //set static references
        DrawingManager.avatarController = avatarController;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
