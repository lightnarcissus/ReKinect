using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {

    private int score = 0;
    public Text scoreTextLeft;
    public Text scoreTextRight;

    //grade panel
    public Text timerText;
    public Text malpositionVal;
    public Text accuracy;
    public Text finalGrade;

    public Text changeText;

    public GameObject leftCanvas;
    public GameObject rightCanvas;

    public GameObject gradePanel;
    public GameObject timerManager;
    public GameObject malpositionManager;

    public float accuracyVal = 0f;
    private float finalScore = 0f;
    public Animator starAnim;
    private int currentLevel = 0;
    public List<int> scoreTarget;

    // Use this for initialization
    void Start () {

        gradePanel.SetActive(false);
        timerText.enabled = false;
        malpositionVal.enabled = false;
        accuracy.enabled = false;
        finalGrade.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(score);
       if(Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine("ChangeSides");
        }
        // Debug.Log("Focus side is" + SceneManager.focusSide);
        if (SceneManager.focusSide == 0)
            scoreTextLeft.text = score.ToString() + " /" + scoreTarget[currentLevel].ToString();
        else
            scoreTextRight.text = score.ToString() + " /" + scoreTarget[currentLevel].ToString();
    }

    public float GetCurrentLevelTime()
    {
        float currentLevelTime = timerManager.GetComponent<TimerText>().CalculateLevelTime();
        return currentLevelTime;
    }

    public void IncreaseLevel()
    {
        currentLevel++;
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

    public void ShowGrading()
    {
        StartCoroutine("ShowGradePanel");
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

    void UpdateGradePanel()
    {
        timerText.text = timerManager.GetComponent<TimerText>().timer.ToString("F2");
        malpositionVal.text = malpositionManager.GetComponent<MalpositionManager>().malpositionVal.ToString();
        accuracy.text = accuracyVal.ToString("F2");

        finalScore = ((timerManager.GetComponent<TimerText>().timer / 2f) * accuracyVal) - (malpositionManager.GetComponent<MalpositionManager>().malpositionVal * 2f);
        StartCoroutine("StarAnimation");
        Debug.Log("Final Score is: " + finalScore);

    }

    IEnumerator StarAnimation()
    {
        float starVal = finalScore / 100f;
        int starInt =  Mathf.FloorToInt(starVal);
        switch(starInt)
        {
            case 10:
                starAnim.SetBool("5Star", true);
                break;
            case 9:
                starAnim.SetBool("4.5Star", true);
                break;
            case 8:
                starAnim.SetBool("4Star", true);
                break;
            case 7:
                starAnim.SetBool("3.5Star", true);
                break;
            case 6:
                starAnim.SetBool("3Star", true);
                break;
            case 5:
                starAnim.SetBool("2.5Star", true);
                break;
            case 4:
                starAnim.SetBool("2Star", true);
                break;
            case 3:
                starAnim.SetBool("1.5Star", true);
                break;
            case 2:
                starAnim.SetBool("1Star", true);
                break;
            default:
                starAnim.SetBool("3Star", true);
                break;
        }
        yield return null;
    }

    IEnumerator ShowGradePanel()
    {
        leftCanvas.SetActive(false);
        rightCanvas.SetActive(false);
        gradePanel.SetActive(true);
        UpdateGradePanel();
        yield return new WaitForSeconds(1f);
        timerText.enabled = true;
        yield return new WaitForSeconds(1f);
        malpositionVal.enabled = true;
        yield return new WaitForSeconds(1f);
        accuracy.enabled = true;
        yield return new WaitForSeconds(1f);
        finalGrade.enabled = true;
        yield return new WaitForSeconds(1f);
        while(!Input.GetKeyDown(KeyCode.Return))
        {
            yield return 0;
        }
        Application.LoadLevel("MainMenu");
        yield return null;
    }

    public int RetrieveScore()
    {
        return score;
    }
}
