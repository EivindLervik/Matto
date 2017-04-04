using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsageScript : MonoBehaviour {

    public InputField expressionNameField;
    public GameObject inputAndOutputFieldPrefab;
    public RectTransform inputField;
    public RectTransform outputField;

    private Expression expression;

    public void SetData(Expression expression)
    {
        this.expression = expression;
        expressionNameField.text = expression.expressionName;

        // Input fields
        int index = 0;
        foreach(Item_Input i in expression.GetInputs())
        {
            GameObject go = Instantiate(inputAndOutputFieldPrefab, inputField);
            go.GetComponent<RectTransform>().localPosition = NextPos(index);

            i.SetTarget(go.GetComponent<InputField>());

            go.GetComponent<USE_InputOutput>().nameText.text = i.itemName;

            index++;
        }
        inputField.sizeDelta = new Vector2(NextXSize(index), inputField.sizeDelta.y);

        // Output fields
        index = 0;
        foreach (Item_Output o in expression.GetOutputs())
        {
            GameObject go = Instantiate(inputAndOutputFieldPrefab, outputField);
            go.GetComponent<RectTransform>().localPosition = NextPos(index);

            o.SetTarget(go.GetComponent<InputField>());

            go.GetComponent<USE_InputOutput>().nameText.text = o.itemName;

            index++;
        }
        outputField.sizeDelta = new Vector2(NextXSize(index), outputField.sizeDelta.y);
    }

	public void GoBack()
    {
        ClearIO();
        Controller.ToggleExpressionList(expression);
    }

    public void EditExpression()
    {
        ClearIO();
        Controller.ToggleExpressionEditor(expression);
    }

    public void RunExpression()
    {
        expression.RunExpression();
    }



    private void ClearIO()
    {
        // Save name on clear
        expression.expressionName = expressionNameField.text;

        // Remove graphix
        foreach (Item_Input i in expression.GetInputs())
        {
            Destroy(i.GetTarget().gameObject);
            i.SetTarget(null);
        }
        foreach (Item_Output o in expression.GetOutputs())
        {
            Destroy(o.GetTarget().gameObject);
            o.SetTarget(null);
        }
    }

    private Vector3 NextPos(int index)
    {
        return new Vector3(100.0f + (170.0f * index), 0.0f, 0.0f);
    }

    private float NextXSize(int index)
    {
        return 200.0f + (170.0f * (index - 1));
    }
}
