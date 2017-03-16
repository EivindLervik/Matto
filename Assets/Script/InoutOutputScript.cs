using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InoutOutputScript : MonoBehaviour {

    public GameObject line;
    public Transform fromPoint;
    private ArrayList targets;

    private GameObject currentLine;

    void Start () {
        //DrawLine(fromPoint.position, new Vector2(0,0));
    }

	void Update () {
		if(currentLine != null)
        {
            Vector3 start = fromPoint.position;
            Vector3 finish = Input.mousePosition;

            RectTransform rt = currentLine.GetComponent<RectTransform>();
            rt.position = start;
            float dist = Vector2.Distance(start, finish) + 30;
            float angle = Vector2.Angle(Vector3.up, (finish - start).normalized);
            print(dist + " | " + start + " og " + finish + " er " + angle);
            rt.offsetMax = new Vector2(0.0f, dist);
            
            if(Vector3.Dot(Vector3.right, (finish - start).normalized) >= 0.0f)
            {
                rt.eulerAngles = new Vector3(0, 0, -angle);
            }
            else
            {
                rt.eulerAngles = new Vector3(0, 0, angle);
            }

        }
	}

    void DrawLine(){
        currentLine = Instantiate(line);
        currentLine.transform.SetParent(transform);
    }

    public void Enter()
    {
        DrawLine();
    }

    public void Drop()
    {
        currentLine = null;
    }
}
