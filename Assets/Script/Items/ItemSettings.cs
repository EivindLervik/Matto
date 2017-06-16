﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Operator, Input, Output, Constant
}

public class ItemSettings : MonoBehaviour {
    public string itemName;
    public string description;

    protected bool canOpen;
    protected Dictionary<string, ItemSettings> inputValues;

    void Awake()
    {
        inputValues = new Dictionary<string, ItemSettings>();
    }

    public virtual void OpenProperties()
    {
        // VIRTUAL
    }

    public virtual void ApplySettings(ArrayList items)
    {
        itemName = (string) items[0];
        description = (string) items[1];
    }

    public virtual float Get()
    {
        return 0.0f;
    }

    public virtual Dictionary<string, string> GetObjectData()
    {
        Dictionary<string, string> data = new Dictionary<string, string>();

        data.Add("Name", itemName);
        data.Add("Description", description);
        data.Add("Type", "None");
        data.Add("Inputs", "None");

        return data;
    }

    public virtual void SetObjectData(Dictionary<string, string> data)
    {
        itemName = data["Name"];
        description = data["Description"];
    }



    public Dictionary<string, ItemSettings> GetInputs()
    {
        return inputValues;
    }

    public void AddInput(string name, ItemSettings itemSettings)
    {
        inputValues.Add(name, itemSettings);
    }

    public void UpdateInput(string name, ItemSettings itemSettings)
    {
        inputValues[name] = itemSettings;
    }

    public bool HasConnection(string name)
    {
        return inputValues[name] != null;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public string GetDescription()
    {
        return description;
    }

    public void SetCanOpen(bool canOpen)
    {
        this.canOpen = canOpen;
    }

    public void Drag()
    {
        transform.parent.position = Input.mousePosition;
        canOpen = false;
    }

}
