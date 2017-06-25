using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandleType
{
    In, Out, BoolIn, BoolOut
}

public class Handle : MonoBehaviour {

    public HandleType handleType;

    private Element element;
    private List<Handle> otherHandle;

    private void Start()
    {
        if (otherHandle == null)
        {
            Setup();
        }
    }

    private void Setup()
    {
        otherHandle = new List<Handle>();
        element = transform.parent.GetComponentInChildren<Element>();
    }

    public void Enter()
    {
        if(otherHandle.Count > 0 && (handleType == HandleType.In || handleType == HandleType.BoolIn))
        {

        }
        else
        {
            RectTransform trans = null;
            if (handleType == HandleType.BoolIn || handleType == HandleType.BoolOut)
            {
                trans = Instantiate(GameHandler.objectHandler.line, element.GetCallBack().lines).GetComponent<RectTransform>();
            }
            else
            {
                trans = Instantiate(GameHandler.objectHandler.line, element.GetCallBack().lines).GetComponent<RectTransform>();
            }
            trans.transform.position = transform.position;

            LineScript ls = trans.gameObject.GetComponent<LineScript>();
            element.GetCallBack().currentDrawingLine = ls;

            ls.SetConnect1(this);
        }
    }

    public void Drop()
    {
        LineScript ls = element.GetCallBack().currentDrawingLine;
        ls.connect2 = this;

        // Check if the connection is in cross
        bool cross = false;
        if(handleType == HandleType.In || handleType == HandleType.BoolIn)
        {
            if(element.GetHandleOut() != null)
            {
                foreach (Handle h in element.GetHandleOut().otherHandle)
                {
                    if (h.element == ls.connect1.element)
                    {
                        cross = true;
                        break;
                    }
                }
            }
        }
        else
        {
            foreach(Handle parentH in element.GetHandels())
            {
                foreach (Handle h in parentH.otherHandle)
                {
                    if (h.element == ls.connect1.element)
                    {
                        cross = true;
                        break;
                    }
                }
            }
        }
        

        if (ls.connect1.element == ls.connect2.element ||
            ls.connect1.handleType == ls.connect2.handleType ||
            (otherHandle.Count > 0 && (handleType == HandleType.In || handleType == HandleType.BoolIn)) ||
            cross
            )
        {
            ls.connect2 = null;
        }
        else if (((ls.connect1.handleType == HandleType.BoolIn || ls.connect1.handleType == HandleType.BoolOut) && 
            (ls.connect2.handleType == HandleType.In || ls.connect2.handleType == HandleType.Out)) ||
            ((ls.connect2.handleType == HandleType.BoolIn || ls.connect2.handleType == HandleType.BoolOut) &&
            (ls.connect1.handleType == HandleType.In || ls.connect1.handleType == HandleType.Out))
            )
        {
            // Connect bool to values
            ls.connect2 = null;
        }
        else
        {
            ls.SetPlaced(true);
        }
    }

    public void SetOtherHandle(Handle h, bool save)
    {
        if(otherHandle == null)
        {
            Setup();
        }

        otherHandle.Add(h);

        if (save && (handleType == HandleType.In || handleType == HandleType.BoolIn))
        {
            element.SaveNewConnection(this, otherHandle[0].element);
        }
    }

    public void DeleteOtherHandle(Handle h)
    {
        if (handleType == HandleType.In || handleType == HandleType.BoolIn)
        {
            element.DeleteConnection(this, otherHandle[0].element);
        }

        otherHandle.Remove(h);
    }

    public Element GetElement()
    {
        return element;
    }
}
