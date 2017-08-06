using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum AudioClips {
	SHIP_EXPLOSION = 0,
	ASTEROID_EXPLOSION = 1,
	MAIN_WEAPON = 2
}

public enum AmbientAudioClips {
	FIRERY_EXPLOSION = 0,
	PHOTON_GUNS = 1,
	RAIL_GUN = 2,
	SMOKUM_EXPLOSION = 3
}

public enum LoopAudioClips {
	SPACE_AMBIENT_1 = 0,
	SPACE_AMBIENT_2 = 1
}

public class PlayerAudioManager : NetworkBehaviour {

	[SerializeField] AudioClip[] m_audioClips;
	[SerializeField] AudioClip[] m_ambientAudioClips;
	[SerializeField] AudioClip[] m_loopAudioClips;

	[SerializeField] AudioSource m_audioSource;
	[SerializeField] AudioSource m_ambientAudioSource;
	[SerializeField] AudioSource m_loopAudioSource;

	[ClientRpc]
	public void RpcPlayAudioClip(AudioClips audioClip, float volumeScale = 1f) {
		m_audioSource.clip = m_audioClips[(int)audioClip];
		m_audioSource.PlayOneShot(m_audioSource.clip, volumeScale);
	}

	[ClientRpc]
	public void RpcPlayAmbientAudioClip(AmbientAudioClips ambientAudioClip, float volumeScale = 1f) {
		m_ambientAudioSource.clip = m_audioClips[(int)ambientAudioClip];
		m_ambientAudioSource.PlayOneShot(m_audioSource.clip, volumeScale);
	}

	[ClientRpc]
	public void RpcPlayLoopAudioClip(LoopAudioClips audioClipToLoop, float volumeScale = 1f) {
		m_loopAudioSource.clip = m_audioClips[(int)audioClipToLoop];
		m_loopAudioSource.loop = true;
		m_loopAudioSource.Play();
	}	
}
