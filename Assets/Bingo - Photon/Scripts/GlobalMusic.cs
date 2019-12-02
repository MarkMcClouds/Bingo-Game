using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMusic : MonoBehaviour {

	public static GlobalMusic m_instance;
	public AudioSource audioMusic;
	public AudioSource audioEffect;
	public AudioClip[] Sounds;
	public AudioClip[] Musics;
	public AudioClip[] NumberSpeak;

	void Awake(){
		m_instance = this;
	}

	public void PlayMusic (int id) {
		audioMusic.Stop ();
		audioMusic.clip = Musics [id];
		audioMusic.Play ();
	}

	public void PlayClickSound () {
		audioEffect.Stop ();
		audioEffect.clip = Sounds [0];
		audioEffect.Play ();
	}

	public void PlayBingoSound () {
		audioEffect.Stop ();
		audioEffect.clip = Sounds [1];
		audioEffect.Play ();
	}

	public void PlayNumberSound(int nbr){
		audioEffect.Stop ();
		audioEffect.clip = NumberSpeak [nbr-1];
		audioEffect.Play ();
	}
}
