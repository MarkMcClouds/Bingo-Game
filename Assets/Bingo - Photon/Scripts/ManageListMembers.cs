using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;

public class ManageListMembers : Photon.MonoBehaviour {

	public static ManageListMembers m_instance = null;
	public List<MPPlayer> playersList = new List<MPPlayer> ();
	public GameObject PrefabTop5Bar;
	public Transform IndexPositionInstantiateBar;


	void Awake(){
		m_instance = this;
	}

	//NEW PLAYER
	public void launch () {
		GlobalUI.m_instance.StartCoroutine (GlobalUI.m_instance.HideLoading());
		Join (PhotonNetwork.player, GlobalVariables.m_instance.pseudo);
	}


	//SEND TO MASTER CLIENT FOR YOUR ADD TO PLAYERS LIST
	public void Join(PhotonPlayer photonP, string pseudo){
		int Myglobalid = PhotonNetwork.room.PlayerCount;
		bool isMaster = false;
		if (photonP.IsMasterClient) {
			isMaster = true;
		}
		photonView.RPC("CmdAddPlayerToList", PhotonTargets.MasterClient, photonP, pseudo, isMaster, Myglobalid);//SEND LOGIN, IF MASTER AND GLOBAL ID TO MASTERCLIENT
	}

	[PunRPC]//THE MASTERCLIENT RECEIVE "CmdAddPlayerToList" SENDING BY NEW PLAYER CONNECTED AND LAUNCH "RpcAddPlayerToList"
	public void CmdAddPlayerToList(PhotonPlayer photonP, string pse, bool master, int globalID){
		if (!PhotonNetwork.isMasterClient) {//IF YOU NOT THE MASTERCLIENT
			return;
		}
		if (PhotonNetwork.room.PlayerCount == PhotonNetwork.room.MaxPlayers) {//IF PLAYERS IN ROOM IS EQUAL OF PLAYERS MAXIMUM IN ROOM
			PhotonNetwork.room.IsVisible = false;//HIDE ROOM IN MATCHMAKING PHOTON
			PhotonNetwork.room.IsOpen = false;//CLOSE ROOM IN MATCHMAKING PHOTON
		}
		//MASTERCLIENT ADDED IN ITS LIST OF PLAYERS, NEW PLAYER
		MPPlayer addlist = new MPPlayer();
		addlist.id = globalID;
		addlist.pseudo = pse;
		addlist.Master = master;
		addlist.photonP = photonP;
		playersList.Add (addlist);

		GameManager.m_instance.VerifIsGameReady (photonP);
		GameManager.m_instance.CountInRoom ();
		GlobalUI.m_instance.putInPlay.text = "$" + GlobalVariables.m_instance.PutInPlay.ToString ("0.##");
		RpcAddPlayerToList ();
	}

