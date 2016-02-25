using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawMouse : MonoBehaviour
{
    List<Vector3> linePoints = new List<Vector3>();
    LineRenderer lineRenderer;
    public float startWidth = 1.0f;
    public float endWidth = 1.0f;
    public float threshold = 0.001f;
    public float multX = 0f;
    public float multY = 0f;
    public float subY = 0f;
    Camera thisCamera;
    int lineCount = 0;

    Vector3 lastPos = Vector3.one * float.MaxValue;


    void Awake()
    {
        thisCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = thisCamera.nearClipPlane;
     //   Debug.Log("Mouse Position: " + mousePos);
        Vector3 mouseWorld = thisCamera.ScreenToWorldPoint(mousePos);
       // Debug.Log("Mouse World:" + mouseWorld);
    //    Debug.Log("Mouse Screen: " + thisCamera.WorldToScreenPoint(mouseWorld));
        Debug.Log(mouseWorld.y);
        mouseWorld = new Vector3(mouseWorld.x*multX, (mouseWorld.y-1f)*multY, 0f);
   //     Debug.Log(mouseWorld.y);
      // Debug.Log("After: "+mouseWorld);
        float dist = Vector3.Distance(lastPos, mouseWorld);
        if (dist <= threshold)
            return;

        lastPos = mouseWorld;
        if (linePoints == null)
            linePoints = new List<Vector3>();
        linePoints.Add(mouseWorld);

        UpdateLine();
    }


    void UpdateLine()
    {
        //Debug.Log("First element: " + linePoints[0]);
        lineRenderer.SetWidth(startWidth, endWidth);

        lineRenderer.SetVertexCount(linePoints.Count);

        for (int i = lineCount; i < linePoints.Count; i++)
        {
            lineRenderer.SetPosition(i,linePoints[i]);
        }
        lineCount = linePoints.Count;

        if(lineCount>300)
        {
            Debug.Log("LinePos: " + linePoints[0]);
          //  Debug.Log("LineRend: "+lineRenderer)

            linePoints.Reverse();
          //  Debug.Log(linePoints[0]);
            for(int i=290;i<297;i++)
            {
                if(i==0)
                {
                    Debug.Log("Removing: " +linePoints[i]);
                }
                linePoints.RemoveAt(i);
            }
            linePoints.Reverse();

            lineRenderer.SetVertexCount(0);
            lineRenderer.SetVertexCount(linePoints.Count);
            for (int i = 0; i < linePoints.Count; i++)
            {
                if(i==0)
                {
                   Debug.Log(linePoints[i]);
                }
                lineRenderer.SetPosition(i, linePoints[i]);
            }
            lineCount = linePoints.Count;
        }
    }
}