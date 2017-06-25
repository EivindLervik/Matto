using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferenceHandler : MonoBehaviour {

    private const string USERNAME = "USERNAME";
    private const string ZOOMSPEED = "ZOOMSPEED";

    public void UpdatePreferences(string username, float zoomSpeed)
    {
        SetUsername(username);
        SetZoomSpeed(zoomSpeed);
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
}
