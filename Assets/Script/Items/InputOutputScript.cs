using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputOutputScript : MonoBehaviour {

    public GameObject line;
    public Transform linePanel;

    private Transform fromPoint;
    private LineScript currentLine;

    // Data
    //private ItemSettings from;
    private ItemSettings to;

	void Update () {
		if(currentLine != null)
        {
            currentLine.SetStartAndTempFinish(fromPoint, Input.mousePosition);
        }
	}

    void LateUpdate()
    {
        if(currentLine != null)
        {
            if (Input.touchCount == 0 && !Input.GetMouseButton(0))
            {
                Drop(fromPoint);
            }
        }
    }

    void DrawLine(){
        GameObject go = Instantiate(line, linePanel);
        currentLine = go.GetComponent<LineScript>();
        currentLine.transform.position = fromPoint.position;
    }

    public void Enter(Transform fromPoint)
    {
        this.fromPoint = fromPoint;

        DrawHandleScript dhs = fromPoint.GetComponent<DrawHandleScript>();

        if (dhs.inputName.Equals("OUT"))
        {
            to = dhs.settings;
            DrawLine();
        }
        else
        {
            if (!dhs.HasConnection())
            {
                DrawLine();
            }
        }
    }

    public void Drop(Transform toPoint)
    {
        DrawHandleScript dhs = toPoint.GetComponent<DrawHandleScript>();
        DrawHandleScript dhsFrom = fromPoint.GetComponent<DrawHandleScript>();

        // Rules for placement
        if (toPoint == fromPoint || 
            (!dhs.inputName.Equals("OUT") && dhs.HasConnection()) || 
            (!dhs.inputName.Equals("OUT") && !dhsFrom.inputName.Equals("OUT")) ||
            (dhs.inputName.Equals("OUT") && dhsFrom.inputName.Equals("OUT")) || 
            (dhs.settings == dhsFrom.settings)
            )
        {
            Destroy(currentLine.gameObject);
        }
        else
        {
            if (dhs.inputName.Equals("OUT"))
            {
                fromPoint.GetComponent<DrawHandleScript>().SetupConnection(dhs.settings);
            }
            else
            {
                dhs.SetupConnection(to);
            }
            
            currentLine.SetStartAndFinish(fromPoint, toPoint);
            currentLine.Add();
            Controller.InsertGraphix(currentLine.gameObject);
        }

        currentLine = null;
    }
}
