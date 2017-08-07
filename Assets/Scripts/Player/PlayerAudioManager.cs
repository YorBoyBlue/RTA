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

	public void PlayAudioClip(AudioClips audioClip, float volumeScale = 1) {
		CmdPlayAudioClip(audioClip, volumeScale);
	}

	public void PlayAmbientAudioClip(AmbientAudioClips ambientAudioClip, float volumeScale = 1) {
		CmdPlayAmbientAudioClip(ambientAudioClip, volumeScale);
	}

	public void PlayLoopAudioClip(LoopAudioClips audioClipToLoop, float volumeScale = 1) {
		CmdPlayLoopAudioClip(audioClipToLoop, volumeScale);
	}

	[Command]
	public void CmdPlayAudioClip(AudioClips audioClip, float volumeScale) {
		RpcPlayAudioClip(audioClip, volumeScale);
	}

	[Command]
	public void CmdPlayAmbientAudioClip(AmbientAudioClips ambientAudioClip, float volumeScale) {
		RpcPlayAmbientAudioClip(ambientAudioClip, volumeScale);
	}

	[Command]
	public void CmdPlayLoopAudioClip(LoopAudioClips audioClipToLoop, float volumeScale) {
		RpcPlayLoopAudioClip(audioClipToLoop, volumeScale);
	}

	[ClientRpc]
	public void RpcPlayAudioClip(AudioClips audioClip, float volumeScale) {
		m_audioSource.clip = m_audioClips[(int)audioClip];
		m_audioSource.PlayOneShot(m_audioSource.clip, volumeScale);
	}

	[ClientRpc]
	public void RpcPlayAmbientAudioClip(AmbientAudioClips ambientAudioClip, float volumeScale) {
		m_ambientAudioSource.clip = m_ambientAudioClips[(int)ambientAudioClip];
		m_ambientAudioSource.PlayOneShot(m_audioSource.clip, volumeScale);
	}

	[ClientRpc]
	public void RpcPlayLoopAudioClip(LoopAudioClips audioClipToLoop, float volumeScale) {
		m_loopAudioSource.clip = m_loopAudioClips[(int)audioClipToLoop];
		m_loopAudioSource.loop = true;
		m_loopAudioSource.Play();
	}	
}
