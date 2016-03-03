using UnityEngine;
using System.Collections;

public class RaycastObj : MonoBehaviour {
    public Ray ray;
    public RaycastHit hit;
    public RaycastHit2D hit2D;
    private int whichLevel = 0; //1 for drawing, 2 for music
	// Use this for initialization
	void Start () {

        if (gameObject.name == "Collider")
            whichLevel = 1;
        else
            whichLevel = 2;
	
	}

    // Update is called once per frame
    void Update()
    {
      
        if (whichLevel==1)
        {
            if (Physics.Linecast(transform.position, Camera.main.transform.position, out hit))
            {
                if (hit.collider.gameObject.tag == "Target")
                {
                    hit.collider.gameObject.GetComponent<CheckCollision>().TargetCollision();
                 //   Debug.Log("blocked by" + hit.collider.gameObject.name);
                }
            }
        }

        //for music conductor scene
        else if (whichLevel==2)
        {
            if (Physics.Linecast(transform.position, Camera.main.transform.position, out hit))
            {
                if (hit.collider.gameObject.tag == "Target")
                {
                    hit.collider.gameObject.GetComponent<OrchestraSegment>().PlaySound();
                 //   Debug.Log("blocked by" + hit.collider.gameObject.name);
                }
            }
        }
        if (gameObject.name == "Collider 2D")
        {
            hit2D = Physics2D.Linecast(transform.position, Camera.main.transform.position);
            if (hit2D.collider.gameObject.tag == "Target")
            {
              //  hit2D.collider.gameObject.GetComponent<CheckCollision>().TargetCollision();
                Debug.Log("blocked by" + hit2D.collider.gameObject.name);
            }
        }
    }
}
