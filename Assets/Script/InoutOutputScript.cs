using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InoutOutputScript : MonoBehaviour {

    public GameObject line;
    private InoutOutputScript target;

	void Start () {
        DrawLine(GetComponent<RectTransform>().position, new Vector2(0,0));
    }

	void Update () {
		
	}

    void DrawLine(Vector2 start, Vector2 finish){
        GameObject go = Instantiate(line);
        go.transform.parent = transform;
        RectTransform rt = go.GetComponent<RectTransform>();

        rt.position = start;
        float dist = Vector2.Distance(start, finish);
        float angle = Vector2.Angle(transform.up, (finish - start).normalized);
        print(dist + " | " + start + " og " + finish + " er " + angle);
        rt.offsetMax = new Vector2(0.0f, dist);
        //rt.offsetMin = new Vector2(dist / 2.0f, dist / 2.0f);

        rt.Rotate(0.0f, 0.0f, angle);
    }  
}
