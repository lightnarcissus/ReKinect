using UnityEngine;
using System.Collections;

public class RaycastObj : MonoBehaviour {
    public Ray ray;
    public RaycastHit hit;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {

        /*   ray = new Ray(transform.position, transform.position - Camera.main.transform.position);
           Debug.DrawRay(transform.position, -transform.position - Camera.main.transform.position,Color.red);
           if(Physics.Raycast(ray,out hit,1000f))
           {
               Debug.Log(hit.collider.gameObject.name);
           }
       */
        if (Physics.Linecast(transform.position, Camera.main.transform.position,out hit))
        {
            if (hit.collider.gameObject.tag == "Target")
            {
                hit.collider.gameObject.GetComponent<CheckCollision>().TargetCollision();
               // Debug.Log("blocked by" + hit.collider.gameObject.name);
            }
        }
    }
}
