using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolsController : MonoBehaviour {

    public float menuSpeedModefier;

    public Transform lines;
    public Transform panels;

    public RectTransform menu;
    private bool menuOpen;
    public RectTransform properties;
    public InputField nameField;
    public InputField descField;
    public InputField valueField;
    public Dropdown operatorDD;
    public Dropdown modifierDD;
    private bool propertiesOpen;
    private Element propertiesEditingElement;

    public LineScript currentDrawingLine;

    /**
     * 
     * (0) - Input
     * (1) - Output
     * (2) - Operator
     * (3) - Constant
     * 
    **/

    private int currentExpression;
    private List<Element> objectPool;
    private List<LineScript> linePool;

    public void SetCurrentExpression(int expression)
    {
        currentExpression = expression;

        Populate();
    }

    public void Finish()
    {
        DePopulate();
        GameHandler.screenHandler.OpenUsage(currentExpression);
    }

    public void ToggleMenu()
    {
        menuOpen = !menuOpen;
    }

    public void AddElement(int index)
    {
        GameObject go = Instantiate(GameHandler.objectHandler.elements[index], panels);
        go.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f);
        Element e = go.GetComponentInChildren<Element>();
        e.SetData(objectPool.Count, this);

        objectPool.Add(e);

        // Datasaving
        List<string> baseDataValue = new List<string>();
        baseDataValue.Add("0");

        // Determen name
        string startName = go.GetComponentInChildren<Text>().text;
        if(e.elementType == ElementType.Operator)
        {
            startName = "Operator";
        }
        else if (e.elementType == ElementType.Modifier)
        {
            startName = "Modifier";
        }

        GameHandler.dataHandler.NewElement(currentExpression, new Vector2(go.transform.localPosition.x, go.transform.localPosition.y), startName, "", (ElementType) index, baseDataValue, e.GetHandleCount());

        ToggleMenu();
    }

    public void AddLineToPool(LineScript ls)
    {
        linePool.Add(ls);
    }

    public void OpenProperties(Element element, ElementType elementType, string name, string desc, List<string> data)
    {
        propertiesOpen = true;

        propertiesEditingElement = element;

        nameField.text = name;
        descField.text = desc;

        valueField.gameObject.SetActive(false);
        operatorDD.gameObject.SetActive(false);
        modifierDD.gameObject.SetActive(false);

        switch (elementType)
        {
            case ElementType.Input:
                break;
            case ElementType.Output:
                break;
            case ElementType.Operator:
                operatorDD.gameObject.SetActive(true);

                operatorDD.value = int.Parse(data[0]);

                break;
            case ElementType.Constant:
                valueField.gameObject.SetActive(true);

                valueField.text = data[0];

                break;
            case ElementType.Modifier:
                modifierDD.gameObject.SetActive(true);

                modifierDD.value = int.Parse(data[0]);

                break;
            default:
                Debug.LogWarning("Element not handeled!");
                break;
        }
    }

    public void ChangeName()
    {
        GameHandler.dataHandler.UpdateElementName(currentExpression, propertiesEditingElement.GetIndex(), nameField.text);
        ReLabel(propertiesEditingElement);
    }

    public void ChangeDesc()
    {
        GameHandler.dataHandler.UpdateElementDesc(currentExpression, propertiesEditingElement.GetIndex(), descField.text);
        ReLabel(propertiesEditingElement);
    }

    public void ChangeValue()
    {
        GameHandler.dataHandler.UpdateElementData(currentExpression, propertiesEditingElement.GetIndex(), valueField.text, 0);
        ReLabel(propertiesEditingElement);
    }

    public void ChangeOperator()
    {
        GameHandler.dataHandler.UpdateElementData(currentExpression, propertiesEditingElement.GetIndex(), operatorDD.value.ToString(), 0);
        ReLabel(propertiesEditingElement);
    }

    public void ChangeModifier()
    {
        GameHandler.dataHandler.UpdateElementData(currentExpression, propertiesEditingElement.GetIndex(), modifierDD.value.ToString(), 0);
        ReLabel(propertiesEditingElement);
    }

    public void DeleteElement()
    {
        int threshold = propertiesEditingElement.GetIndex();

        // Delete lines
        List<int> instances = new List<int>();
        for (int i = 0; i < linePool.Count; i++)
        {
            if (linePool[i].ConnectedTo(propertiesEditingElement))
            {
                instances.Add(i);
            }
        }
        int removed = 0;
        foreach (int i in instances)
        {
            linePool[i - removed].Delete();
            removed++;
        }

        foreach (Element e in objectPool)
        {
            if (e.GetIndex() > threshold)
            {
                e.DecrementIndex();
            }
        }

        propertiesEditingElement.Delete();
        GameHandler.dataHandler.DeleteElement(currentExpression, propertiesEditingElement.GetIndex());
        objectPool.RemoveAt(threshold);

        CloseProperties();
    }

    public void DeleteLine(LineScript ls)
    {
        linePool.Remove(ls);
    }

    public void CloseProperties()
    {
        propertiesOpen = false;
    }

    public int GetCurrentExpression()
    {
        return currentExpression;
    }





    private void Update()
    {
        if (menuOpen)
        {
            menu.localPosition = Vector3.Lerp(menu.localPosition, Vector3.zero, Time.deltaTime * menuSpeedModefier);
        }
        else
        {
            menu.localPosition = Vector3.Lerp(menu.localPosition, new Vector3(menu.anchorMax.x * -Screen.width, 0.0f, 0.0f), Time.deltaTime * menuSpeedModefier);
        }

        if (propertiesOpen)
        {
            properties.localPosition = Vector3.Lerp(properties.localPosition, Vector3.zero, Time.deltaTime * menuSpeedModefier);
        }
        else
        {
            properties.localPosition = Vector3.Lerp(properties.localPosition, new Vector3(properties.anchorMax.x * -Screen.width, 0.0f, 0.0f), Time.deltaTime * menuSpeedModefier);
        }
    }

    private void ReLabel(Element e)
    {
        if (e.elementType == ElementType.Operator)
        {
            e.gameObject.GetComponentInChildren<Text>().text = IntToOperatorLabel(e.GetIndex());
        }
        else if (e.elementType == ElementType.Modifier)
        {
            e.gameObject.GetComponentInChildren<Text>().text = IntToModifierLabel(e.GetIndex());
        }
        else
        {
            e.gameObject.GetComponentInChildren<Text>().text = GameHandler.dataHandler.GetElementName(currentExpression, e.GetIndex());
        }
    }

    private string IntToOperatorLabel(int index)
    {
        return operatorDD.options[GameHandler.dataHandler.GetElementDataOperator(currentExpression, index)].text;
    }

    private string IntToModifierLabel(int index)
    {
        return modifierDD.options[GameHandler.dataHandler.GetElementDataModifier(currentExpression, index)].text;
    }

    private void Populate()
    {
        objectPool = new List<Element>();
        linePool = new List<LineScript>();

        foreach(ElementData ed in GameHandler.dataHandler.GetElementData(currentExpression))
        {
            GameObject go = Instantiate(GameHandler.objectHandler.elements[(int) ed.type], panels);
            go.transform.localPosition = new Vector3(ed.positionX, ed.positionY, 0.0f);
            Element e = go.GetComponentInChildren<Element>();
            e.SetData(objectPool.Count, this);

            ReLabel(e);

            objectPool.Add(e);
        }

        int currentElement = 0;
        foreach (ElementData ed in GameHandler.dataHandler.GetElementData(currentExpression))
        {
            for(int i = 0; i < ed.inputs.Length; i++)
            {
                if(ed.inputs[i] != null)
                {
                    // Add line
                    GameObject go = Instantiate(GameHandler.objectHandler.line, lines);

                    LineScript ls = go.GetComponent<LineScript>();
                    ls.SetConnect1(objectPool[currentElement].GetHandels()[i]);
                    ls.SetConnect2(objectPool[GameHandler.dataHandler.GetElementInputDestinationIndex(currentExpression, currentElement, i)].GetHandleOut());
                    ls.SetPlaced(false);
                }
            }

            currentElement++;
        }
    }

    private void DePopulate()
    {
        foreach(Element e in objectPool)
        {
            Destroy(e.transform.parent.gameObject);
        }

        foreach (LineScript ls in linePool)
        {
            if(ls != null)
            {
                Destroy(ls.gameObject);
            }
        }
    }
}
