using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpController : MonoBehaviour {

    public Button blocksBTN;
    public GameObject blocks;
    public Button linesBTN;
    public GameObject lines;

    public void OpenBlocks()
    {
        DisableAll();
        EnableAllButtons();

        blocks.SetActive(true);
        blocksBTN.interactable = false;
    }

    public void OpenLines()
    {
        DisableAll();
        EnableAllButtons();

        lines.SetActive(true);
        linesBTN.interactable = false;
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
