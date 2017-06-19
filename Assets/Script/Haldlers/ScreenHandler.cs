﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHandler : MonoBehaviour {

    public GameObject list;
    public GameObject usage;
    public GameObject tools;

    private ListController listController;
    private UsageController usageController;
    private ToolsController toolsController;

    private void Start()
    {
        listController = list.GetComponent<ListController>();
        usageController = usage.GetComponent<UsageController>();
        toolsController = tools.GetComponent<ToolsController>();

        DisableAll();
    }

    public void OpenList()
    {
        DisableAll();
        list.SetActive(true);
        listController.RefreshMe();
    }

    public void OpenUsage(int expression)
    {
        DisableAll();
        usage.SetActive(true);
        usageController.SetCurrentExpression(expression);
    }

    public void OpenTools(int expression)
    {
        DisableAll();
        tools.SetActive(true);
        toolsController.SetCurrentExpression(expression);
    }

    private void DisableAll()
    {
        list.SetActive(false);
        usage.SetActive(false);
        tools.SetActive(false);
    }
}