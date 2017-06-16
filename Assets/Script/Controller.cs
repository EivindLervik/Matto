using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    public PropertiesBoxScript propertiesIn;
    public MenuBoxScript menuIn;
    public UsageScript usageIn;
    public EditorPopulator editorPopulatorIn;
    public GameObject expressionListIn;
    public GameObject expressionEditorIn;
    public GameObject expressionUserIn;
    public Transform spawnLayout_Panel;
    public Transform spawnLayout_Lines;

    [Header("Prefabs")]
    public GameObject inputPrefabIn;
    public GameObject operatorPrefabIn;
    public GameObject constantPrefabIn;
    public GameObject outputPrefabIn;
    public static GameObject inputPrefab;
    public static GameObject operatorPrefab;
    public static GameObject constantPrefab;
    public static GameObject outputPrefab;

    public static PropertiesBoxScript properties;
    public static MenuBoxScript menu;
    public static UsageScript usage;
    public static EditorPopulator editorPopulator;
    public static GameObject expressionList;
    public static GameObject expressionEditor;
    public static GameObject expressionUser;

    private static int currentExpression;
    private static List<Expression> expressions;

    private static List<SavedExpression> stringExpressions;

    void Start()
    {
        properties = propertiesIn;
        menu = menuIn;
        usage = usageIn;
        editorPopulator = editorPopulatorIn;
        expressionList = expressionListIn;
        expressionEditor = expressionEditorIn;
        expressionUser = expressionUserIn;

        inputPrefab = inputPrefabIn;
        operatorPrefab = operatorPrefabIn;
        constantPrefab = constantPrefabIn;
        outputPrefab = outputPrefabIn;

        currentExpression = 0;
        expressions = new List<Expression>();

        stringExpressions = new List<SavedExpression>();

        Expression.spawnLayout_Panel = spawnLayout_Panel;
        Expression.spawnLayout_Lines = spawnLayout_Lines;
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



    /**
    Save and load
    **/

    public static void SaveExpressions()
    {
        /*
        BinaryFormatter bf = new BinaryFormatter();
        print(Application.persistentDataPath);
        FileStream file = File.Open(Application.persistentDataPath + "/expressionData.dat", FileMode.Create);
        
        stringExpressions = new List<SavedExpression>();
        foreach(Expression e in expressions)
        {
            SavedExpression se = new SavedExpression();

            se.expressionName = e.expressionName;
            //se.expressionNameText = e.expressionNameText;
            se.layoutGraphix = e.GetGraphixSerialized();

            stringExpressions.Add(se);
        }
        
        ExpressionData data = new ExpressionData();
        data.expressions = stringExpressions;

        bf.Serialize(file, data);
        file.Close();
        LoadExpressions();
        */
    }

    public static void LoadExpressions()
    {
        /*
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            print(Application.persistentDataPath);
            FileStream file = File.Open(Application.persistentDataPath + "/expressionData.dat", FileMode.Open);

            ExpressionData data = (ExpressionData)bf.Deserialize(file);

            foreach (SavedExpression s in data.expressions)
            {
                print(s.expressionName);

                foreach (Object o in s.layoutGraphix)
                {
                    print("Position X: " + o.positionX);
                    print("Position Y: " + o.positionY);

                    
                }

                print("");
            }
        }
        catch (Exception e)
        {
            print("No expressions: " + e.Data);
        }
        */
    }

    public static void LoadExpressions(ListPopulatingScript lps)
    {
        /*
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            print(Application.persistentDataPath);
            FileStream file = File.Open(Application.persistentDataPath + "/expressionData.dat", FileMode.Open);

            ExpressionData data = (ExpressionData)bf.Deserialize(file);

            foreach (SavedExpression s in data.expressions)
            {
                lps.AddExpression();
                //currentExpression
            }
        }
        catch (Exception e)
        {
            print("No expressions: " + e.Data);
        }
        */
    }
}

[Serializable]
class ExpressionData
{
    // Expressions as JSON
    public List<SavedExpression> expressions;
}

[Serializable]
class SavedExpression
{
    public string expressionName;
    public List<Object> layoutGraphix;
}
