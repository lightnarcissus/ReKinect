using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SoundInstPanel : MonoBehaviour {

    public Text instText;
    public Text hoverText;
    public Text returnText;

    Vector3 hoverButtonPos;
    Vector3 returnButtonPos;

    public static string wellDone = "Well Done";
    public static string hoverInst= "Hover over the box below to hear the correct tone. \n This tone will indicate that you are moving correctly.";
	// Use this for initialization
	void Start () {
        hoverButtonPos = hoverText.gameObject.transform.parent.localPosition;
        returnButtonPos = returnText.gameObject.transform.parent.localPosition;
        instText.text = hoverInst;
        hoverText.text = "Hover Over";
        returnText.text = "Go To Game";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowWellDoneState()
    {
        instText.text = wellDone;
        hoverText.gameObject.transform.parent.localPosition = returnButtonPos;
        returnText.gameObject.transform.parent.localPosition = hoverButtonPos;
        hoverText.text = "Play tone again";
    }

    public void GoToGame()
    {
        gameObject.SetActive(false);
    }
}
