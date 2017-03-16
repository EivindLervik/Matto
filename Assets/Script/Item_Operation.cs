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

    private ItemSettings[] inputValues;

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

    /**
        This method retrives the value from the previous object
    **/
    public override float Get()
    {
        int index = 0;
        float[] values = new float[inputValues.Length];

        foreach(ItemSettings item in inputValues)
        {
            values[index] = item.Get();
        }

        return DoMath(values);
    }





    // MATHEMATICS

    public float DoMath(float[] values)
    {
        switch (itemOperator)
        {
            case Operator.Plus:
                return DoAddition(values);
            case Operator.Minus:
                return DoSubtraction(values[0], values[1]);
            case Operator.Multiplication:
                return DoMultiplication(values);
            case Operator.Division:
                return DoDivision(values[0], values[1]);
            default:
                Debug.LogWarning("This operator is not supported!");
                return 0.0f;
        }
    }

    public float DoAddition(float[] values)
    {
        float total = 0.0f;
        foreach(float i in values)
        {
            total += i;
        }

        return total;
    }

    public float DoSubtraction(float a, float b)
    {
        return a-b;
    }

    public float DoMultiplication(float[] values)
    {
        float total = 1.0f;
        foreach (float i in values)
        {
            total *= i;
        }

        return total;
    }

    public float DoDivision(float a, float b)
    {
        return a/b;
    }
}
