using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public PropertiesBoxScript propertiesIn;
    public MenuBoxScript menuIn;
    public GameObject expressionListIn;
    public GameObject expressionEditorIn;
    public GameObject expressionUserIn;

    public static PropertiesBoxScript properties;
    public static MenuBoxScript menu;
    public static GameObject expressionList;
    public static GameObject expressionEditor;
    public static GameObject expressionUser;

    private static int currentExpression;
    private static List<Expression> expressions;

    void Start()
    {
        properties = propertiesIn;
        menu = menuIn;
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

    public static void ToggleExpressionEditor(int index)
    {
        currentExpression = index;
        print(expressions[currentExpression]);
    }

    public static void ToggleExpressionList()
    {
        print("Went Back");
    }

    public static void ToggleExpressionUse()
    {
        print("Go to usage");
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

    public static void InsertInputs(GameObject go)
    {
        expressions[currentExpression].InsertInput(go);
    }

    public static void InsertGraphix(GameObject go)
    {
        expressions[currentExpression].InsertGraphix(go);
    }

    public static void InsertOutput(GameObject go)
    {
        expressions[currentExpression].InsertOutput(go);
    }
}
