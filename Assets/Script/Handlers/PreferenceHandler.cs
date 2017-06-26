using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferenceHandler : MonoBehaviour {

    private const string USERNAME = "USERNAME";
    private const string ZOOMSPEED = "ZOOMSPEED";
	private const string LANGUAGE = "LANGUAGE";

	public void UpdatePreferences(string username, float zoomSpeed, int language)
    {
        SetUsername(username);
        SetZoomSpeed(zoomSpeed);
		SetLanguage (language);
        PlayerPrefs.Save();
    }

	public string GetUsername()
    {
        return PlayerPrefs.GetString(USERNAME);
    }

    private void SetUsername(string username)
    {
        PlayerPrefs.SetString(USERNAME, username);
    }

    public float GetZoomSpeed()
    {
        return PlayerPrefs.GetFloat(ZOOMSPEED);
    }

    private void SetZoomSpeed(float zoomSpeed)
    {
        PlayerPrefs.SetFloat(ZOOMSPEED, zoomSpeed);
    }

	public Language GetLanguage(){
		return (Language)PlayerPrefs.GetInt(LANGUAGE);
	}

	private void SetLanguage(int language){
		PlayerPrefs.SetInt(LANGUAGE, language);
	}
}
