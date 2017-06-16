using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Constant : ItemSettings {

    private float constantValue;

    public override void OpenProperties()
    {
        if (canOpen)
        {
            base.OpenProperties();
            Controller.ToggleProperties(true, ItemType.Constant, this);
        }
    }

    public override void ApplySettings(ArrayList items)
    {
        base.ApplySettings(items);

        constantValue = (float)items[2];

        GetComponentInChildren<Text>().text = itemName;
    }

    public override float Get()
    {
        return constantValue;
    }

    public override Dictionary<string, string> GetObjectData()
    {
        Dictionary<string, string> data = base.GetObjectData();

        data["Type"] = "Constant";
        data["Inputs"] = "";

        return data;
    }
}
