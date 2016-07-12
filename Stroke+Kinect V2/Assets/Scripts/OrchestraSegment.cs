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
            if (avatarController.activeJoint == 1)
                tuningArmPos = avatarController.rightElbowPos;
            else if (avatarController.activeJoint == 2)
                tuningArmPos = avatarController.elbowPos;
            else
                tuningArmPos = avatarController.rightElbowPos;
            Debug.Log(tuningArmPos.y);
        if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                tuningArmPos += new Vector3(0f, 0.1f,0f);
            }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                tuningArmPos-= new Vector3(0f, 0.1f, 0f);
            }

			//the higher the arm on either extremes, the more the volume
			orcSource.volume = Mathf.Abs (tuningArmPos.y);
            PointerCollide.volumeVal = tuningArmPos.y;
		}
	}

    public void PlaySound()
    {
    //    GetComponent<PointerCollide>().PlaySegment(this.gameObject);
    }
}
