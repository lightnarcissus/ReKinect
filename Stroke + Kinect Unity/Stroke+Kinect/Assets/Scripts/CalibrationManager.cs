using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CalibrationManager : MonoBehaviour {
    public AvatarController avatarController;
    public KinectManager kinectManager;
    public Text balanceWarning;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(avatarController.outOfBalance)
        {
            kinectManager.displayUserMap = true;
            balanceWarning.enabled = true;
        }
        else
        {
            kinectManager.displayUserMap = false;
            balanceWarning.enabled = false;
        }
	
	}
}
