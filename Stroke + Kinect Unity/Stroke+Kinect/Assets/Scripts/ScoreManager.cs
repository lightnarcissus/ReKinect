using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {

    private int score = 0;
    public Text scoreTextLeft;
    public Text scoreTextRight;

    public Text changeText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(score);
       if(Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine("ChangeSides");
        }
       // Debug.Log("Focus side is" + SceneManager.focusSide);
        if(SceneManager.focusSide==0)
        scoreTextLeft.text = score.ToString() + " /17";
        else
            scoreTextRight.text = score.ToString() + " /17";
    }

    public void IncrementScore()
    {
        score++;
    }

    public void DecrementScore()
    {
        score--;
    }

    public void ResetScore()
    {
        score = 0;
    }

    IEnumerator ChangeSides()
    {

        changeText.enabled = true;
        yield return new WaitForSeconds(2f);
        changeText.enabled = false;
        if (SceneManager.focusSide == 1)
            SceneManager.focusSide = 0;
        else
            SceneManager.focusSide = 1;
        yield return null;
    }

    public int RetrieveScore()
    {
        return score;
    }
}
