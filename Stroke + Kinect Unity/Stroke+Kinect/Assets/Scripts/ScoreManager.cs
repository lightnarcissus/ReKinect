using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {

    private int score = 0;
    public Text scoreText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        scoreText.text = score.ToString() + " /17";
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

    public int RetrieveScore()
    {
        return score;
    }
}
