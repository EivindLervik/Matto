using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPopulator : MonoBehaviour {

    private Expression currentExpression;

	public void Populate(Expression expression)
    {
        currentExpression = expression;

        foreach(GameObject go in currentExpression.GetGraphix())
        {
            go.SetActive(true);
        }
    }

    public void Done()
    {
        foreach (GameObject go in currentExpression.GetGraphix())
        {
            go.SetActive(false);
        }

        Controller.ToggleExpressionUse(currentExpression);
    }


}
