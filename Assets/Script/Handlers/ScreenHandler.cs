using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHandler : MonoBehaviour {

    public GameObject list;
    public GameObject usage;
    public GameObject tools;
    public GameObject store;
    public GameObject preferences;
    public GameObject mainMenu;

    private ListController listController;
    private UsageController usageController;
    private ToolsController toolsController;
    private StoreController storeController;
    private PreferencesController preferencesController;

    private void Start()
    {
        listController = list.GetComponent<ListController>();
        usageController = usage.GetComponent<UsageController>();
        toolsController = tools.GetComponent<ToolsController>();
        storeController = store.GetComponent<StoreController>();
        preferencesController = preferences.GetComponent<PreferencesController>();

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

    public void OpenStore()
    {
        DisableAll();
        store.SetActive(true);
    }

    public void OpenPreferences()
    {
        DisableAll();
        preferences.SetActive(true);
        preferencesController.Populate();
    }

    public void OpenMainMenu()
    {
        DisableAll();
        mainMenu.SetActive(true);
    }

    public void DisableAll()
    {
        list.SetActive(false);
        usage.SetActive(false);
        tools.SetActive(false);
        store.SetActive(false);
        preferences.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void EnableAll()
    {
        list.SetActive(true);
        usage.SetActive(true);
        tools.SetActive(true);
        store.SetActive(true);
        preferences.SetActive(true);
        mainMenu.SetActive(true);
    }
}
