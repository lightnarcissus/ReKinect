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

    void Awake()
    {
        Cursor.visible = false;
        drawPos = kinectAvatar.GetComponent<AvatarController>().elbowPos;
        thisCamera = Camera.main;
    }

    void Update()
    {
        drawPos = kinectAvatar.GetComponent<AvatarController>().elbowPos;
       // drawPos = Input.mousePosition;
        Vector3 mousePos = drawPos;
        mousePos.z = thisCamera.nearClipPlane;
        Vector3 mouseWorld = thisCamera.ViewportToWorldPoint(mousePos);
        mouseWorld.z = thisCamera.nearClipPlane;
      //    Debug.Log(mouseWorld.y);
        mouseWorld = new Vector3((mouseWorld.x+0.2f)*multX, (mouseWorld.y-1.2f)*multY, 0f);
      //  Debug.Log("After "+mouseWorld);
        float dist = Vector3.Distance(lastPos, mouseWorld);
        if (dist <= threshold)
            return;
        trailRend.transform.position=new Vector3(mouseWorld.x,mouseWorld.y,50f);
        lastPos = mouseWorld;
        
    }

}