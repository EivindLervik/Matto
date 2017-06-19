using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX
{
    Ok, Back
}

public class AudioHandler : MonoBehaviour {

    public AudioClip[] audioClips;

    private AudioSource source;

	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	private void PlaySFX(SFX sfx)
    {
        source.clip = audioClips[(int)sfx];
        source.Play();
    }
}
