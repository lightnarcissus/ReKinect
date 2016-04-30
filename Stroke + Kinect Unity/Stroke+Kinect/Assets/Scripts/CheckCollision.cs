using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour {

    public int targetID = 0;
    public GameObject drawingManager;
    private bool changed = false;
    public GameObject indicatorSprite;
	// Use this for initialization
	void Start () {

        indicatorSprite.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TargetCollision()
    {
        //  GetComponent<SpriteRenderer>().color = Color.green;
        StartCoroutine("IndicatorTarget");
        if (changed)
            changed = false;
        changed = true;
        drawingManager.GetComponent<DrawingManager>().AssignNextTarget(targetID);
        StartCoroutine("ChangeBack");
       // Debug.Log("collision");
    }

    IEnumerator IndicatorTarget()
    {
        indicatorSprite.SetActive(true);
        if (drawingManager.GetComponent<DrawingManager>().correct)
            indicatorSprite.GetComponent<SpriteRenderer>().color = Color.green;
        else
            indicatorSprite.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(1f);
        indicatorSprite.SetActive(false);
        yield return null;
    }


    IEnumerator ChangeBack()
    {
        yield return new WaitForSeconds(1f);
        if(changed)
        {
            GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
}
