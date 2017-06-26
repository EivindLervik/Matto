using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public static AudioHandler audioHandler;
    public static DataHandler dataHandler;
    public static ScreenHandler screenHandler;
    public static ObjectHandler objectHandler;
    public static ErrorHandler errorHandler;
    public static NetworkHandler networkHandler;
    public static PreferenceHandler preferenceHandler;
	public static LanguageHandler languageHandler;

    private void Start()
    {
        audioHandler = GetComponentInChildren<AudioHandler>();
        dataHandler = GetComponentInChildren<DataHandler>();
        objectHandler = GetComponentInChildren<ObjectHandler>();
        errorHandler = GetComponentInChildren<ErrorHandler>();
        networkHandler = GetComponentInChildren<NetworkHandler>();
        preferenceHandler = GetComponentInChildren<PreferenceHandler>();
		languageHandler = GetComponentInChildren<LanguageHandler>();

        screenHandler = GetComponentInChildren<ScreenHandler>();

        if (preferenceHandler.GetUsername() == "")
        {
            screenHandler.OpenPreferences();
        }
        else
        {
            screenHandler.OpenMainMenu();
        }
    }
}
