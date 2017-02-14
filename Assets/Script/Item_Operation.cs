using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Operator
{
    Plus, Minus, Multiplication, Division
}

public class Item_Operation : ItemSettings {

    [Header("Operation")]
    public Operator itemOperator;
    public int inputs;

    public override void OpenProperties()
    {
        if (canOpen)
        {
            base.OpenProperties();
            Controller.ToggleProperties(true, ItemType.Operator, this);
        }
    }

    public override void ApplySettings(ArrayList items)
    {
        base.ApplySettings(items);
        itemOperator = (Operator) items[2];
        inputs = (int)items[3];
    }

    public Operator GetItemOperator()
    {
        return itemOperator;
    }

    public int GetInputs()
    {
        return inputs;
    }
}
