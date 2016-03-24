using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ConductorManager : MonoBehaviour {

    public GameObject sceneManager;
    public GameObject avatarController;
    public Text focusText;
    public TimerText timerManager;
    public ScoreManager scoreManager;
    // Use this for initialization

    void Awake()
    {
        sceneManager = GameObject.Find("SceneManager");
    }
    void Start () {
        if (sceneManager != null)
        {
            Debug.Log("focus side " + sceneManager.GetComponent<SceneManager>().focusSide);
            if (sceneManager.GetComponent<SceneManager>().focusSide == 1)
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 1;
                focusText.text = "Focus Side: \n Left Arm";
            }
            if (sceneManager.GetComponent<SceneManager>().focusSide == 2)
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 2;
                focusText.text = "Focus Side: \n Right Arm";
            }
            else
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 1;
                focusText.text = "Focus Side: \n Left Arm";
            }

            sceneManager.GetComponent<SceneManager>().kinectManager.GetComponent<KinectManager>().avatarControllers[0] = avatarController.GetComponent<AvatarController>();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RestartLevel()
    {
        timerManager.ResetTimer();
        scoreManager.GetComponent<ScoreManager>().ResetScore();
    }
}
