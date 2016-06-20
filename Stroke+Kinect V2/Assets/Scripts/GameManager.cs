using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public InteractionManager intManager;
	public GameObject gamePage;
    public GameObject focusSelectionPage;
	public GameObject titlePage;
    public AvatarController avatarCont;
	public AlternateController altControl;
    public SceneManager sceneManager;
    public bool focusSelected = false;
    public GUIText debugText;
    public Text confirmText;
    public Text chooseText;

    public static int activeApp = 0; // 1 is drawing and so on
	// Use this for initialization
	void Start () {

        focusSelectionPage.SetActive(false);
        gamePage.SetActive(false);
        titlePage.SetActive(true);
		InvokeRepeating ("CheckController", 0.1f, 0.1f);


     
	}
	
	// Update is called once per frame
	void Update () {
        MouseControl.MouseMove(avatarCont.elbowPos,debugText);
       // Input.mousePosition = avatarCont.elbowPos;
       //        Debug.Log(intManager.userActive);


    }

	void CheckController()
	{
		if (!altControl.noKinect) {
			if (intManager.userActive && !focusSelected) {
				titlePage.SetActive (false);
                focusSelectionPage.SetActive(true);
                avatarCont.outOfBalance = true;
                CheckStatus();
            } else if(focusSelected) {
               
				focusSelectionPage.SetActive (false);
				gamePage.SetActive (true);
			}
		} else {
			titlePage.SetActive (false);
			gamePage.SetActive (true);
		}
	}

    void CheckStatus()
    {
       // Debug.Log("left" + intManager.leftHandPos.y);
        
      //  Debug.Log("right" + intManager.rightHandPos.y);
      if(SceneManager.focusSide==0)
        //if (intManager.leftHandPos.y > intManager.rightHandPos.y && Mathf.Abs(intManager.leftHandPos.y) > 0.5f)
        {
            avatarCont.activeJoint = 1;
            Debug.Log("left");
            focusSelectionPage.transform.GetChild(0).gameObject.SetActive(false);
            focusSelectionPage.transform.GetChild(1).gameObject.SetActive(true);
            focusSelectionPage.transform.GetChild(3).gameObject.SetActive(true);
            StartCoroutine("ActivateLeft");
            confirmText.gameObject.SetActive(true);
            confirmText.text = "CONFIRM IF LEFT ARM IS \n THE FOCUS SIDE";
            chooseText.gameObject.SetActive(false);

        }
        else if (SceneManager.focusSide==1)
      //  else if(intManager.rightHandPos.y > intManager.leftHandPos.y && Mathf.Abs(intManager.rightHandPos.y) > 0.5f)
        {
            avatarCont.activeJoint=2;
            Debug.Log("right");
            focusSelectionPage.transform.GetChild(0).gameObject.SetActive(false);
            focusSelectionPage.transform.GetChild(2).gameObject.SetActive(true);
            focusSelectionPage.transform.GetChild(3).gameObject.SetActive(true);
            StartCoroutine("ActivateRight");
            confirmText.gameObject.SetActive(true);
            confirmText.text = "CONFIRM IF RIGHT ARM IS \n THE FOCUS SIDE";
            chooseText.gameObject.SetActive(false);
        }
        else
        {
            chooseText.gameObject.SetActive(true);
        }
    }

    IEnumerator ActivateLeft()
    {
        yield return new WaitForSeconds(2f);
        if (intManager.leftHandPos.y > intManager.rightHandPos.y)
        {
            focusSelected = true;
            SceneManager.focusSide = 0;
            focusSelectionPage.transform.GetChild(2).gameObject.SetActive(false);
        }

            yield return null;
    }
    IEnumerator ActivateRight()
    {
        yield return new WaitForSeconds(2f);
        if (intManager.rightHandPos.y > intManager.leftHandPos.y)
        {
            focusSelected = true;
            SceneManager.focusSide = 1;
            focusSelectionPage.transform.GetChild(2).gameObject.SetActive(false);
        }

            yield return null;
    }

    
}
