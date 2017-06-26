using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ErrorType
{
    Loop, MissingInput, InvalidUsername
}

public class ErrorHandler : MonoBehaviour {

    public MessageMenu menu;

    private void Start()
    {
        if (menu.gameObject.activeSelf)
        {
            menu.gameObject.SetActive(false);
        }
    }

    public void ThrowError(ErrorType e)
    {
        switch (e)
        {
            case ErrorType.Loop:
                menu.Open(GameHandler.languageHandler.GetText("msg_loop_t"), GameHandler.languageHandler.GetText("msg_loop_c"));
                break;

            case ErrorType.MissingInput:
                menu.Open(GameHandler.languageHandler.GetText("msg_missing_t"), GameHandler.languageHandler.GetText("msg_missing_c"));
                break;

            case ErrorType.InvalidUsername:
                menu.Open(GameHandler.languageHandler.GetText("msg_usr_t"), GameHandler.languageHandler.GetText("msg_usr_c"));
                break;
        }
    }

}
