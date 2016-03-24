using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimerText : MonoBehaviour {

    public float timer = 0f;
    public Text timerText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        timerText.text =timer.ToString("F2");
    }

    public void ResetTimer()
    {
        timer = 0f;
    }
}
