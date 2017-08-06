using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioClip_Enum {
	EXPLOSION_Alien,
	EXPLOSION_Smokum,
	WEAPON_Railgun,
	WEAPON_PhotonGun,
	WEAPON_Blaster,
	EXPLOSION_Fiery,
	EXPLOSION_Test,
	ATMOSPHERE_1,
	ATMOSPHERE_2
}

public class AudioManager : MonoBehaviour {

	static AudioManager instance;
	public static AudioManager Instance { get { return instance; } private set { instance = value; } }

	void Start() {
		// int currentSec = 500;
		// Debug.Log(currentSec);
		// int seconds = currentSec % 60;
		// int minutes = currentSec / 60;
		// Debug.Log(minutes + " : " + seconds);
	}

	void Awake() {
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	[SerializeField] AudioClip[] clips;

	public void PlayMusic(AudioSource src) {
		src.clip = clips[(int)AudioClip_Enum.ATMOSPHERE_1];
		src.loop = true;
		src.Play();
	}

	public void PlayOneShot(AudioSource src, AudioClip clip, float volume = 1f, float pitch = 1f) {
		src.pitch = pitch;
		src.PlayOneShot(clip, volume);
	}
	public void PlayOneShot(AudioSource src, AudioClip_Enum clip, float volume = 1f, float pitch = 1f) {
		src.pitch = pitch;
		src.PlayOneShot(clips[(int)clip], volume);
	}
}
