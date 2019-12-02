using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AllCardBoards : Photon.MonoBehaviour {

	public static AllCardBoards m_instance;
	public RangeCardBoard[] CardsBoards;


	void Awake(){
		m_instance = this;
	}

	//FUNCTION WHEN CLICK REFRESH BUTTON FOR REFRESH CARDBOARD
	public void RefreshCardBoard () {
		GlobalMusic.m_instance.PlayClickSound ();
		if (GlobalUI.m_instance.validateBuy.IsInteractable()) {
			GlobalUI.m_instance.validateBuy.interactable = false;
		}
		foreach(RangeCardBoard Cards in CardsBoards){
			Cards.ResetCard ();
		}
	}

	public void DisableCardBoard () {
		foreach(RangeCardBoard Cards in CardsBoards){
			Cards.transform.parent.gameObject.GetComponent<Button> ().interactable = false;
			if (!Cards.isSelected) {
				Cards.transform.parent.gameObject.SetActive (false);
			} else {
				Cards.transform.parent.gameObject.GetComponent<Image> ().color = TemplateColor.m_instance.ColorsList [GlobalVariables.m_instance.IDTemplate].ColorCardIsBuy;
			}
		}
	}

	public void ResetCardBoard () {
		foreach(RangeCardBoard Cards in CardsBoards){
			if (!Cards.transform.parent.gameObject.activeSelf) {
				Cards.transform.parent.gameObject.SetActive (true);
			}
			Cards.ResetCard ();
		}
	}

	public void SendLastnumber(int nbr){
		if (!GlobalVariables.m_instance.Bingo) {
			List<int> RemaningBalls = new List<int> (12);
			foreach (RangeCardBoard Cards in CardsBoards) {
				if (Cards.transform.parent.gameObject.activeSelf) {
					if (Cards.isSelected) {
						Cards.CheckIfNumber (nbr);
						RemaningBalls.Add (Cards.sendRemaning ());
					}
				}
			}
			int NbrRemaning = 0;
			if (RemaningBalls.Count > 0) {
				NbrRemaning = RemaningBalls.Min ();
			}
			ManageListMembers.m_instance.photonView.RPC ("refreshRemaningBallMaster", PhotonTargets.MasterClient, PhotonNetwork.player, NbrRemaning);
			if (PhotonNetwork.isMasterClient) {
				if (!GlobalVariables.m_instance.Bingo) {
					GameManager.m_instance.StartCoroutine (GameManager.m_instance.LaunchPrint ());
				}
			}
		}
	}
}
