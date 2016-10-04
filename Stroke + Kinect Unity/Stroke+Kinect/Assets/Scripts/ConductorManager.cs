using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ConductorManager : MonoBehaviour {

    public GameObject sceneManager;
    public GameObject avatarController;
    public TimerText timerManager;
    public ScoreManager scoreManager;
    public GameObject calibManager;

    public GameObject leftCanvas;
    public GameObject rightCanvas;
    public GameObject instructionPanel;

    public Transform orcLeft;
    public Transform orcRight;
    public Transform orchestra;
    public GameObject loadingScreen;
    public GameObject collidersGroupLeft;
    public GameObject collidersGroupRight;
    // Use this for initialization

    void Awake()
    {
        sceneManager = GameObject.Find("SceneManager");
    }
    void Start () {
        if (sceneManager != null)
        {
            StartCoroutine("ShowInstructions");
            //  Debug.Log("focus side " + sceneManager.GetComponent<SceneManager>().focusSide);
            if (SceneManager.focusSide == 0)
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 1;
                leftCanvas.SetActive(true);
                orchestra.transform.position = orcLeft.transform.position;
                collidersGroupLeft.SetActive(true);
                collidersGroupRight.SetActive(false);
                rightCanvas.SetActive(false);
              //  focusText.text = "Focus Side: \n Left Arm";
            }
            if (SceneManager.focusSide == 1)
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 2;
                leftCanvas.SetActive(false);
                orchestra.transform.position = orcRight.transform.position;
                collidersGroupLeft.SetActive(false);
                collidersGroupRight.SetActive(true);
                rightCanvas.SetActive(true);
                // focusText.text = "Focus Side: \n Right Arm";
            }
            else
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 1;
                leftCanvas.SetActive(true);
                collidersGroupLeft.SetActive(true);
                collidersGroupRight.SetActive(false);
                rightCanvas.SetActive(false);
                //   focusText.text = "Focus Side: \n Left Arm";
            }

            sceneManager.GetComponent<SceneManager>().kinectManager.GetComponent<KinectManager>().avatarControllers[0] = avatarController.GetComponent<AvatarController>();
            calibManager.GetComponent<CalibrationManager>().kinectManager = sceneManager.GetComponent<SceneManager>().kinectManager.GetComponent<KinectManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneManager != null)
        {
            //  Debug.Log("focus side " + sceneManager.GetComponent<SceneManager>().focusSide);
            if (SceneManager.focusSide == 0)
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 1;
                leftCanvas.SetActive(true);
                rightCanvas.SetActive(false);
                //  focusText.text = "Focus Side: \n Left Arm";
            }
            if (SceneManager.focusSide == 1)
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 2;
                leftCanvas.SetActive(false);
                rightCanvas.SetActive(true);
                // focusText.text = "Focus Side: \n Right Arm";
            }
            else
            {
                avatarController.GetComponent<AvatarController>().activeJoint = 1;
                leftCanvas.SetActive(true);
                rightCanvas.SetActive(false);
                //   focusText.text = "Focus Side: \n Left Arm";
            }
        }

    }  

    public void RestartLevel()
    {
        timerManager.ResetTimer();
        scoreManager.GetComponent<ScoreManager>().ResetScore();
    }
    IEnumerator ShowInstructions()
    {
        PointerCollide.canPlay = false;
        instructionPanel.SetActive(true);
        yield return new WaitForSeconds(10f);
        instructionPanel.SetActive(false);
        PointerCollide.canPlay = true;
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(5f);
        loadingScreen.SetActive(false);
        yield return null;
    }
}
