using UnityEngine;
using System.Collections;

public class OrchestraSegment : MonoBehaviour {

	public AvatarController avatarController;
	public GameObject kinectAvatar;
	public Vector3 tuningArmPos;
	public AudioSource orcSource;
	public bool allow=false;
	// Use this for initialization
	void Start () {
	
	//	orcSource = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!avatarController.outOfBalance) {
			tuningArmPos = avatarController.rightElbowPos;
			//the higher the arm on either extremes, the more the volume
			orcSource.volume = Mathf.Abs (tuningArmPos.y);
		}
	}

    public void PlaySound()
    {
    //    GetComponent<PointerCollide>().PlaySegment(this.gameObject);
    }
}
