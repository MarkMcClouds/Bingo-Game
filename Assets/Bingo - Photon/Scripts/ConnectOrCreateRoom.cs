using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectOrCreateRoom : Photon.MonoBehaviour {

	public byte maxPlayerFromTable = 100;

	public void CreateOrJoinRoom(float stakes){
		GlobalMusic.m_instance.PlayClickSound ();
		GlobalVariables.m_instance.stakesSelected = stakes;
		JoinRoom (stakes, maxPlayerFromTable);
	}

	public void SelectTemplate(int idtemplate){
		GlobalVariables.m_instance.IDTemplate = idtemplate;
	}

	public void ApplyJackpot(float jackpot){
		GlobalVariables.m_instance.Jackpot = jackpot;
	}

	public void ApplyAtBall(float atball){
		GlobalVariables.m_instance.AtBallJackpot = atball;
	}

	void JoinRoom (float stakes, byte maxPlayer) {
		ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable(){
			{ "Stakes",stakes }
		};
		PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, maxPlayer);
	}

	void OnPhotonRandomJoinFailed(){
		string roomname = Random.Range(0, 999999).ToString();
		float stakes = GlobalVariables.m_instance.stakesSelected;
		CreateRoom(roomname, maxPlayerFromTable, stakes);
	}

	void CreateRoom(string name, byte maxPlayer, float stake){
		ExitGames.Client.Photon.Hashtable roomProps = new ExitGames.Client.Photon.Hashtable() { { "Stakes", stake } };
		string[] roomPropsInLobby = { "Stakes" };
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.customRoomProperties = roomProps;
		roomOptions.customRoomPropertiesForLobby = roomPropsInLobby;
		roomOptions.maxPlayers = maxPlayer;
		PhotonNetwork.CreateRoom(name, roomOptions, TypedLobby.Default);
	}

	public void OnJoinedRoom(){
		GlobalVariables.m_instance.initJackpot ();
		GlobalMusic.m_instance.PlayMusic (1);
		GlobalUI.m_instance.ShowGamePanel ();
		GlobalUI.m_instance.MoneyText.text = "Money's\n$"+GlobalVariables.m_instance.Money.ToString ("0.##");
		ManageListMembers.m_instance.launch ();
		TemplateColor.m_instance.initTemplate();
		Debug.Log("In Room");
	}
}
