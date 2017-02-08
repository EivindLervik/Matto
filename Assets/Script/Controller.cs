using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public PropertiesBoxScript propertiesIn;
    public MenuBoxScript menuIn;

    public static PropertiesBoxScript properties;
    public static MenuBoxScript menu;

    void Start()
    {
        properties = propertiesIn;
        menu = menuIn;
    }

    public static void ToggleProperties(bool state)
    {
        properties.Toggle(state);
    }

    public static void ToggleMenu(bool state)
    {
        menu.Toggle(state);
    }

    public static void SetCurrent(ItemSettings current)
    {
        properties.SetCurrent(current);
    }
}
