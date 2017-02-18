using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmHTFManager : MonoBehaviour {

    public static float malPosFactor = 0f;
    public GameObject avatarController;
    public static int tuningForkTask = 0;
    public GameObject sceneManager;
    public GameObject leftCircle;
    public GameObject rightCircle;
    public RectTransform canvasRect;
    public GameObject rightTracker;
    public GameObject leftTracker;
    public float multX = 1f;
    public float multY = 1f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector2 leftPos = new Vector3(0f, avatarController.GetComponent<AvatarController>().handLeftPos.y * multY, avatarController.GetComponent<AvatarController>().handLeftPos.z);
        Vector2 leftViewport = RectTransformUtility.WorldToScreenPoint(Camera.main, leftPos);
        leftTracker.GetComponent<RectTransform>().anchoredPosition = new Vector3(-100f,leftViewport.y - canvasRect.sizeDelta.y / 2f);
        // leftTracker.GetComponent<RectTransform>().anchorMax = leftViewport;

        Vector2 rightPos = new Vector3(0f, avatarController.GetComponent<AvatarController>().handRightPos.y * multY, avatarController.GetComponent<AvatarController>().handRightPos.z);
        Vector2 rightViewport = RectTransformUtility.WorldToScreenPoint(Camera.main, rightPos);
        rightTracker.GetComponent<RectTransform>().anchoredPosition = new Vector3(100f,rightViewport.y-canvasRect.sizeDelta.y/2f);
        //rightTracker.GetComponent<RectTransform>().anchorMax = rightViewport;
    }
}
