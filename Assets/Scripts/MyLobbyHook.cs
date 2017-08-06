using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class MyLobbyHook : LobbyHook {

	public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lPlayer, GameObject gPlayer) {
		
		LobbyPlayer lobbyPlayer = lPlayer.GetComponent<LobbyPlayer>();
		PlayerManager gamePlayer = gPlayer.GetComponent<PlayerManager>();

		gamePlayer.m_name = lobbyPlayer.playerName;
	}
}
