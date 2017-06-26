using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum Language{
	English, Norwegian
}

public class LanguageHandler : MonoBehaviour {

	public Language language;
	public List<TextAsset> languageFiles;

	private void Start(){
		SetLanguage (GameHandler.preferenceHandler.GetLanguage ());
	}

	private void LoadLanguage(){
		// Load the selected language
	}

	public void SetLanguage(Language lang){
		language = lang;
		LoadLanguage ();
	}

	public int GetLanguageCount(){
		return languageFiles.Count;
	}

}
