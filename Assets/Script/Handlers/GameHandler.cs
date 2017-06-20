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

    private void Start()
    {
        audioHandler = GetComponentInChildren<AudioHandler>();
        dataHandler = GetComponentInChildren<DataHandler>();
        screenHandler = GetComponentInChildren<ScreenHandler>();
        objectHandler = GetComponentInChildren<ObjectHandler>();
        errorHandler = GetComponentInChildren<ErrorHandler>();
        networkHandler = GetComponentInChildren<NetworkHandler>();

        screenHandler.OpenList();
    }

}
