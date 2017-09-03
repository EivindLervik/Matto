using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolsController : MonoBehaviour {

    public float menuSpeedModefier;

    public RectTransform canvas;
    public RectTransform canvasScaler;
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
    public Dropdown comparisonDD;
    public Dropdown switchDD;
    private bool propertiesOpen;
    private Element propertiesEditingElement;

    public LineScript currentDrawingLine;

    private int currentExpression;
    private List<Element> objectPool;
    private List<LineScript> linePool;
    private string propertiesHelpString;

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
        else if (e.elementType == ElementType.Switch)
        {
            startName = "Switch";
        }
        else if (e.elementType == ElementType.Comparison)
        {
            startName = "Comparison";
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
        switchDD.gameObject.SetActive(false);
        comparisonDD.gameObject.SetActive(false);

        // Set help-string
        propertiesHelpString = "msg_" + elementType.ToString().ToLower();

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

            case ElementType.Switch:
                switchDD.gameObject.SetActive(true);

                // Add up and down
                List<string> l = new List<string>();
                l.Add(GameHandler.languageHandler.GetText("tools_pro_up"));
                l.Add(GameHandler.languageHandler.GetText("tools_pro_down"));
                switchDD.ClearOptions();
                switchDD.AddOptions(l);

                switchDD.value = int.Parse(data[0]);

                break;

            case ElementType.Comparison:
                comparisonDD.gameObject.SetActive(true);

                comparisonDD.value = int.Parse(data[0]);

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

    public void ChangeSwitch()
    {
        GameHandler.dataHandler.UpdateElementData(currentExpression, propertiesEditingElement.GetIndex(), switchDD.value.ToString(), 0);
        ReLabel(propertiesEditingElement);
    }

    public void ChangeComparison()
    {
        GameHandler.dataHandler.UpdateElementData(currentExpression, propertiesEditingElement.GetIndex(), comparisonDD.value.ToString(), 0);
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

    public void GetHelp()
    {
        GameHandler.errorHandler.DisplayMessage(propertiesHelpString);
    }

    public void GetHelp(string key)
    {
        GameHandler.errorHandler.DisplayMessage("msg_" + key);
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

        // Zooming
        float sizeModefier = 0.5f * GameHandler.preferenceHandler.GetZoomSpeed();
        float delta = 0;
        if(Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            delta = prevTouchDeltaMag - touchDeltaMag;
            
            Vector3 startScale = canvasScaler.localScale;
            startScale.x -= delta * sizeModefier * Time.deltaTime;
            startScale.y -= delta * sizeModefier * Time.deltaTime;

            Vector2 scaleLimits = new Vector2(0.75f, 2.0f);
            startScale.x = Mathf.Clamp(startScale.x, scaleLimits.x, scaleLimits.y);
            startScale.y = Mathf.Clamp(startScale.y, scaleLimits.x, scaleLimits.y);

            canvasScaler.localScale = startScale;
        }

        Vector3 recover = canvas.position;
        if (canvas.position.x > 0.0f)
        {
            recover.x = 0.0f;
        }
        else if (canvas.position.x < Screen.width - (canvas.sizeDelta.x * canvasScaler.localScale.x))
        {
            recover.x = Screen.width - (canvas.sizeDelta.x * canvasScaler.localScale.x);
        }
        if (canvas.position.y < Screen.height)
        {
            recover.y = Screen.height;
        }
        
        else if (canvas.position.y > canvas.sizeDelta.y * canvasScaler.localScale.y)
        {
            recover.y = canvas.sizeDelta.y * canvasScaler.localScale.y;
        }
        
        canvas.position = recover;
        panels.position = canvas.position;
    }

    private Vector3 prevPos;
    public void MoveCanvasDown()
    {
        prevPos = Input.mousePosition;
    }
    public void MoveCanvas()
    {
        canvas.Translate((Input.mousePosition- prevPos), Space.World);

        MoveCanvasDown();
    }

    private void ReLabel(Element e)
    {
        if (e.elementType == ElementType.Operator)
        {
            e.gameObject.GetComponentInChildren<Text>().text = IntToDDLabel(operatorDD, e.GetIndex());
        }
        else if (e.elementType == ElementType.Modifier)
        {
            e.gameObject.GetComponentInChildren<Text>().text = IntToDDLabel(modifierDD, e.GetIndex());
        }
        else if (e.elementType == ElementType.Comparison)
        {
            e.gameObject.GetComponentInChildren<Text>().text = IntToDDLabel(comparisonDD, e.GetIndex());
        }
        else
        {
            e.gameObject.GetComponentInChildren<Text>().text = GameHandler.dataHandler.GetElementName(currentExpression, e.GetIndex());
        }
    }

    private string IntToDDLabel(Dropdown dd, int index)
    {
        return dd.options[GameHandler.dataHandler.GetElementDataDD(currentExpression, index)].text;
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

                    // TODO - Make special line for bools

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
