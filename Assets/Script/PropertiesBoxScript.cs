using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesBoxScript : BOX_MAIN {

    [Header("Properties")]
    public InputField itemName;
    public InputField description;
    public Dropdown op;
    public Dropdown inputs;

    private ItemSettings current;
    private ItemType itemType;

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
            ArrayList answer = new ArrayList();
            answer.Add(itemName.text);
            answer.Add(description.text);

            switch (itemType)
            {
                case ItemType.Operator:
                    answer.Add(op.value);
                    answer.Add(inputs.value);
                    break;
                case ItemType.Input:
                    // Something
                    break;
                case ItemType.Output:
                    // Something
                    break;
                default:
                    Debug.LogWarning("No handeling for this operator: " + itemType.ToString());
                    break;
            }

            current.ApplySettings(answer);
            current = null;
        }
    }

    public void SetCorrectView(ItemType itemType)
    {
        this.itemType = itemType;
        switch (itemType)
        {
            case ItemType.Operator:
                // Something
                break;
            case ItemType.Input:
                // Something
                break;
            case ItemType.Output:
                // Something
                break;
            default:
                Debug.LogWarning("No handeling for this operator: " + itemType.ToString());
                break;
        }
    }

    public void SetCurrent(ItemSettings current)
    {
        this.current = current;

        itemName.text = current.GetItemName();
        description.text = current.GetDescription();

        switch (itemType)
        {
            case ItemType.Operator:
                Item_Operation io = (Item_Operation)current;
                op.value = (int)io.GetItemOperator();
                inputs.value = io.GetInputs();
                break;
            case ItemType.Input: 
                // Something
                break;
            case ItemType.Output: 
                // Something
                break;
            default: Debug.LogWarning("No handeling for this operator: " + itemType.ToString());
                break;
        }
    }
}
