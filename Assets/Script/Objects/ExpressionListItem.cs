using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionListItem : MonoBehaviour {

    public Text nameText;

    private int index;
    private ListController callBack;

    public void Set(int index, ListController callBack)
    {
        this.index = index;
        this.callBack = callBack;

        nameText.text = GameHandler.dataHandler.GetExpressionName(index);
    }

    public void Clicked()
    {
        callBack.UseExpression(index);
    }

    public void Delete()
    {
        callBack.DeleteExpression(index);
    }

}
