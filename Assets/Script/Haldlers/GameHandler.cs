using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public static AudioHandler audioHandler;
    public static DataHandler dataHandler;
    public static ScreenHandler screenHandler;
    public static ObjectHandler objectHandler;

    private void Start()
    {
        audioHandler = GetComponentInChildren<AudioHandler>();
        dataHandler = GetComponentInChildren<DataHandler>();
        screenHandler = GetComponentInChildren<ScreenHandler>();
        objectHandler = GetComponentInChildren<ObjectHandler>();

        screenHandler.OpenList();
    }

}
