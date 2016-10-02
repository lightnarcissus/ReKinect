using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CalibrationManager : MonoBehaviour {
    public AvatarController avatarController;
    public KinectManager kinectManager;
    public GameObject balanceWarning;
    public GameObject balanceWarning2;
	// Use this for initialization
	void Start () {
        balanceWarning.SetActive(false);
        if (GameManager.activeApp == 3)
            balanceWarning2.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        /*
        if(avatarController.outOfBalance)
        {
            kinectManager.displayUserMap = true;
            balanceWarning.SetActive(true);
            if (GameManager.activeApp==3)
            balanceWarning2.SetActive(true);
        }
        else
        {
            kinectManager.displayUserMap = false;
            balanceWarning.SetActive(false);
            if (GameManager.activeApp == 3)
                balanceWarning2.SetActive(false);
        }
        */
	
	}
}
