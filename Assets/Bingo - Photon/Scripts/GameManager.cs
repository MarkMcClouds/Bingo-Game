using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.MonoBehaviour {

	public static GameManager m_instance;
	public List<int> ListPrintNumbers;
	public List<int> ListPrintNumberProvisional;
	public int LastNbrPrint;
	public Animator anim;
	public List<WinList> WinnerList = new List<WinList> ();
	public bool LaunchTimer = false;
	public float Maxtimer = 10f;
	public float timer = 10f;
	public float timeRound = 2f;
	float minutes;
	float seconds;

	void Awake () {
		m_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (LaunchTimer) {
			timer -= Time.deltaTime;
			minutes = Mathf.Floor (timer / 60);
			seconds = Mathf.RoundToInt (timer % 60);
			GlobalUI.m_instance.Avertiss.fontSize = 25;
			GlobalUI.m_instance.Avertiss.text = minutes.ToString ("0") + ":" + seconds.ToString ("00");
			if (minutes <= 0f && seconds <= 0) {
				if (PhotonNetwork.isMasterClient) {
					if (GlobalVariables.m_instance.NbrPlayersIsReady >= 1) {
						GlobalVariables.m_instance.GameIsReady = true;
						photonView.RPC ("StartGame", PhotonTargets.All, GlobalVariables.m_instance.GameIsReady);
						CreateList ();
					} else {
						timer = Maxtimer;
						GlobalUI.m_instance.Avertiss.fontSize = 15;
						GlobalUI.m_instance.Avertiss.text = "Wait others players..";
					}
					LaunchTimer = false;
				}
			}
			if (PhotonNetwork.isMasterClient) {
				if (GlobalVariables.m_instance.NbrPlayersIsReady < 1) {
					LaunchTimer = false;
					timer = Maxtimer;
					GlobalUI.m_instance.Avertiss.fontSize = 15;
					GlobalUI.m_instance.Avertiss.text = "Wait others players..";
				}
			}
		} else {
			if (!GlobalVariables.m_instance.GameIsReady) {
				if (GlobalVariables.m_instance.NbrPlayersIsReady >= 1) {
					LaunchTimer = true;
					timer = Maxtimer;
				} else {
					GlobalUI.m_instance.Avertiss.fontSize = 15;
					GlobalUI.m_instance.Avertiss.text = "Wait others players..";
				}
			}
		}
	}

	public void CreateList(){
		for (int i = 1; i < 91; i++) {
			ListPrintNumbers.Add(0);
		}
		for (int i = 1; i < 91; i++) {
			ListPrintNumberProvisional.Add(i);
		}
		for (int i = 0; i < 90; i++) {
			int thisNumber = Random.Range(0, ListPrintNumberProvisional.Count);
			ListPrintNumbers[i] = ListPrintNumberProvisional[thisNumber];
			ListPrintNumberProvisional.RemoveAt(thisNumber);
		}
		foreach(MPPlayer playerIsReady in ManageListMembers.m_instance.playersList.ToArray()){
			playerIsReady.InGame = true;
			photonView.RPC ("SendListOthers", playerIsReady.photonP, ListPrintNumbers.ToArray ());
		}
		ManageListMembers.m_instance.RpcAddPlayerToList ();
		StartCoroutine (LaunchPrint ());
	}

	public void CountInRoom(){
		GlobalUI.m_instance.GlobalParticipants.text = PhotonNetwork.room.PlayerCount.ToString ("0");
	}

	public void VerifIsGameReady(PhotonPlayer photonP){
		if (GlobalVariables.m_instance.GameIsReady) {
			photonView.RPC ("WaitInSpectator", photonP, GlobalVariables.m_instance.NbrPrint, LastNbrPrint, ListPrintNumbers.ToArray());
		}
	}

	public void Bingo(){
		print ("Bingo!");
		photonView.RPC ("SendBingo", PhotonTargets.MasterClient, PhotonNetwork.player);
	}

	public void ResetGame(){
		ListPrintNumbers.Clear ();
		WinnerList.Clear ();
		if (!GlobalUI.m_instance.AllCards.activeSelf) {
			GlobalUI.m_instance.AllCards.SetActive (true);
		}
		if (GlobalUI.m_instance.AlertForNextGame.activeSelf) {
			GlobalUI.m_instance.AlertForNextGame.SetActive (false);
		}
		if (!GlobalUI.m_instance.BlockValidateBuy.activeSelf) {
			GlobalUI.m_instance.BlockValidateBuy.SetActive (true);
		}
		if (!GlobalUI.m_instance.BlockTimer.activeSelf) {
			GlobalUI.m_instance.BlockTimer.SetActive (true);
		}
		if (!GlobalUI.m_instance.RefreshBtn.activeSelf) {
			GlobalUI.m_instance.RefreshBtn.SetActive (true);
		}
		if (GlobalUI.m_instance.BallPrint.activeSelf) {
			GlobalUI.m_instance.BallPrint.SetActive (false);
		}
		if (GlobalUI.m_instance.Top5Block.activeSelf) {
			GlobalUI.m_instance.Top5Block.SetActive (false);
		}
		if (GlobalVariables.m_instance.NbrPlayersIsReady >= 1) {
			LaunchTimer = true;
			GlobalUI.m_instance.Avertiss.fontSize = 25;
		} else {
			GlobalUI.m_instance.Avertiss.fontSize = 15;
			GlobalUI.m_instance.Avertiss.text = "Wait others players..";
		}
		GlobalUI.m_instance.LoginBingo.text = "";
		GlobalUI.m_instance.validateBuy.interactable = false;
		GlobalUI.m_instance.MoneyText.text = "Money's\n$" + GlobalVariables.m_instance.Money.ToString ("0.##");
		GlobalUI.m_instance.PrebuyMoneyText.text = "Total : $0";
		GlobalUI.m_instance.putInPlay.text = "$0";
		GlobalVariables.m_instance.CardBoardBuy = 0;
		GlobalVariables.m_instance.InCurrentGame = false;
		GlobalVariables.m_instance.CardBoardSelected = 0;
		AllCardBoards.m_instance.ResetCardBoard ();
		foreach(MPPlayer player in ManageListMembers.m_instance.playersList.ToArray()){
			player.InGame = false;
			player.RemainingBalls = 15;
			player.NbrCardBoards = 0;
			Destroy (player.BarTop);
		}
		foreach (Transform child in ManageListMembers.m_instance.IndexPositionInstantiateBar){
			Destroy(child.gameObject);
		}
		if (PhotonNetwork.isMasterClient) {
			timer = Maxtimer;
			GlobalVariables.m_instance.NbrPlayersIsReady = 0;
			GlobalVariables.m_instance.PutInPlay = 0f;
			GlobalVariables.m_instance.GameIsReady = false;
			GlobalVariables.m_instance.NbrPrint = 0;
			GlobalVariables.m_instance.Bingo = false;
			ManageListMembers.m_instance.RpcAddPlayerToList ();
		}
	}

	public IEnumerator LaunchPrint(){
		if (!GlobalVariables.m_instance.Bingo) {
			yield return new WaitForSeconds (timeRound);
			StopAllCoroutines ();
			int thisNumber = Random.Range (0, ListPrintNumbers.Count);
			GlobalUI.m_instance.BallPrint.SetActive (true);
			GlobalUI.m_instance.BallPrint.GetComponent<ShowNbrBall> ().TextBallNbr.text = ListPrintNumbers [thisNumber].ToString ();
			LastNbrPrint = ListPrintNumbers [thisNumber];
			ListPrintNumbers.RemoveAt (thisNumber);
			GlobalMusic.m_instance.PlayNumberSound (LastNbrPrint);
			GlobalVariables.m_instance.NbrPrint++;
			GlobalUI.m_instance.NbrPrint.text = GlobalVariables.m_instance.NbrPrint.ToString () + " / 90";
			GlobalUI.m_instance.putInPlay.text = "$" + GlobalVariables.m_instance.PutInPlay.ToString ("0.##");
			anim.SetBool ("Play", true);
			StopCoroutine ("LaunchPrint");
			StartCoroutine (VerifIfNumber ());
			photonView.RPC ("RefreshListOthers", PhotonTargets.Others, ListPrintNumbers.ToArray (), LastNbrPrint);
		}
	}

	IEnumerator VerifIfNumber(){
		yield return new WaitForSeconds (1f);
		AllCardBoards.m_instance.SendLastnumber (LastNbrPrint);
		StopCoroutine ("VerifIfNumber");
	}

	public IEnumerator CloseLoadingAndLaunchReset(){
		yield return new WaitForSeconds (3f);
		if (PhotonNetwork.isMasterClient && GlobalVariables.m_instance.InCurrentGame) {
			GlobalVariables.m_instance.Money += GlobalVariables.m_instance.PutInPlay;
		}
		GlobalUI.m_instance.Loading.SetActive (false);
		ResetGame ();
		StopCoroutine ("CloseLoadingAndLaunchReset");
	}
		
	IEnumerator coroutineResetGame(){
		yield return new WaitForSeconds (5f);
		GlobalUI.m_instance.BingoWindows.SetActive(false);
		ResetGame ();
		StopCoroutine ("coroutineResetGame");
	}

	IEnumerator verifWinner(){
		yield return new WaitForSeconds (1f);
		string phrase = "";
		float putTotal = 0;
		bool jackpo = false;
		if (GlobalVariables.m_instance.NbrPrint <= GlobalVariables.m_instance.AtBallJackpot) {
			putTotal = GlobalVariables.m_instance.PutInPlay / WinnerList.Count + GlobalVariables.m_instance.Jackpot / WinnerList.Count;
			jackpo = true;
		} else {
			putTotal = GlobalVariables.m_instance.PutInPlay / WinnerList.Count;
		}
		for (int i = 0; i < WinnerList.Count; i++) {
			phrase += WinnerList[i].pseudoWin + "     $" + putTotal.ToString("0.##") + "\n";
		}
		foreach (WinList photonInWinList in WinnerList) {
			photonView.RPC("SendMoney", photonInWinList.photonP, putTotal);
		}
		StopCoroutine ("verifWinner");
		photonView.RPC("GameFinish", PhotonTargets.All, phrase, jackpo);
	}

	[PunRPC]
	public void SendBingo(PhotonPlayer photonP){
		GlobalVariables.m_instance.Bingo = true;
		StopAllCoroutines ();
		if (WinnerList.Count == 0) {
			WinList newAdd = new WinList ();
			newAdd.pseudoWin = photonP.NickName;
			newAdd.photonP = photonP;
			WinnerList.Add (newAdd);
		}else{
			foreach (WinList photonInWinList in WinnerList) {
				if (photonP != photonInWinList.photonP) {
					WinList newAdd = new WinList ();
					newAdd.pseudoWin = photonP.NickName;
					newAdd.photonP = photonP;
					WinnerList.Add (newAdd);
				}
			}
		}

		StartCoroutine (verifWinner ());
	}

	[PunRPC]
	public void GameFinish(string phrase, bool Jackpo){
		StopAllCoroutines ();
		GlobalMusic.m_instance.PlayBingoSound ();
		GlobalVariables.m_instance.Bingo = true;
		GlobalUI.m_instance.BingoWindows.SetActive(true);
		if (Jackpo) {
			GlobalUI.m_instance.LogoBingo.sprite = GlobalUI.m_instance.logosBingo [1];
		} else {
			GlobalUI.m_instance.LogoBingo.sprite = GlobalUI.m_instance.logosBingo [0];
		}
		GlobalUI.m_instance.BallPrint.SetActive (false);
		GlobalUI.m_instance.LoginBingo.text = phrase;
		StartCoroutine (coroutineResetGame());
	}

	[PunRPC]
	public void SendMoney(float moneys){
		GlobalVariables.m_instance.Money += moneys;
		GlobalUI.m_instance.MoneyText.text = "Money's\n$"+GlobalVariables.m_instance.Money.ToString ("0.##");
	}

	[PunRPC]
	public void SendListOthers(int[] nbr){
		ListPrintNumbers = new List<int>(nbr);
	}

	[PunRPC]
	public void StartGame(bool gameready){
		GlobalVariables.m_instance.GameIsReady = gameready;
		if (GlobalVariables.m_instance.GameIsReady) {
			if (GlobalUI.m_instance.BlockTimer.activeSelf) {
				GlobalUI.m_instance.BlockTimer.SetActive (false);
			}
			if (GlobalUI.m_instance.WindowsBuy.activeSelf) {
				GlobalUI.m_instance.WindowsBuy.SetActive (false);
			}
			if (GlobalUI.m_instance.BlockValidateBuy.activeSelf) {
				GlobalUI.m_instance.BlockValidateBuy.SetActive (false);
			}
			if (GlobalUI.m_instance.RefreshBtn.activeSelf) {
				GlobalUI.m_instance.RefreshBtn.SetActive (false);
			}
			BuyCardboards.m_instance.DeSelectCard ();
			if (GlobalVariables.m_instance.CardBoardBuy == 0) {
				GlobalUI.m_instance.AlertForNextGame.SetActive (true);
			}
		}
	}

	[PunRPC]
	public void RefreshListOthers(int[] newList, int nbr){
		if (!GlobalVariables.m_instance.Bingo) {
			GlobalVariables.m_instance.NbrPrint++;
			LastNbrPrint = nbr;
			GlobalUI.m_instance.NbrPrint.text = GlobalVariables.m_instance.NbrPrint.ToString () + " / 90";
			GlobalUI.m_instance.putInPlay.text = "$" + GlobalVariables.m_instance.PutInPlay.ToString ("0.##");
			GlobalUI.m_instance.BallPrint.SetActive (true);
			GlobalUI.m_instance.BallPrint.GetComponent<ShowNbrBall> ().TextBallNbr.text = LastNbrPrint.ToString ();
			GlobalMusic.m_instance.PlayNumberSound (LastNbrPrint);
			anim.SetBool ("Play", true);
			ListPrintNumbers.Clear ();
			ListPrintNumbers = new List<int> (newList);
			if (GlobalVariables.m_instance.GameIsReady) {
				if (GlobalVariables.m_instance.InCurrentGame) {
					StartCoroutine (VerifIfNumber ());
				}
			}
		}
	}

	[PunRPC]
	public void WaitInSpectator(int print, int Last, int[] nbr){
		if (GlobalUI.m_instance.BlockTimer.activeSelf) {
			GlobalUI.m_instance.BlockTimer.SetActive (false);
		}
		if (GlobalUI.m_instance.BlockValidateBuy.activeSelf) {
			GlobalUI.m_instance.BlockValidateBuy.SetActive (false);
		}
		if (GlobalUI.m_instance.RefreshBtn.activeSelf) {
			GlobalUI.m_instance.RefreshBtn.SetActive (false);
		}
		if (GlobalUI.m_instance.WindowsBuy.activeSelf) {
			GlobalUI.m_instance.WindowsBuy.SetActive (false);
		}
		GlobalUI.m_instance.AllCards.SetActive (false);
		GlobalVariables.m_instance.NbrPrint = print;
		ListPrintNumbers = new List<int>(nbr);
		LastNbrPrint = Last;
		GlobalUI.m_instance.AlertForNextGame.SetActive (true);
		BuyCardboards.m_instance.DeSelectCard ();
	}

	public int CountPlayerInGame(){
		int value = 0;
		foreach (MPPlayer p in ManageListMembers.m_instance.playersList.ToArray()) {
			if (p.InGame) {
				value++;
			}
		}
		return value;
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		// Always send transform (depending on reliability of the network view)
		if (stream.isWriting){
			stream.SendNext(LaunchTimer);
			stream.SendNext(GlobalVariables.m_instance.GameIsReady);
			stream.SendNext(GlobalVariables.m_instance.Bingo);
			stream.SendNext(GlobalVariables.m_instance.PutInPlay);
			stream.SendNext(GlobalVariables.m_instance.NbrPrint);
			stream.SendNext(GlobalVariables.m_instance.NbrPlayersIsReady);
			stream.SendNext(timer);
			stream.SendNext(minutes);
			stream.SendNext(seconds);
		}else{
			LaunchTimer = (bool)stream.ReceiveNext();
			GlobalVariables.m_instance.GameIsReady = (bool)stream.ReceiveNext();
			GlobalVariables.m_instance.Bingo = (bool)stream.ReceiveNext();
			GlobalVariables.m_instance.PutInPlay = (float)stream.ReceiveNext();
			GlobalVariables.m_instance.NbrPrint = (int)stream.ReceiveNext();
			GlobalVariables.m_instance.NbrPlayersIsReady = (int)stream.ReceiveNext();
			timer = (float)stream.ReceiveNext();
			minutes = (float)stream.ReceiveNext();
			seconds = (float)stream.ReceiveNext();
		}
	}
}

[System.Serializable]
public class WinList
{
	public string pseudoWin;
	public PhotonPlayer photonP;
}