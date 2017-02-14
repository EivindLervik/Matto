using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Output : ItemSettings {

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
}
