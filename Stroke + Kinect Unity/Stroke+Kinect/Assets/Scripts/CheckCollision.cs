using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour {

    public int targetID = 0;
    public GameObject drawingManager;
    private bool changed = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TargetCollision()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        if (changed)
            changed = false;
        changed = true;
        drawingManager.GetComponent<DrawingManager>().AssignNextTarget(targetID);
        StartCoroutine("ChangeBack");
       // Debug.Log("collision");
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
