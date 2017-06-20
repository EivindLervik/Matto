using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageMenu : MonoBehaviour {

    public Text title;
    public Text content;

    public void Open(string title, string content)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        this.title.text = title;
        this.content.text = content;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

}
