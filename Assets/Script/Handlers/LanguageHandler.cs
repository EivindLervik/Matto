using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public enum Language{
	English, Norsk, 日本語
}

public class LanguageHandler : MonoBehaviour {

	public Language language;
	public List<TextAsset> languageFiles;

    private Dictionary<string, string> dict;

	public void Initialize(){
		SetLanguage (GameHandler.preferenceHandler.GetLanguage ());
	}

    private void LoadLanguage(){
        TextAsset ta = languageFiles[(int)language];
        dict = new Dictionary<string, string>();

        string fs = ta.text;
        string[] fLines = Regex.Split(fs, System.Environment.NewLine);

        for (int i = 0; i < fLines.Length; i++)
        {

            string valueLine = fLines[i];
            string[] values = valueLine.Split('|');

            if (values.Length > 1)
            {
                dict.Add(values[0], values[1]);
            }
        }
    }

	public void SetLanguage(Language lang){
		language = lang;
		LoadLanguage ();

        TextChanger[] comp = GameHandler.objectHandler.GetTextComponents();
        foreach (TextChanger t in comp)
        {
            t.Change();
        }
    }

	public int GetLanguageCount(){
		return languageFiles.Count;
	}

    public string GetText(string key)
    {
        try
        {
            return dict[key];
        }
        catch(KeyNotFoundException e)
        {
            Debug.LogError("This language does not have this key: " + key + ". Error printout: " + e.ToString());
            return null;
        }
    }

}
