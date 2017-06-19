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

    public void Changed()
    {
        float value = 0;
        if (!inputField.text.Equals(""))
        {
            value = float.Parse(inputField.text);
        }
        SetValue(value);
    }

    public void Run()
    {
        print("Did run");
        if (!inputField.interactable)
        {
            SetValue(element.GetValue());
            
        }
    }

    private void SetValue(float value)
    {
        GameHandler.dataHandler.UpdateElementValue(element, value);
        inputField.text = value.ToString();
    }

}
