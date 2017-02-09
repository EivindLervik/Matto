using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Operator, Input, Output
}

public class ItemSettings : MonoBehaviour {
    public string itemName;
    public string description;

    public virtual void OpenProperties()
    {
        // VIRTUAL
    }

    public virtual void ApplySettings(ArrayList items)
    {
        itemName = (string) items[0];
        description = (string) items[1];
    }

    public string GetItemName()
    {
        return itemName;
    }

    public string GetDescription()
    {
        return description;
    }
}