	//IF MASTERCLIENT, SEND YOUR LIST MEMBERS TO ALL CLIENT
	public void RpcAddPlayerToList(){//THE FUNCTION IS JUST FOR MASTERCLIENT
		photonView.RPC("RpcClearList", PhotonTargets.Others);//MASTERCLIENT SEND TO ALL PLAYERS FOR CLEARING THE LIST PLAYERS
		foreach (MPPlayer p in playersList.OrderByDescending(p => p.RemainingBalls).ToArray()) {//SEARCH IN THE LIST OF MASTERCLIENT, PLAYER BY PLAYER, WITH ORDER BY REMAINING BALLS
			Destroy(p.BarTop);// DESTROY BAR REMANING BALLS IN TOP5 OF SELECT PLAYER IN THE LIST
			photonView.RPC("RpcAddPlayerToList2", PhotonTargets.Others, p.pseudo, p.photonP, p.Master, p.Bingo, p.InGame, p.id, p.NbrCardBoards, p.RemainingBalls);//MASTER SEND TO ALL PLAYERS THE NEW LIST PLAYERS WITH NEW VALUES
			if (p.RemainingBalls <= 5) {//IF REMANING BALL OF SELECTED PLAYER IN THE LIST <= 5
				if (p.InGame) {//IF SELECTED PLAYER IN THE LIST IS INGAME
					GlobalUI.m_instance.Top5Block.SetActive (true);//ACTIVE TOP5 GAMEOBJECT
					GameObject objbarTop = Instantiate (PrefabTop5Bar, this.transform.position, Quaternion.identity) as GameObject;// ADD REMANING BALL BAR OF SELECTED PLAYER IN THE LIST, IN TOP5
					p.BarTop = objbarTop;//Add gameObject in the <List> player
					objbarTop.transform.SetParent (IndexPositionInstantiateBar);//MOVE TRANSFORM BAR IN INDEXPOSITION FOR UI
					objbarTop.GetComponent<Top5> ().pseudo.text = p.pseudo;//ADD IN REMANING BALL BAR THE LOGIN IN PLAYER SELECTED
					if (GlobalVariables.m_instance.GameIsReady) {//IF GLOBAL VARABLE GAME IS READY
						if (p.Bingo) {// IF PLAYER SELECTED IN THE LIST IS "BINGO"
							objbarTop.GetComponent<Top5> ().remaning.text = "BINGO!";//ADD TEXT "BINGO" IN REMANING BALLS BAR OF PLAYER
						} else {
							if (p.RemainingBalls == 1) {//IF REMANING BALLS OF SELECTED PLAYER IN THE LIST, IS EQUAL OF 1, CHANGE THE COLOR OF REMANING BALLS BAR, OF LOGIN AND TEXT.
								objbarTop.GetComponent<Image> ().color = TemplateColor.m_instance.ColorsList [GlobalVariables.m_instance.IDTemplate].ColorTop5BarOneBall;
								objbarTop.GetComponent<Top5> ().remaning.color = TemplateColor.m_instance.ColorsList [GlobalVariables.m_instance.IDTemplate].ColorTop5BarTextOneBall;
								objbarTop.GetComponent<Top5> ().pseudo.color = TemplateColor.m_instance.ColorsList [GlobalVariables.m_instance.IDTemplate].ColorTop5BarTextOneBall;
							}
							objbarTop.GetComponent<Top5> ().remaning.text = p.RemainingBalls.ToString ();//SHOW THE TEXT OF REMANING BALLS OF SELECTED PLAYER
						}
						p.BarTop.GetComponent<RectTransform> ().SetAsFirstSibling ();
					}
				}
			} else {//IF REMANING BALL OF SELECTED PLAYER IN THE LIST > 5
				GlobalUI.m_instance.Top5Block.SetActive (false);//DISABLE GAMEOBJECT TOP5
				foreach (Transform child in IndexPositionInstantiateBar){//SEARCH ALL REMANING BAR IN INDEX POSITION IN UI
					Destroy(child.gameObject);//AND DESTROY ALL REMANING BALLS BAR IN UI
				}
			}
		}
	}

