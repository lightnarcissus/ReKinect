using UnityEngine;
using System.Collections;

public class TuningForkManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(MalpositionManager.malPosActive || Input.GetKey(KeyCode.F))
        {
            GetComponent<Sinus>().IncrementFrequency();
        }
       else
        {
            GetComponent<Sinus>().DecrementFrequency();
        }
    }
}
