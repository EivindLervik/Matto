using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseInput : MonoBehaviour {

    public Text nameField;
    public InputField inputField;

    private ElementData element;


    public void SetData(bool interactable, ElementData element)
    {
        inputField.interactable = interactable;
        this.element = element;

        nameField.text = element.name;
        inputField.text = element.value.ToString();
    }

    public bool Save()
    {
        float value = 0;
        try
        {
            value = float.Parse(inputField.text);
        }
        catch(FormatException e)
        {
            Debug.LogWarning("Tried to write something illegal: " + e);
            return false;
        }
        SetValue(value);

        return true;
    }

    public void Run()
    {
        SetValue(element.GetValue(new Stack<ElementData>()));
    }

    private void SetValue(float value)
    {
        GameHandler.dataHandler.UpdateElementValue(element, value);
        inputField.text = value.ToString();
    }

}
