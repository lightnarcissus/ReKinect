using UnityEngine;
using System.Collections;

public class AlternateController : MonoBehaviour {

	public static bool noKinect=false;
	// Use this for initialization
	void Start () {
	
       
		if (gameObject.GetComponent<InteractionManager> () != null) {
			if (noKinect) {
				gameObject.GetComponent<InteractionManager> ().enabled = false;
			} else {
				gameObject.GetComponent<InteractionManager> ().enabled = true;
			}
		} else if (gameObject.GetComponent<DrawMouse> () != null) {
			if (noKinect)
				gameObject.GetComponent<DrawMouse> ().allowMouse= true;
			else
				gameObject.GetComponent<DrawMouse> ().allowMouse = false;
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
