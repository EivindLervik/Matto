using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Input : ItemSettings {

    private InputField target;

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



    // ACCESSERS

    public float GetValue()
    {
        return float.Parse(target.text);
    }

    public void SetValue(float value)
    {
        target.text = value.ToString();
    }

    public InputField GetTarget()
    {
        return target;
    }

    public void SetTarget(InputField target)
    {
        this.target = target;
    }
}
