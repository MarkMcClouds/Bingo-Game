using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This script automatically connects to Photon (using the settings file),
/// tries to join a random room and creates one if none was found (which is ok).
/// </summary>
public class ConnectToPhoton : Photon.MonoBehaviour
{
	/// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
	/// 

	public bool AutoConnect = true;

	public ShowStatusWhenConnecting StatusConnection;

	public byte Version = 1;

	/// <summary>if we don't want to connect in Start(), we have to "remember" if we called ConnectUsingSettings()</summary>
	private bool ConnectInUpdate = false;


	public virtual void Start()
	{
		PhotonNetwork.autoJoinLobby = true;    // we join randomly. always. no need to join a lobby to get the list of rooms.
	}

	public virtual void Update(){
		if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected){
			ConnectInUpdate = false;
			StatusConnection.enabled = true;
			PhotonNetwork.ConnectUsingSettings(Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
		}
	}

	public virtual void OnJoinedLobby()
	{
		GlobalUI.m_instance.ShowSelectBingoPanels ();//SHOW PANEL "SELECT GAME" IN GLOBALUI INSTANCE
		StatusConnection.enabled = false;
		Debug.Log("In Lobby");

	}
		
	public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		Debug.LogError("Cause: " + cause);
	}

	public void LaunchConnectToPhoton(){
		ConnectInUpdate = true;// LAUNCH CONNECTION PHOTON
		GlobalMusic.m_instance.PlayClickSound ();//PLAY CLICK SOUND IN GLOBALMUSIC INSTANCE
		GlobalUI.m_instance.btn_play.interactable = false;// DISABLE BUTTON "PLAY" IN GLOBALUI INSTANCE
		GlobalVariables.m_instance.pseudo = GlobalUI.m_instance.loginInput.text + Random.Range(0, 9999).ToString();//SAVE LOGIN IN GLOBALVARIABLES INSTANCE.
		PhotonNetwork.player.NickName = GlobalVariables.m_instance.pseudo;//SAVE LOGIN IN PHOTONNETWORK AND IN PHOTONPLAYER.
	}
}
