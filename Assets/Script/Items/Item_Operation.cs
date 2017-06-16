using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Operator
{
    Plus, Minus, Multiplication, Division
}

public class Item_Operation : ItemSettings {

    [Header("Operation")]
    public Operator itemOperator;

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

        string newText = "";
        switch (itemOperator)
        {
            case Operator.Plus:
                newText = "+";
                break;
            case Operator.Minus:
                newText = "-";
                break;
            case Operator.Multiplication:
                newText = "*";
                break;
            case Operator.Division:
                newText = "/";
                break;
            default:
                Debug.LogWarning("This operator is not supported!");
                newText = "Error";
                break;
        }

        GetComponentInChildren<Text>().text = newText;
    }



    public Operator GetItemOperator()
    {
        return itemOperator;
    }

    /**
        This method retrives the value from the previous object
    **/
    public override float Get()
    {
        float[] values = new float[inputValues.Count];

        values[0] = inputValues["A"].Get();
        values[1] = inputValues["B"].Get();

        return DoMath(values);
    }

    public override Dictionary<string, string> GetObjectData()
    {
        Dictionary<string, string> data = base.GetObjectData();

        data["Type"] = "Operation";
        data["Inputs"] = "";

        return data;
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