	[PunRPC]//ADD NEW PLAYERS TO LIST MEMBERS
	public void RpcAddPlayerToList2(string playerName, PhotonPlayer photonP, bool isMaster, bool bingo, bool ingame, int idplayer, int nbrcards, int remaningball){
		//THE FUNCTION IS SENDING BY THE MASTERCLIENTFOR ALL PLAYERS AND NOT FOR THE MASTERCLIENT
		if (PhotonNetwork.isMasterClient) {
			return;
		}
		GameObject objbarTop = null;
		if (GlobalVariables.m_instance.GameIsReady) {//IF GLOBAL VARABLE GAME IS READY
			if (remaningball <= 5) {//IF REMANING BALL OF SELECTED PLAYER IN THE LIST <= 5
				if (ingame) {//IF SELECTED PLAYER IN THE LIST IS INGAME
					GlobalUI.m_instance.Top5Block.SetActive (true);//ACTIVE TOP5 GAMEOBJECT
					objbarTop = Instantiate (PrefabTop5Bar, this.transform.position, Quaternion.identity) as GameObject;// ADD REMANING BALL BAR OF SELECTED PLAYER IN THE LIST, IN TOP5
					objbarTop.transform.SetParent (IndexPositionInstantiateBar);//MOVE TRANSFORM BAR IN INDEXPOSITION FOR UI
					objbarTop.GetComponent<Top5> ().pseudo.text = playerName;//ADD IN REMANING BALL BAR THE LOGIN IN PLAYER SELECTED
					if (bingo) {// IF PLAYER SELECTED IN THE LIST IS "BINGO"
						objbarTop.GetComponent<Top5> ().remaning.text = "BINGO!";//ADD TEXT "BINGO" IN REMANING BALLS BAR OF PLAYER
					} else {
						if (remaningball == 1) {//IF REMANING BALLS OF SELECTED PLAYER IN THE LIST, IS EQUAL OF 1, CHANGE THE COLOR OF REMANING BALLS BAR, OF LOGIN AND TEXT.
							objbarTop.GetComponent<Image> ().color = TemplateColor.m_instance.ColorsList [GlobalVariables.m_instance.IDTemplate].ColorTop5BarOneBall;
							objbarTop.GetComponent<Top5> ().remaning.color = TemplateColor.m_instance.ColorsList [GlobalVariables.m_instance.IDTemplate].ColorTop5BarTextOneBall;
							objbarTop.GetComponent<Top5> ().pseudo.color = TemplateColor.m_instance.ColorsList [GlobalVariables.m_instance.IDTemplate].ColorTop5BarTextOneBall;
						}
						objbarTop.GetComponent<Top5> ().remaning.text = remaningball.ToString ();//SHOW THE TEXT OF REMANING BALLS OF SELECTED PLAYER
					}
					objbarTop.transform.SetParent (IndexPositionInstantiateBar);//MOVE TRANSFORM BAR IN INDEXPOSITION FOR UI
				}
			} else {//IF REMANING BALL OF SELECTED PLAYER IN THE LIST > 5
				GlobalUI.m_instance.Top5Block.SetActive (false);//DISABLE GAMEOBJECT TOP5
				foreach (Transform child in IndexPositionInstantiateBar){//SEARCH ALL REMANING BAR IN INDEX POSITION IN UI
					Destroy(child.gameObject);//AND DESTROY ALL REMANING BALLS BAR IN UI
				}
			}
		}
		//ADD NEW PLAYER IN THE LIST WITH NEW VALUES
		MPPlayer addlist = new MPPlayer ();
		addlist.id = idplayer;
		addlist.pseudo = playerName;
		addlist.Master = isMaster;
		addlist.Bingo = bingo;
		addlist.InGame = ingame;
		addlist.photonP = photonP;
		addlist.NbrCardBoards = nbrcards;
		addlist.BarTop = objbarTop;
		addlist.RemainingBalls = remaningball;
		playersList.Add (addlist);

		foreach (MPPlayer player in playersList.OrderByDescending(p => p.RemainingBalls).ToArray()) {//SEARCH IN THE LIST OF MASTERCLIENT, PLAYER BY PLAYER, WITH ORDER BY REMAINING BALLS
			if (player.BarTop != null) {
				player.BarTop.GetComponent<RectTransform>().SetAsFirstSibling(); //ORDER BAR TOP5
			}
		}
		GameManager.m_instance.CountInRoom ();//SHOW COUNT PLAYERS IN ROOM
		GlobalUI.m_instance.putInPlay.text = "$" + GlobalVariables.m_instance.PutInPlay.ToString ("0.##");//SHOW PUT IN PLAY MONEY'S
	}

	[PunRPC]
	//ERASE THE LIST MEMBERS TO ALL CLIENTS EXCEPTED THE MASTERCLIENT
	public void RpcClearList(){
		if (PhotonNetwork.isMasterClient) {
			return;
		}
		foreach (MPPlayer p in playersList.ToArray()) {
			Destroy(p.BarTop);
		}
		playersList.Clear ();
	}

