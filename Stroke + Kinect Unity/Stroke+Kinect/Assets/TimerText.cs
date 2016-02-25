using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimerText : MonoBehaviour {

    public float timer = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        GetComponent<Text>().text = "Time: " + timer.ToString("F2");
	}
}
