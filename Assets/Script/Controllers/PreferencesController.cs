using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreferencesController : MonoBehaviour {

    public InputField username;
    public InputField zoomSpeed;
	public Dropdown languages;

    public void Populate()
    {
        username.text = GameHandler.preferenceHandler.GetUsername();
        if (!username.text.Equals(""))
        {
            zoomSpeed.text = GameHandler.preferenceHandler.GetZoomSpeed().ToString();
        }

		// Populate the language-Dropdown
		Language[] vals = (Language[]) System.Enum.GetValues(typeof(Language));
		List<string> options = new List<string>();
		for (int i = 0; i < GameHandler.languageHandler.GetLanguageCount(); i++) {
			options.Add (vals[i].ToString());
		}
		languages.AddOptions (options);
    }

    public void Save()
    {
        if(username.text.Length > 0)
        {
			GameHandler.preferenceHandler.UpdatePreferences(username.text, float.Parse(zoomSpeed.text), languages.value);
            GameHandler.screenHandler.OpenMainMenu();
        }
        else
        {
            GameHandler.errorHandler.ThrowError(ErrorType.InvalidUsername);
        }
    }

}
