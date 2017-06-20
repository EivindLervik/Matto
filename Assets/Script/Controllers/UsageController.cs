using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsageController : MonoBehaviour {

    public InputField nameField;
    public RectTransform inputs;
    public RectTransform outputs;

    private List<UseInput> inputPool;
    private List<UseInput> outputPool;

    private int currentExpression;

    public void SetCurrentExpression(int expression)
    {
        currentExpression = expression;

        Populate();
    }

    public void ChangedName()
    {
        GameHandler.dataHandler.ChangeExpressionName(currentExpression, nameField.text);
    }

    public void GoBack()
    {
        GameHandler.screenHandler.OpenList();
        RemoveObjects();
    }

    public void RunExpression()
    {
        bool ok = true;
        foreach (UseInput ui in inputPool)
        {
            ok = ok && ui.Save();
        }

        if (ok)
        {
            foreach (UseInput ui in outputPool)
            {
                ui.Run();
            }
        }
    }

    public void EditExpression()
    {
        GameHandler.screenHandler.OpenTools(currentExpression);
        RemoveObjects();
    }



    private void RemoveObjects()
    {
        foreach (UseInput ui in inputPool)
        {
            Destroy(ui.gameObject);
        }
        foreach (UseInput ui in outputPool)
        {
            Destroy(ui.gameObject);
        }
    }

    private void Populate()
    {
        inputPool = new List<UseInput>();
        outputPool = new List<UseInput>();
        nameField.text = GameHandler.dataHandler.GetExpressionName(currentExpression);

        // Create inputs and outputs
        int created = 0;
        float elementSize = 0;
        float padding = 20.0f;
        foreach (ElementData ed in GameHandler.dataHandler.GetExpressionInputs(currentExpression))
        {
            UseInput ui = Instantiate(GameHandler.objectHandler.useInput, inputs).GetComponent<UseInput>();
            RectTransform trans = ui.GetComponent<RectTransform>();
            elementSize = trans.sizeDelta.x;
            trans.localPosition = new Vector3(padding + (created * (elementSize + padding)), 0.0f, 0.0f);

            ui.SetData(true, ed);

            inputPool.Add(ui);
            created++;
        }
        inputs.sizeDelta = new Vector2(((elementSize + padding) * created) + padding, inputs.sizeDelta.y);

        created = 0;
        foreach (ElementData ed in GameHandler.dataHandler.GetExpressionOutputs(currentExpression))
        {
            UseInput ui = Instantiate(GameHandler.objectHandler.useInput, outputs).GetComponent<UseInput>();
            RectTransform trans = ui.GetComponent<RectTransform>();
            elementSize = trans.sizeDelta.x;
            trans.localPosition = new Vector3(padding + (created * (elementSize + padding)), 0.0f, 0.0f);

            ui.SetData(false, ed);

            outputPool.Add(ui);
            created++;
        }
        outputs.sizeDelta = new Vector2(((elementSize + padding) * created) + padding, outputs.sizeDelta.y);
    }

}
