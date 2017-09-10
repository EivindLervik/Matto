using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpController : MonoBehaviour {

    public Button blocksBTN;
    public GameObject blocks;
    public Button linesBTN;
    public GameObject lines;

    public void Open(string menuType)
    {
        DisableAll();
        EnableAllButtons();

        switch (menuType)
        {
            case "Blocks":
                blocks.SetActive(true);
                blocksBTN.interactable = false;
                break;
            case "Lines":
                lines.SetActive(true);
                linesBTN.interactable = false;
                break;
            case "NONE1":
                break;
            case "NONE2":
                break;
            default:
                Debug.LogError("The menu type '" + menuType + "' is not handled!");
                break;
        }
    }



    public void Help(string key)
    {
        GameHandler.errorHandler.DisplayMessage("msg_" + key);
    }



    private void DisableAll()
    {
        blocks.SetActive(false);
        lines.SetActive(false);
    }

    private void EnableAllButtons()
    {
        blocksBTN.interactable = true;
        linesBTN.interactable = true;
    }

}
