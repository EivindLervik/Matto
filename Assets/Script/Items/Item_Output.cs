using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Output : ItemSettings {

    private InputField target;

    public override void OpenProperties()
    {
        if (canOpen)
        {
            base.OpenProperties();
            Controller.ToggleProperties(true, ItemType.Output, this);
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
        float value = 0.0f;

        try
        {
            value = inputValues["A"].Get();
        }
        catch(NullReferenceException e)
        {
            // Handtere feil
            Debug.LogWarning("Missing a connection! Data: " + e);
        }

        return value;
    }




    // ACCESSERS

    public InputField GetTarget()
    {
        return target;
    }

    public void SetTarget(InputField target)
    {
        this.target = target;
    }
}
