using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {

    public ElementType elementType;

    private int index;
    private ToolsController callBack;

    // Line
    private List<Handle> handleIn;
    private Handle handleOut;

    private bool drag;

    private void Start()
    {
        if (handleIn == null)
        {
            SetupHandles();
        }
    }

    private void SetupHandles()
    {
        handleIn = new List<Handle>();
        foreach (Handle h in transform.parent.GetComponentsInChildren<Handle>())
        {
            if (h.handleType == HandleType.Out)
            {
                handleOut = h;
            }
            else
            {
                handleIn.Add(h);
            }
        }
    }

    public void DecrementIndex()
    {
        index--;
    }

    public int GetHandleCount()
    {
        return handleIn.Count;
    }

    public List<Handle> GetHandels()
    {
        return handleIn;
    }

    public Handle GetHandleOut()
    {
        return handleOut;
    }

    public void SetData(int index, ToolsController callBack)
    {
        this.index = index;
        this.callBack = callBack;

        if(handleIn == null)
        {
            SetupHandles();
        }
    }

    public string FormatData()
    {
        return "";
    }

    public int GetIndex()
    {
        return index;
    }

    public void OpenProperties()
    {
        if (drag)
        {
            drag = false;
        }
        else
        {
            callBack.OpenProperties(
                this, 
                elementType,
                GameHandler.dataHandler.GetElementName(callBack.GetCurrentExpression(), index),
                GameHandler.dataHandler.GetElementDesc(callBack.GetCurrentExpression(), index),
                GameHandler.dataHandler.GetElementData(callBack.GetCurrentExpression(), index)
                );
        }
    }

    public void Drag()
    {
        if(Input.touchCount > 0)
        {
            transform.parent.position = Input.GetTouch(0).position;
        }
        else
        {
            transform.parent.position = Input.mousePosition;
        }

        GameHandler.dataHandler.UpdateElementPos(callBack.GetCurrentExpression(), index, new Vector2(transform.parent.localPosition.x, transform.parent.localPosition.y));
        drag = true;
    }

    public void Delete()
    {
        Destroy(transform.parent.gameObject);
    }

    public ToolsController GetCallBack()
    {
        return callBack;
    }

    public void SaveNewConnection(Handle h, Element other)
    {
        GameHandler.dataHandler.UpdateInputs(
            callBack.GetCurrentExpression(),
            index,
            handleIn.IndexOf(h),
            other.index
            );
    }

    public void DeleteConnection(Handle h, Element other)
    {
        GameHandler.dataHandler.DeleteInput(callBack.GetCurrentExpression(), index, handleIn.IndexOf(h));
    }

}
