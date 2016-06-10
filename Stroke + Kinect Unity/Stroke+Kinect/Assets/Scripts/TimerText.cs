using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimerText : MonoBehaviour {

    public float timer = 0f;
    public Text timerTextLeft;
    public Text timerTextRight;
    public float prevLevelTimer = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        timerTextLeft.text =timer.ToString("F2");
        timerTextRight.text= timer.ToString("F2");
    }

    public float CalculateLevelTime()
    {
        float levelTime = timer - prevLevelTimer;
        prevLevelTimer = levelTime;
        Debug.Log("Level time is " + levelTime);
        return levelTime;
    }

    public void ResetTimer()
    {
        timer = 0f;
    }
}
