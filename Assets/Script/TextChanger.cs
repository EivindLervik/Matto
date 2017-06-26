using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour {

    public string key;

    private Text component;
    private bool change;

    private void Start()
    {
        GetComp();
    }

    private void GetComp()
    {
        component = GetComponent<Text>();
    }

    private void Update () {
        if (change)
        {
            LanguageHandler h = GameHandler.languageHandler;
            if (h != null)
            {
                component.text = h.GetText(key);
                change = false;
            }
        }
    }

    public void Change()
    {
        LanguageHandler h = GameHandler.languageHandler;
        if (h != null)
        {
            if(component == null)
            {
                GetComp();
            }

            component.text = h.GetText(key);
            change = false;
        }
        else
        {
            change = true;
        }
    }
}
