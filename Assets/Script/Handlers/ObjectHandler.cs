using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour {

    public GameObject line;
    public GameObject useInput;

    public List<GameObject> elements;

    public GameObject canvas;
    private TextChanger[] textComponents;

    public void Initialize()
    {
        textComponents = canvas.GetComponentsInChildren<TextChanger>();
        print(textComponents.Length);
    }

    public TextChanger[] GetTextComponents()
    {
        return textComponents;
    }
}
