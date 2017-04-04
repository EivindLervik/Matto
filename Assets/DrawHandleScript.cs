using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawHandleScript : MonoBehaviour {

    public InputOutputScript itemPanel;
    public string inputName;

    public ItemSettings settings;

    void Start()
    {
        itemPanel = transform.GetComponentInParent<InputOutputScript>();
        settings = GetComponentInParent<ItemSettings>();

        if(settings == null)
        {
            settings = transform.parent.GetComponentInChildren<ItemSettings>();
        }

        if (!inputName.Equals("OUT"))
        {
            settings.AddInput(inputName, null);
        }
    }

	public void Enter(Transform fromPoint)
    {
        itemPanel.Enter(fromPoint);
    }

    public void Drop(Transform toPoint)
    {
        itemPanel.Drop(toPoint);
    }

    public void SetupConnection(ItemSettings newSettings)
    {
        settings.UpdateInput(inputName, newSettings);
    }

    public bool HasConnection()
    {
        return settings.HasConnection(inputName);
    }
}
