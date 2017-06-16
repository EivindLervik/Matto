using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Expression : MonoBehaviour {

    // Name-stuff
    public string expressionName;
    public Text expressionNameText;

    // Editor-Screen Graphix
    private List<GameObject> layoutGraphix;

    // Input and output scripts
    private List<Item_Input> inputs;
    private List<Item_Output> outputs;

    public static Transform spawnLayout_Panel;
    public static Transform spawnLayout_Lines;

    public void Instantiate () {
        layoutGraphix = new List<GameObject>();
        inputs = new List<Item_Input>();
        outputs = new List<Item_Output>();

        expressionNameText = GetComponentInChildren<Text>();
        expressionName = "My expression";
	}

	public void RunExpression()
    {
        foreach (Item_Output item in outputs)
        {
            item.GetTarget().text = item.Get().ToString();
        }
        
    }

    

    // PUBLIC ACCESSERS

    public List<Item_Input> GetInputs()
    {
        return inputs;
    }

    public List<Item_Output> GetOutputs()
    {
        return outputs;
    }

    public void InsertGraphix(GameObject go)
    {
        layoutGraphix.Add(go);

        // Is it an input or an output?
        Item_Input ii = go.GetComponentInChildren<Item_Input>();
        Item_Output io = go.GetComponentInChildren<Item_Output>();
        if (ii != null)
        {
            InsertInput(ii);
        }
        else if (io != null)
        {
            InsertOutput(io);
        }
    }

    public void RemoveGraphix(GameObject go)
    {
        // Is it an input or an output?
        Item_Input ii = go.GetComponentInChildren<Item_Input>();
        Item_Output io = go.GetComponentInChildren<Item_Output>();
        if (ii != null)
        {
            RemoveInput(ii);
        }
        else if (io != null)
        {
            RemoveOutput(io);
        }

        layoutGraphix.Remove(go);
    }

    public List<GameObject> GetGraphix()
    {
        return layoutGraphix;
    }

    public List<Object> GetGraphixSerialized()
    {
        List<Object> list = new List<Object>();

        foreach (GameObject go in GetGraphix())
        {
            ItemSettings current = go.GetComponentInChildren<ItemSettings>();
            Object o = new Object();

            o.positionX = go.transform.position.x;
            o.positionY = go.transform.position.y;
            o.data = current.GetObjectData();

            list.Add(o);
        }

        return list;
    }

    public void SetGraphixFromSerialization(List<Object> objects)
    {
        foreach (Object o in objects)
        {
            GameObject go = null;
            switch (o.data["Type"])
            {
                case "Input":
                    go = Instantiate(Controller.inputPrefab, spawnLayout_Panel);
                    break;
                case "Operator":
                    go = Instantiate(Controller.operatorPrefab, spawnLayout_Panel);
                    break;
                case "Constant":
                    go = Instantiate(Controller.constantPrefab, spawnLayout_Panel);
                    break;
                case "Output":
                    go = Instantiate(Controller.outputPrefab, spawnLayout_Panel);
                    break;
                default:
                    Debug.LogWarning("Not supported");
                    break;

            }

            Transform trans = go.transform;
            trans.position = new Vector3(o.positionX, o.positionY, 0.0f);

            ItemSettings itemSettings = go.GetComponent<ItemSettings>();
            itemSettings.SetObjectData(o.data);
        }
        
    }




    // PRIVATE ACCESSERS

    private void InsertInput(Item_Input input)
    {
        inputs.Add(input);
    }

    private void RemoveInput(Item_Input input)
    {
        inputs.Remove(input);
    }

    private void InsertOutput(Item_Output output)
    {
        outputs.Add(output);
    }

    private void RemoveOutput(Item_Output output)
    {
        outputs.Remove(output);
    }



    // Clicked
    public void UseExpression()
    {
        Controller.ToggleExpressionUse(this);
    }

    public void UpdateExpressionInList()
    {
        if(expressionNameText == null)
        {
            expressionNameText = GetComponentInChildren<Text>();
        }
        expressionNameText.text = expressionName;
    }
}

[Serializable]
public class Object
{
    public float positionX;
    public float positionY;
    public Dictionary<string, string> data;
}
