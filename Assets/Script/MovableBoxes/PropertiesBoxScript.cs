using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesBoxScript : BOX_MAIN {

    [Header("Properties")]
    public Text title;
    public InputField itemName;
    public InputField description;
    public Dropdown op;
    public InputField value;

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
                    break;
                case ItemType.Input:
                    // Something
                    break;
                case ItemType.Output:
                    // Something
                    break;
                case ItemType.Constant:
                    answer.Add(float.Parse(value.text));
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

        op.gameObject.SetActive(false);
        value.gameObject.SetActive(false);

        switch (itemType)
        {
            case ItemType.Operator:
                op.gameObject.SetActive(true);
                break;
            case ItemType.Input:
                // Something
                break;
            case ItemType.Output:
                // Something
                break;
            case ItemType.Constant:
                value.gameObject.SetActive(true);
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
                title.text = "Operator";
                break;
            case ItemType.Input:
                title.text = "Input";
                break;
            case ItemType.Output:
                title.text = "Output";
                break;
            case ItemType.Constant:
                Item_Constant ic = (Item_Constant)current;
                value.text = ic.Get().ToString();
                title.text = "Constant";
                break;
            default: Debug.LogWarning("No handeling for this operator: " + itemType.ToString());
                break;
        }
    }
}
