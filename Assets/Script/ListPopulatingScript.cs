using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListPopulatingScript : MonoBehaviour {

    public int amount;
    public float spaceBetween;
    public float indentFromEdges;
    public float height;
    public GameObject item;

    private ArrayList items;

	void Start () {
        RectTransform rtSelf = GetComponent<RectTransform>();
        rtSelf.offsetMin = new Vector2(0.0f, (-amount * (spaceBetween + height)) - indentFromEdges);


        for (int i=0; i<amount; i++)
        {
            GameObject go = Instantiate(item, transform);
            RectTransform rt = go.GetComponent<RectTransform>();

            rt.offsetMin = new Vector2(indentFromEdges, -indentFromEdges - ((height * (i + 1)) + (spaceBetween * i)));
            rt.offsetMax = new Vector2(-indentFromEdges, -indentFromEdges - ((height + spaceBetween) * i));
            
        }
	}

	void Update () {
		
	}
}
