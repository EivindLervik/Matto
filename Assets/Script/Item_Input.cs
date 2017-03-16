using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Input : ItemSettings {

    private float value;

    public override void OpenProperties()
    {
        if (canOpen)
        {
            base.OpenProperties();
            Controller.ToggleProperties(true, ItemType.Input, this);
        }
    }

    public override void ApplySettings(ArrayList items)
    {
        base.ApplySettings(items);

        GetComponentInChildren<Text>().text = itemName;
    }

    /**
        This method retrives the value from the previous object
    **/
    public override float Get()
    {
        return GetValue();
    }



    public float GetValue()
    {
        return value;
    }

    public void SetValue(float value)
    {
        this.value = value;
    }
}
