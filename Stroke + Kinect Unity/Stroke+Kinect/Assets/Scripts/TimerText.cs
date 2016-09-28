using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimerText : MonoBehaviour {

    public float timer = 0f;
    public Text timerTextLeft;
    public Text timerTextRight;
    public float prevLevelTimer = 0f;

	private int secondsCount=0;
	private int minutesCount=0;
	private int hoursCount=0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
	 // say there were 241.24
		minutesCount= Mathf.FloorToInt(timer/60);
		secondsCount = Mathf.FloorToInt (timer % 60);

		hoursCount = Mathf.FloorToInt (minutesCount / 60);
		if(SceneManager.focusSide==0)
			timerTextLeft.text=System.TimeSpan.FromSeconds (Mathf.FloorToInt(timer)).ToString();
		else
			timerTextRight.text=System.TimeSpan.FromSeconds (Mathf.FloorToInt(timer)).ToString();
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
