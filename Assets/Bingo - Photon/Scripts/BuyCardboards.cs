using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCardboards : Photon.MonoBehaviour {

	public static BuyCardboards m_instance;
	public Image[] imgCardBoard;

	public AllCardBoards Cards;

	void Awake(){
		m_instance = this;
	}

	void Start(){
		for (int i = 0; i < 12; i++) {
			imgCardBoard[i] = Cards.CardsBoards [i].transform.parent.gameObject.GetComponent<Image> ();
		}
	}

	public void SelectCard(int idCardBoard){
		if (!GlobalVariables.m_instance.GameIsReady) {
			if (!Cards.CardsBoards [idCardBoard].isSelected) {
				GlobalVariables.m_instance.CardBoardSelected++;
				imgCardBoard [idCardBoard].color = TemplateColor.m_instance.ColorsList[GlobalVariables.m_instance.IDTemplate].ColorCardsSelected;
				Cards.CardsBoards [idCardBoard].isSelected = true;
			} else {
				GlobalVariables.m_instance.CardBoardSelected--;
				imgCardBoard [idCardBoard].color = TemplateColor.m_instance.ColorsList[GlobalVariables.m_instance.IDTemplate].ColorCardsNoSelected;
				Cards.CardsBoards [idCardBoard].isSelected = false;
			}
			TotalPrice ();
		}
	}

	public void DeSelectCard(){
		if (GlobalVariables.m_instance.CardBoardBuy == 0) {
			for (int i = 0; i < 12; i++) {
				imgCardBoard [i].color = TemplateColor.m_instance.ColorsList[GlobalVariables.m_instance.IDTemplate].ColorCardsNoSelected;
				Cards.CardsBoards [i].isSelected = false;
				Cards.CardsBoards [i].transform.parent.gameObject.GetComponent<Button> ().interactable = false;
			}
		}
	}

	public void SelectCardAuto(int NbrBuy){
		for (int i = 0; i < 12; i++) {
			if (i < NbrBuy) {
				GlobalVariables.m_instance.CardBoardSelected = NbrBuy;
				imgCardBoard [i].color = TemplateColor.m_instance.ColorsList[GlobalVariables.m_instance.IDTemplate].ColorCardsSelected;
				Cards.CardsBoards [i].isSelected = true;
			} else {
				GlobalVariables.m_instance.CardBoardSelected = NbrBuy;
				imgCardBoard [i].color = TemplateColor.m_instance.ColorsList[GlobalVariables.m_instance.IDTemplate].ColorCardsNoSelected;
				Cards.CardsBoards [i].isSelected = false;
			}
		}
		TotalPrice();
	}

	public void OpenBuy(){
		float totalPrice = GlobalVariables.m_instance.stakesSelected * GlobalVariables.m_instance.CardBoardSelected;
		if (GlobalVariables.m_instance.Money >= totalPrice) {
			if (GlobalVariables.m_instance.CardBoardSelected > 0) {
				GlobalUI.m_instance.validateBuy.interactable = false;
				GlobalUI.m_instance.WindowsBuy.SetActive (true);
				GlobalUI.m_instance.NbrCard.text = GlobalVariables.m_instance.CardBoardSelected.ToString ("0");
				GlobalUI.m_instance.PriceCard.text = "$" + GlobalVariables.m_instance.stakesSelected.ToString ("0.##");
				GlobalUI.m_instance.TotalPrice.text = "$" + totalPrice.ToString ("0.##");
			}
		}
		GlobalMusic.m_instance.PlayClickSound ();
	}

	public void SendBuyToMaster(){
		photonView.RPC ("RequestBuy", PhotonTargets.MasterClient, PhotonNetwork.player, GlobalVariables.m_instance.CardBoardSelected);
		GlobalMusic.m_instance.PlayClickSound ();
	}

	[PunRPC]
	public void RequestBuy(PhotonPlayer photonP, int NbrCards){
		if (!GlobalVariables.m_instance.GameIsReady) {
			GlobalVariables.m_instance.PutInPlay += NbrCards * GlobalVariables.m_instance.stakesSelected;
			GlobalVariables.m_instance.NbrPlayersIsReady++;
			foreach(MPPlayer player in ManageListMembers.m_instance.playersList.ToArray()){
				player.NbrCardBoards = NbrCards;
			}
			ManageListMembers.m_instance.RpcAddPlayerToList ();
			photonView.RPC ("ReceiveForBuy", photonP, "yes");
		} else {
			photonView.RPC ("ReceiveForBuy", photonP, "no");
		}
		photonView.RPC ("RefreshPutAndPlayText", PhotonTargets.All, GlobalVariables.m_instance.PutInPlay);
	}

	[PunRPC]
	public void ReceiveForBuy(string response){
		if (response == "yes") {
			GlobalVariables.m_instance.InCurrentGame = true;
			float totalPrice = GlobalVariables.m_instance.stakesSelected * GlobalVariables.m_instance.CardBoardSelected;
			GlobalVariables.m_instance.CardBoardBuy = GlobalVariables.m_instance.CardBoardSelected;
			GlobalVariables.m_instance.Money = GlobalVariables.m_instance.Money - totalPrice;
			GlobalUI.m_instance.PrebuyMoneyText.text = "Total : $0";
			GlobalUI.m_instance.MoneyText.text = "Money's\n$" + GlobalVariables.m_instance.Money.ToString ("0.##");
			GlobalUI.m_instance.BlockValidateBuy.SetActive (false);
			GlobalUI.m_instance.WindowsBuy.SetActive (false);
			GlobalUI.m_instance.RefreshBtn.SetActive (false);
			Cards.DisableCardBoard ();
		} else if (response == "no") {
			if (GlobalUI.m_instance.WindowsBuy.activeSelf) {
				GlobalUI.m_instance.WindowsBuy.SetActive (false);
			}
			GlobalUI.m_instance.WindowsAlert.SetActive (true);
			GlobalUI.m_instance.AlertText.text = "Your cards could not be saved!";
		}
	}

	[PunRPC]
	public void RefreshPutAndPlayText(float putandplay){
		GlobalVariables.m_instance.PutInPlay = putandplay;
		GlobalUI.m_instance.putInPlay.text = "$" + GlobalVariables.m_instance.PutInPlay.ToString ("0.##");
	}

	void TotalPrice(){
		float totalPrice = GlobalVariables.m_instance.stakesSelected * GlobalVariables.m_instance.CardBoardSelected;

		if(GlobalVariables.m_instance.CardBoardSelected > 0){
			GlobalUI.m_instance.validateBuy.interactable = true;
			GlobalUI.m_instance.PrebuyMoneyText.text = "Total : $" + totalPrice.ToString ("0.##");
		}else if(GlobalVariables.m_instance.CardBoardSelected <= 0){
			GlobalUI.m_instance.validateBuy.interactable = false;
			GlobalUI.m_instance.PrebuyMoneyText.text = "Total : $0";
		}
		GlobalMusic.m_instance.PlayClickSound ();
	}
}
