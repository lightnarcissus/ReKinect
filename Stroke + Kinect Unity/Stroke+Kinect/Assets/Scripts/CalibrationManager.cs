using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CalibrationManager : MonoBehaviour {
    public AvatarController avatarController;
    public KinectManager kinectManager;
    public Text balanceWarning;
    public Text balanceWarning2;
	// Use this for initialization
	void Start () {
        balanceWarning.enabled = false;
        if (GameManager.activeApp != 1)
            balanceWarning2.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(avatarController.outOfBalance)
        {
            kinectManager.displayUserMap = true;
            balanceWarning.enabled = true;
            if(GameManager.activeApp!=1)
            balanceWarning2.enabled = true;
        }
        else
        {
            kinectManager.displayUserMap = false;
            balanceWarning.enabled = false;
            if (GameManager.activeApp != 1)
                balanceWarning2.enabled = false;
        }
	
	}
}
