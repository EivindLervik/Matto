using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBoxItemScript : MonoBehaviour {

    public Transform layout;
    public GameObject spawn;

	void Start () {
		
	}

	void Update () {
		
	}

    public void Spawn()
    {
        GameObject go = Instantiate(spawn);
        go.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f);
        go.transform.SetParent(layout);

        Controller.ToggleMenu(false);
    }
}
