using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineScript : MonoBehaviour {

    public Handle connect1;
    public Handle connect2;

    private RectTransform trans;
    private bool placed;

    private void Start()
    {
        trans = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (connect1 != null)
        {
            transform.position = connect1.transform.position;
        }

        if (placed)
        {
            Vector3 pos = connect2.transform.position;
            float panelScale = connect1.GetElement().GetCallBack().canvasScaler.localScale.x;
            float modifier = Mathf.Sign(Vector3.Dot(Vector3.left, (pos - trans.position).normalized));
            float angle = Vector3.Angle(Vector3.up, (pos - trans.position).normalized);
            trans.localEulerAngles = new Vector3(0.0f, 0.0f, modifier * angle);
            trans.sizeDelta = new Vector2(trans.sizeDelta.x, Vector3.Distance(trans.position, pos) / panelScale);
        }
        else
        {
            Vector3 pos = Input.mousePosition;
            if (Input.touchCount > 0)
            {
                pos = Input.GetTouch(0).position;
            }

            float panelScale = connect1.GetElement().GetCallBack().canvasScaler.localScale.x;
            float modifier = Mathf.Sign(Vector3.Dot(Vector3.left, (pos - trans.position).normalized));
            float angle = Vector3.Angle(Vector3.up, (pos - trans.position).normalized);
            trans.localEulerAngles = new Vector3(0.0f, 0.0f, modifier * angle);
            trans.sizeDelta = new Vector2(trans.sizeDelta.x,  Vector3.Distance(trans.position, pos) / panelScale);
            print(panelScale);
        }
    }

    private void LateUpdate()
    {
        if(Input.touchCount < 1 && !Input.GetMouseButton(0) && connect2 == null)
        {
            Destroy(gameObject);
        }
    }

    public void SetConnect1(Handle h)
    {
        connect1 = h;

        if (connect1.handleType == HandleType.BoolIn || connect1.handleType == HandleType.BoolOut)
        {
            GetComponent<Image>().color = new Color(0.0f, 150.0f, 255.0f);
        }
    }

    public void SetConnect2(Handle h)
    {
        connect2 = h;
    }

    public void SetPlaced(bool save)
    {
        placed = true;
        connect1.SetOtherHandle(connect2, save);
        connect2.SetOtherHandle(connect1, save);
        connect1.GetElement().GetCallBack().AddLineToPool(this);
    }

    public void Delete()
    {
        connect1.DeleteOtherHandle(connect2);
        connect2.DeleteOtherHandle(connect1);
        connect1.GetElement().GetCallBack().DeleteLine(this);
        Destroy(gameObject);
    }

    public bool ConnectedTo(Element e)
    {
        if(connect1.GetElement() == e || connect2.GetElement() == e)
        {
            return true;
        }
        return false;
    }

}
