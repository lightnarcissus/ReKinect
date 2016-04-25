using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimerText : MonoBehaviour {

    public float timer = 0f;
    public Text timerTextLeft;
    public Text timerTextRight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        timerTextLeft.text =timer.ToString("F2");
        timerTextRight.text= timer.ToString("F2");
    }

    public void ResetTimer()
    {
        timer = 0f;
    }
}
