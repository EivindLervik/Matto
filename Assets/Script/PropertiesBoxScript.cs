using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesBoxScript : BOX_MAIN {

    [Header("Properties")]
    public InputField itemName;

    private ItemSettings current;

    void Start()
    {
        GetRect();
    }

    void Update()
    {
        Move();
    }

    public override void Toggle(bool state)
    {
        base.Toggle(state);
        if (!open)
        {
            current.ApplySettings(itemName.text);
            current = null;
        }
    }

    public void SetCurrent(ItemSettings current)
    {
        this.current = current;
        itemName.text = current.GetItemName();
    }
}
