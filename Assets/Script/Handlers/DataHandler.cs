using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataHandler : MonoBehaviour {

    private const string EXPRESSION_FILE_NAME = "/expressions.dat";

    private static FileData allData;

    private void Start()
    {
        LoadAllExpressions();
    }

    private void LoadAllExpressions()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream loadFile = null;
        if (File.Exists(Application.persistentDataPath + EXPRESSION_FILE_NAME)){
            loadFile = File.Open(Application.persistentDataPath + EXPRESSION_FILE_NAME, FileMode.Open);
        }
        else
        {
            loadFile = File.Create(Application.persistentDataPath + EXPRESSION_FILE_NAME);
        }
        

        try
        {
            allData = (FileData)bf.Deserialize(loadFile);
        }
        catch (SerializationException e)
        {
            Debug.LogWarning("No data saved: " + e);
            allData = new FileData();
            allData.expressions = new List<ExpressionData>();
        }
        

        loadFile.Close();
    }

    private void SaveExpressions()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = File.Open(Application.persistentDataPath + EXPRESSION_FILE_NAME, FileMode.Open);

        bf.Serialize(saveFile, allData);
        saveFile.Close();
    }




    // Getters
    public int GetExpressionCount()
    {
        return allData.expressions.Count;
    }

    public string GetExpressionName(int index)
    {
        return allData.expressions[index].title;
    }

    // Expressions
    public int NewExpression()
    {
        if(allData.expressions == null)
        {
            allData.expressions = new List<ExpressionData>();
        }

        ExpressionData ed = new ExpressionData();
        ed.elements = new List<ElementData>();
        allData.expressions.Add(ed);

        SaveExpressions();

        return allData.expressions.Count - 1;
    }

    public void DeleteExpression(int index)
    {
        allData.expressions.RemoveAt(index);
        SaveExpressions();
    }

    public void ChangeExpressionName(int index, string newName)
    {
        allData.expressions[index].title = newName;
        SaveExpressions();
    }

    // Elements
    public void NewElement(int expression, Vector2 pos, string name, string desc, ElementType elementType, List<string> data, int inputCapacity)
    {
        ElementData element = new ElementData();

        element.positionX = pos.x;
        element.positionY = pos.y;
        element.name = name;
        element.desc = desc;
        element.type = elementType;
        element.data = data;
        element.inputs = new ElementData[inputCapacity];

        allData.expressions[expression].elements.Add(element);

        SaveExpressions();
    }

    public void UpdateElement(int expression, int elementIndex, Vector2 pos, string name, string desc, List<string> data)
    {
        ElementData element = allData.expressions[expression].elements[elementIndex];

        element.positionX = pos.x;
        element.positionY = pos.y;
        element.name = name;
        element.desc = desc;
        element.data = data;

        SaveExpressions();
    }

    public void UpdateElementPos(int expression, int elementIndex, Vector2 pos)
    {
        ElementData element = allData.expressions[expression].elements[elementIndex];

        element.positionX = pos.x;
        element.positionY = pos.y;

        SaveExpressions();
    }

    public void UpdateElementName(int expression, int elementIndex, string name)
    {
        ElementData element = allData.expressions[expression].elements[elementIndex];

        element.name = name;

        SaveExpressions();
    }

    public void UpdateElementDesc(int expression, int elementIndex, string desc)
    {
        ElementData element = allData.expressions[expression].elements[elementIndex];

        element.desc = desc;

        SaveExpressions();
    }

    public void UpdateElementData(int expression, int elementIndex, List<string> data)
    {
        ElementData element = allData.expressions[expression].elements[elementIndex];

        element.data = data;

        SaveExpressions();
    }

    public void UpdateElementData(int expression, int elementIndex, string data, int index)
    {
        ElementData element = allData.expressions[expression].elements[elementIndex];

        element.data[index] = data;

        SaveExpressions();
    }

    public void UpdateInputs(int expression, int elementIndex, int inputIndex, int connectIndex)
    {
        //print(expression + " - " + elementIndex + " - " + inputIndex + " - " + connectIndex);
        //print(allData.expressions.Count + " - " +
        //    allData.expressions[expression].elements.Count + " - " +
        //    allData.expressions[expression].elements[elementIndex].inputs.Length
        //    );
        allData.expressions[expression].elements[elementIndex].inputs[inputIndex] = allData.expressions[expression].elements[connectIndex];
        SaveExpressions();
    }

    public void DeleteInput(int expression, int elementIndex, int inputIndex)
    {
        allData.expressions[expression].elements[elementIndex].inputs[inputIndex] = null;
        SaveExpressions();
    }

    public void UpdateElementValue(int expression, int elementIndex, float value)
    {
        ElementData element = allData.expressions[expression].elements[elementIndex];

        element.value = value;

        SaveExpressions();
    }

    public void UpdateElementValue(ElementData element, float value)
    {
        element.value = value;

        SaveExpressions();
    }

    public void DeleteElement(int expression, int elementIndex)
    {
        allData.expressions[expression].elements.RemoveAt(elementIndex);
        SaveExpressions();
    }

    public List<ElementData> GetElementData(int expression)
    {
        return allData.expressions[expression].elements;
    }

    public string GetElementName(int expression, int element)
    {
        return allData.expressions[expression].elements[element].name;
    }

    public string GetElementDesc(int expression, int element)
    {
        return allData.expressions[expression].elements[element].desc;
    }

    public ElementType GetElementType(int expression, int element)
    {
        return allData.expressions[expression].elements[element].type;
    }

    public float GetElementValue(int expression, int element)
    {
        return allData.expressions[expression].elements[element].value;
    }

    // All dropdowns are on data[0]
    public int GetElementDataDD(int expression, int element)
    {
        return int.Parse(allData.expressions[expression].elements[element].data[0]);
    }

    public List<string> GetElementData(int expression, int element)
    {
        return allData.expressions[expression].elements[element].data;
    }

    public int GetElementInputDestinationIndex(int expression, int element, int input)
    {
        return allData.expressions[expression].elements.IndexOf(allData.expressions[expression].elements[element].inputs[input]);
    }

    public List<ElementData> GetExpressionInputs(int expression)
    {
        return allData.expressions[expression].GetInputs();
    }

    public List<ElementData> GetExpressionOutputs(int expression)
    {
        return allData.expressions[expression].GetOutputs();
    }

}

