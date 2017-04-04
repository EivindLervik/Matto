using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public PropertiesBoxScript propertiesIn;
    public MenuBoxScript menuIn;
    public UsageScript usageIn;
    public EditorPopulator editorPopulatorIn;
    public GameObject expressionListIn;
    public GameObject expressionEditorIn;
    public GameObject expressionUserIn;

    public static PropertiesBoxScript properties;
    public static MenuBoxScript menu;
    public static UsageScript usage;
    public static EditorPopulator editorPopulator;
    public static GameObject expressionList;
    public static GameObject expressionEditor;
    public static GameObject expressionUser;

    private static int currentExpression;
    private static List<Expression> expressions;

    void Start()
    {
        properties = propertiesIn;
        menu = menuIn;
        usage = usageIn;
        editorPopulator = editorPopulatorIn;
        expressionList = expressionListIn;
        expressionEditor = expressionEditorIn;
        expressionUser = expressionUserIn;

        currentExpression = 0;
        expressions = new List<Expression>();
    }

    public static void ToggleProperties(bool state, ItemType itemType, ItemSettings current)
    {
        properties.Toggle(state);
        properties.SetCorrectView(itemType);
        SetCurrent(current);
    }

    public static void ToggleMenu(bool state)
    {
        menu.Toggle(state);
    }

    public static void ToggleExpressionEditor(Expression expression)
    {
        expressionUser.SetActive(false);
        expressionEditor.SetActive(true);

        editorPopulator.Populate(expression);
    }

    public static void ToggleExpressionList(Expression expression)
    {
        expressionUser.SetActive(false);
        expressionList.SetActive(true);

        expression.UpdateExpressionInList();
    }

    public static void ToggleExpressionUse(Expression expression)
    {
        expressionUser.SetActive(true);
        expressionList.SetActive(false);
        expressionEditor.SetActive(false);

        currentExpression = expressions.IndexOf(expression);
        usage.SetData(expression);
    }

    public static void SetCurrent(ItemSettings current)
    {
        properties.SetCurrent(current);
    }




    /**
    Update Expression
    **/

    public static void InsertExpression(Expression e)
    {
        expressions.Add(e);
    }

    public static void InsertGraphix(GameObject go)
    {
        expressions[currentExpression].InsertGraphix(go);
    }

    public static void RemoveGraphix(GameObject go)
    {
        expressions[currentExpression].RemoveGraphix(go);
    }
}
