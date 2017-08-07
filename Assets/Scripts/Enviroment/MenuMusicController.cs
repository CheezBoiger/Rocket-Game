using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuMusicController : MonoBehaviour {
    public AudioClip audio;
    private AudioSource source;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		source = GetComponent<AudioSource>();
        source.Play();
        source.volume = 0.01f;
	}
}
