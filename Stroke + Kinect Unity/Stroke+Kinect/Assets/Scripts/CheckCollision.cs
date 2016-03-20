using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour {

    public int targetID = 0;
    public GameObject drawingManager;
	public Color correctColor;
	public Color normalColor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TargetCollision()
    {
		GetComponent<SpriteRenderer>().color = correctColor;
        StartCoroutine("TurnOffColor");
        drawingManager.GetComponent<DrawingManager>().AssignNextTarget(targetID);
       // Debug.Log("collision");
    }

    IEnumerator TurnOffColor()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().color = normalColor;
        yield return null;
    }
}