[Serializable]
public class FileData
{
    public List<ExpressionData> expressions;
}

[Serializable]
public class ExpressionData
{
    public string title;
    public List<ElementData> elements;

    public List<ElementData> GetInputs()
    {
        List<ElementData> data = new List<ElementData>();

        foreach(ElementData ed in elements)
        {
            if(ed.type == ElementType.Input)
            {
                data.Add(ed);
            }
        }

        return data;
    }

    public List<ElementData> GetOutputs()
    {
        List<ElementData> data = new List<ElementData>();

        foreach (ElementData ed in elements)
        {
            if (ed.type == ElementType.Output)
            {
                data.Add(ed);
            }
        }

        return data;
    }
}

[Serializable]
public class ElementData
{
    public float positionX;
    public float positionY;

    public string name;
    public string desc;
    public ElementType type;
    public List<string> data;

    public ElementData[] inputs;
    public float value;

    public float GetValue(Stack<ElementData> calls)
    {
        if(calls.Contains(this)){
            GameHandler.errorHandler.ThrowError(ErrorType.Loop);
            return 0;
        }
        else
        {
            calls.Push(this);
        }

        float retur = 0;
        try
        {
            switch (type)
            {
                case ElementType.Input:
                    retur = value;
                    break;

                case ElementType.Output:
                    retur = inputs[0] == null ? 0.0f : inputs[0].GetValue(calls);
                    break;

                case ElementType.Operator:
                    float retVal = 0;

                    switch (data[0])
                    {
                        case "0": // Plus
                            foreach (ElementData ed in inputs)
                            {
                                retVal += ed.GetValue(calls);
                            }
                            break;

                        case "1": // Minus
                            retVal = inputs[0].GetValue(calls);
                            for (int i = 1; i < inputs.Length; i++)
                            {
                                retVal -= inputs[i].GetValue(calls);
                            }
                            break;

                        case "2": // Multi
                            retVal = inputs[0].GetValue(calls);
                            for (int i = 1; i < inputs.Length; i++)
                            {
                                retVal *= inputs[i].GetValue(calls);
                            }
                            break;

                        case "3": // Divide
                            retVal = inputs[0].GetValue(calls);
                            for (int i = 1; i < inputs.Length; i++)
                            {
                                retVal /= inputs[i].GetValue(calls);
                            }
                            break;

                        case "4": // Modulo
                            retVal = inputs[0].GetValue(calls);
                            for (int i = 1; i < inputs.Length; i++)
                            {
                                retVal %= inputs[i].GetValue(calls);
                            }
                            break;

                        case "5": // Power
                            retVal = Mathf.Pow(inputs[0].GetValue(calls), inputs[1].GetValue(calls));
                            break;

                        case "6": // Minimum
                            float[] valuesMax = new float[inputs.Length];
                            for (int i = 0; i < inputs.Length; i++)
                            {
                                valuesMax[i] = inputs[i].GetValue(calls);
                            }
                            retVal = Mathf.Max(valuesMax);
                            break;

                        case "7": // Maximum
                            float[] valuesMin = new float[inputs.Length];
                            for (int i = 0; i < inputs.Length; i++)
                            {
                                valuesMin[i] = inputs[i].GetValue(calls);
                            }
                            retVal = Mathf.Min(valuesMin);
                            break;
                    }

                    retur = retVal;
                    break;

                case ElementType.Constant:
                    retur = float.Parse(data[0]);
                    break;

                case ElementType.Modifier:
                    float rounder = 10000.0f;
                    switch (data[0])
                    {
                        case "0": // Factorial
                            float ans = inputs[0].GetValue(calls);
                            for (int i = (int)inputs[0].GetValue(calls) - 1; i > 0; i--)
                            {
                                ans *= i;
                            }
                            retur = ans;
                            break;

                        case "1": // Absolute value
                            retur = Mathf.Abs(inputs[0].GetValue(calls));
                            break;

                        case "2": // Negation
                            retur = inputs[0].GetValue(calls) * -1.0f;
                            break;

                        case "3": // Square Root
                            retur = Mathf.Sqrt(inputs[0].GetValue(calls));
                            break;

                        case "4": // Sinus
                            retur = Mathf.Round((Mathf.Sin(inputs[0].GetValue(calls) * (Mathf.PI / 180.0f))) * rounder) / rounder;
                            break;

                        case "5": // Cosinus
                            retur = Mathf.Round((Mathf.Cos(inputs[0].GetValue(calls) * (Mathf.PI / 180.0f))) * rounder) / rounder;
                            break;

                        case "6": // Tang
                            retur = Mathf.Round((Mathf.Tan(inputs[0].GetValue(calls) * (Mathf.PI / 180.0f))) * rounder) / rounder;
                            break;
                    }
                    break;

                case ElementType.Switch:
                    if(inputs[0].GetValue(calls) == 1.0f)
                    {
                        if (data[0].Equals("0"))
                        {
                            retur = inputs[1].GetValue(calls);
                        }
                        else
                        {
                            retur = inputs[2].GetValue(calls);
                        }
                    }
                    else
                    {
                        if (data[0].Equals("1"))
                        {
                            retur = inputs[1].GetValue(calls);
                        }
                        else
                        {
                            retur = inputs[2].GetValue(calls);
                        }

                    }
                    break;

                case ElementType.Comparison:
                    switch (data[0])
                    {
                        case "0":
                            retur = inputs[0].GetValue(calls) > inputs[1].GetValue(calls) ? 1.0f : 0.0f;
                            break;

                        case "1":
                            retur = inputs[0].GetValue(calls) < inputs[1].GetValue(calls) ? 1.0f : 0.0f;
                            break;

                        case "2":
                            retur = inputs[0].GetValue(calls) == inputs[1].GetValue(calls) ? 1.0f : 0.0f;
                            break;

                        case "3":
                            retur = inputs[0].GetValue(calls) >= inputs[1].GetValue(calls) ? 1.0f : 0.0f;
                            break;

                        case "4":
                            retur = inputs[0].GetValue(calls) <= inputs[1].GetValue(calls) ? 1.0f : 0.0f;
                            break;

                        case "5":
                            retur = inputs[0].GetValue(calls) != inputs[1].GetValue(calls) ? 1.0f : 0.0f;
                            break;
                    }
                    
                    break;
            }
        }
        catch (NullReferenceException e)
        {
            GameHandler.errorHandler.ThrowError(ErrorType.MissingInput);
        }

        calls.Pop();
        return retur;
    }
}

[Serializable]
public enum ElementType
{
    Input, Output, Operator, Constant, Modifier, Switch, Comparison
}