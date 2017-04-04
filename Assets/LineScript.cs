using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineScript : MonoBehaviour {

    private Transform start;
    private Transform finish;
    private Vector3 tempFinish;

    private RectTransform rt;

    private bool placed;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        placed = false;
    }

    void Update()
    {
        Vector3 begin = start.position;
        Vector3 end = new Vector3();

        if (!placed)
        {
            end = tempFinish;
        }
        else
        {
            end = finish.position;
        }

        rt.position = begin;

        float dist = Vector2.Distance(begin, end);
        float angle = Vector2.Angle(Vector3.up, (end - begin).normalized);

        rt.sizeDelta = new Vector2(20.0f, dist);

        if (Vector3.Dot(Vector3.right, (end - begin).normalized) >= 0.0f)
        {
            rt.eulerAngles = new Vector3(0, 0, -angle);
        }
        else
        {
            rt.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    public void SetStartAndFinish(Transform start, Transform finish)
    {
        this.start = start;
        this.finish = finish;
    }

    public void SetStartAndTempFinish(Transform start, Vector3 tempFinish)
    {
        this.start = start;
        this.tempFinish = tempFinish;
    }

    public void Add()
    {
        GetComponent<Image>().raycastTarget = true;
        placed = true;
    }

    public void Delete()
    {
        finish.GetComponent<DrawHandleScript>().SetupConnection(null);
        Controller.RemoveGraphix(gameObject);
        Destroy(gameObject);
    }
}
