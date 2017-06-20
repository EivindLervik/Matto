using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ErrorType
{
    Loop, MissingInput
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
                menu.Open("Loop-error", "The expression has a loop somewhere and could not give an answer.");
                break;

            case ErrorType.MissingInput:
                menu.Open("Missing input", "One or more of your blocks have missing inputs.");
                break;
        }
    }

}