	[PunRPC]
	//REFRESH THE LIST PLAYERS AFTER PLAYER DISCONNECTED
	public void CheckList(PhotonPlayer photonP){
		if (PhotonNetwork.isMasterClient) {

			GameManager.m_instance.StopAllCoroutines ();

			MPPlayer tempPlayer = null;
			foreach (MPPlayer p in playersList) {
				if (p.photonP == photonP) {
					tempPlayer = p;
				}
			}
			if (tempPlayer != null) {
				if (tempPlayer.NbrCardBoards > 0) {
					GlobalVariables.m_instance.NbrPlayersIsReady--;
				}
				playersList.Remove (tempPlayer);
				Destroy (tempPlayer.BarTop);
			}
			GameManager.m_instance.CountInRoom ();

			if (GlobalVariables.m_instance.GameIsReady) {
				if (playersList.Count < 2) {
					GameManager.m_instance.StartCoroutine (GameManager.m_instance.CloseLoadingAndLaunchReset ());
					GlobalUI.m_instance.Loading.SetActive (true);
				} else {
					GameManager.m_instance.StartCoroutine (GameManager.m_instance.LaunchPrint ());
				}
			}  else {
				GameManager.m_instance.LaunchTimer = true;
			}
			RpcAddPlayerToList ();

			if (PhotonNetwork.room.PlayerCount < PhotonNetwork.room.MaxPlayers) {
				PhotonNetwork.room.IsVisible = true;
				PhotonNetwork.room.IsOpen = true;
			}
		}
	}

	[PunRPC]
	//REFRESH LIST PLAYERS AFTER PLAYER DISCONNECTED
	public void refreshRemaningBallMaster(PhotonPlayer photonP, int remaning){
		if (PhotonNetwork.isMasterClient) {
			MPPlayer tempPlayer = null;
			foreach (MPPlayer p in playersList) {
				if (p.photonP == photonP) {
					tempPlayer = p;
				}
			}
			if (tempPlayer != null) {
				tempPlayer.RemainingBalls = remaning;
			}

			RpcAddPlayerToList ();
		}
	}

	//FUNCTION IF MASTERCLIENT IS DISCONNECTED SO SWITCH NEW MASTERCLIENT
	void OnMasterClientSwitched(PhotonPlayer newMasterClient){
		MPPlayer tempPlayer = null;
		foreach (MPPlayer p in playersList) {
			if (p.photonP == newMasterClient) {
				tempPlayer = p;
			}
		}
		if (tempPlayer != null) {
			tempPlayer.Master = true;
		}
		if (GlobalVariables.m_instance.GameIsReady) {
			int countInGame = GameManager.m_instance.CountPlayerInGame ();
			print (countInGame);
			if (countInGame <= 2) {
				GameManager.m_instance.StartCoroutine (GameManager.m_instance.CloseLoadingAndLaunchReset ());
				GlobalUI.m_instance.Loading.SetActive (true);
			} else {
				GameManager.m_instance.StartCoroutine (GameManager.m_instance.LaunchPrint ());
			}
		} else {
			GameManager.m_instance.LaunchTimer = true;
		}
	}

	//IF PLAYER DISCONNECTED SO REFRESH THE LIST PLAYERS
	public void OnPhotonPlayerDisconnected(PhotonPlayer player){    
		CheckList(player);
	}

	//IF YOU IS DISCONNECTED OF PHOTON
	public void OnDisconnectedFromPhoton(){    
		playersList.Clear ();
	}

}

[System.Serializable]
public class MPPlayer
{
	public int id;
	public string pseudo;
	public bool Master = false;
	public bool Bingo = false;
	public bool InGame = false;
	public int NbrCardBoards;
	public int RemainingBalls = 15;
	public GameObject BarTop;
	public PhotonPlayer photonP;
}
