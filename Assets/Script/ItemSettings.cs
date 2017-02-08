using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSettings : MonoBehaviour {

    public string itemName;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenProperties()
    {
        Controller.ToggleProperties(true);
        Controller.SetCurrent(this);
    }

    public void ApplySettings(string itemName)
    {
        this.itemName = itemName;
    }

    public string GetItemName()
    {
        return itemName;
    }
}
