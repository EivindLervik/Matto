using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListController : MonoBehaviour {

    public RectTransform itemPanel;
    public GameObject expressionListItem;

    private void Start()
    {
        RefreshList();
    }

    public void RefreshMe()
    {
        RefreshList();
    }

    public void NewExpression()
    {
        GameHandler.screenHandler.OpenUsage(GameHandler.dataHandler.NewExpression());
    }

    public void UseExpression(int index)
    {
        GameHandler.screenHandler.OpenUsage(index);
    }

    public void DeleteExpression(int index)
    {
        GameHandler.dataHandler.DeleteExpression(index);

        RefreshList();
    }

    public void OpenStore()
    {
        GameHandler.screenHandler.OpenStore();
    }





    // PRIVATE

    private void RefreshList()
    {
        ClearList();

        float padding = 12.0f;
        int expressionCount = GameHandler.dataHandler.GetExpressionCount();

        RectTransform lastRect = null;
        for (int i = 0; i < expressionCount; i++)
        {
            RectTransform rect = Instantiate(expressionListItem, itemPanel).GetComponent<RectTransform>();
            rect.localPosition = new Vector3(0.0f, -((rect.sizeDelta.y + padding) * i) - padding, 0.0f);
            lastRect = rect;

            rect.GetComponent<ExpressionListItem>().Set(i, this);
        }

        if(expressionCount > 0)
        {
            itemPanel.sizeDelta = new Vector2(itemPanel.sizeDelta.x, -lastRect.localPosition.y + lastRect.sizeDelta.y + padding);
        }
    }

    private void ClearList()
    {
        foreach (Transform t in itemPanel.GetComponentsInChildren<Transform>())
        {
            if(t != itemPanel)
            {
                Destroy(t.gameObject);
            }
        }
    }

}
