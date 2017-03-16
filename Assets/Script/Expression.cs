using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Expression : MonoBehaviour {

    public int id;

    // Inputs
    private List<GameObject> inputsGraphix;
    private List<Item_Input> inputs;

    // Saved graphix from the layout
    private List<GameObject> layoutGraphix;

    // Outputs
    private List<GameObject> outputsGraphix;
    private List<Item_Output> outputs;

	void Start () {
        inputsGraphix = new List<GameObject>();
        inputs = new List<Item_Input>();
        layoutGraphix = new List<GameObject>();
        outputsGraphix = new List<GameObject>();
        outputs = new List<Item_Output>();
	}

	public void RunExpression()
    {
        int index = 0;

        foreach (GameObject field in inputsGraphix)
        {
            // TODO Sjekk at det ikkje vert skrive inn noko anna enn tall
            inputs[index].SetValue(float.Parse(field.GetComponent<Text>().text));
            index++;
        }

        index = 0;

        foreach (Item_Output item in outputs)
        {
            outputsGraphix[index].GetComponent<Text>().text = item.Get().ToString();
            index++;
        }
    }

    

    // ACCESSERS

    public void InsertInput(GameObject go)
    {
        inputsGraphix.Add(go);
        layoutGraphix.Add(go);
    }

    public void InsertGraphix(GameObject go)
    {
        layoutGraphix.Add(go);
    }

    public void InsertOutput(GameObject go)
    {
        outputsGraphix.Add(go);
        layoutGraphix.Add(go);
    }



    // Clicked
    public void UseExpression()
    {
        Controller.ToggleExpressionUse();
    }
}
