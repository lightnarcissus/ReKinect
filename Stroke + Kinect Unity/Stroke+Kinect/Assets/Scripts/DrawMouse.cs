using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawMouse : MonoBehaviour
{
    List<Vector3> linePoints = new List<Vector3>();
    public float startWidth = 1.0f;
    public float endWidth = 1.0f;
    public float threshold = 0.001f;
    public float multX = 0f;
    public float multY = 0f;
    public float subY = 0f;
    public GameObject kinectAvatar;
    Camera thisCamera;
    int lineCount = 0;
    private Vector3 drawPos;
    Vector3 lastPos = Vector3.one * float.MaxValue;
    public GameObject trailRend;
    public AvatarController avatarController;
	public bool allowMouse=true;
    public GUIText ok;
    public Camera mainCam;
    //SINGLETON
    private static DrawMouse _instance;

    public static DrawMouse Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {

        if (_instance != null)
        {
            Debug.Log("Instance already exists!");
            return;
        }
        _instance = this;

        Cursor.visible = false;
        //  drawPos = kinectAvatar.GetComponent<AvatarController>().activeJointPos;
        thisCamera = mainCam;
    }

    void Update()
    {
		if (!avatarController.outOfBalance)
        {
			if (!allowMouse) {
                if (avatarController.activeJoint == 1)
                {
                  //  Debug.Log("left");
                    drawPos = kinectAvatar.GetComponent<AvatarController>().elbowPos;
                }
                else if (avatarController.activeJoint == 2)
                {
                //    Debug.Log("right");
                    drawPos = kinectAvatar.GetComponent<AvatarController>().rightElbowPos;
                }
                else
                    drawPos = kinectAvatar.GetComponent<AvatarController>().elbowPos;
                    //  Debug.Log(drawPos);
                    Vector3 mousePos = drawPos;
                    mousePos.z = thisCamera.nearClipPlane;
                    Vector3 mouseWorld = thisCamera.ViewportToWorldPoint(mousePos);
                    mouseWorld.z = thisCamera.nearClipPlane;
                    //    Debug.Log(mouseWorld.y);
                    mouseWorld = new Vector3((mouseWorld.x + 0.2f) * multX, (mouseWorld.y - 1.2f) * multY, 0f);
                    //  Debug.Log("After "+mouseWorld);
                    float dist = Vector3.Distance(lastPos, mouseWorld);
                    if (dist <= threshold)
                        return;
                    trailRend.transform.position = new Vector3(mouseWorld.x, mouseWorld.y, 50f);
              //  Vector3 actualMousePos = thisCamera.WorldToViewportPoint(mouseWorld);
              //  Debug.Log("ACTUAL: " + actualMousePos); 
              //  MouseControl.MouseMove(actualMousePos,ok);
                    lastPos = mouseWorld;
                }
                else
                {
                    drawPos = Input.mousePosition;
                    Vector3 tempPos = new Vector3(drawPos.x, drawPos.y, 10f);
                    Vector3 mouseWorldAlt = thisCamera.ScreenToWorldPoint(tempPos);
                    //Debug.Log (mouseWorldAlt);
                    trailRend.transform.position = mouseWorldAlt;
                    //Debug.Log (drawPos);
                }

        }
        
    }

}