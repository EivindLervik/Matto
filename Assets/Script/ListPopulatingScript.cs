using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListPopulatingScript : MonoBehaviour {

    public int amount;
    public float spaceBetween;
    public float indentFromEdges;
    public float height;
    public GameObject item;

    private List<GameObject> items;

	void Start () {
        items = new List<GameObject>();

        for (int i = 0; i < amount; i++)
        {
            MakeGraphix(i);
        }

    }

    public GameObject MakeGraphix(int index)
    {
        GameObject go = Instantiate(item, transform);
        RectTransform rt = go.GetComponent<RectTransform>();

        rt.offsetMin = new Vector2(indentFromEdges, -indentFromEdges - ((height * (index + 1)) + (spaceBetween * index)));
        rt.offsetMax = new Vector2(-indentFromEdges, -indentFromEdges - ((height + spaceBetween) * index));

        RectTransform rtSelf = GetComponent<RectTransform>();
        rtSelf.offsetMin = new Vector2(0.0f, ((-index - 1) * (spaceBetween + height)) - indentFromEdges + rtSelf.offsetMax.y);

        items.Add(go);

        return go;
    }

	public void AddExpression () {
        Expression e = MakeGraphix(amount).GetComponent<Expression>();
        e.id = amount;
        Controller.InsertExpression(e);
        amount++;

        e.UseExpression();
    }
}
