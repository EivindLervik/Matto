using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreferencesController : MonoBehaviour {

    public InputField username;
    public InputField zoomSpeed;

    public void Populate()
    {
        username.text = GameHandler.preferenceHandler.GetUsername();
        if (!username.text.Equals(""))
        {
            zoomSpeed.text = GameHandler.preferenceHandler.GetZoomSpeed().ToString();
        }
    }

    public void Save()
    {
        if(username.text.Length > 0)
        {
            GameHandler.preferenceHandler.UpdatePreferences(username.text, float.Parse(zoomSpeed.text));
            GameHandler.screenHandler.OpenMainMenu();
        }
        else
        {
            GameHandler.errorHandler.ThrowError(ErrorType.InvalidUsername);
        }
    }

}
