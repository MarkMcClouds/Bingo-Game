using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUI : MonoBehaviour {

	public static GlobalUI m_instance;

	[Header("All Images")]
	public Image LogoBingo;
	public Image LogoTemplateRoom;
	public Image JackpotImg;
	public Image ParticipantIcon;
	public Image ClockImg;
	public Sprite[] logosBingo;
	public Sprite[] logosSkinRoom;
	public Image BuyButton;
	public Image BuyWindowsBackground;
	public Image AlertWindowsBackground;

	[Header("All Texts")]
	public Text Top5Title;
	public Text LoginBingo;
	public Text ChangeCardsTitle;
	public Text putInPlayTitle;
	public Text putInPlay;
	public Text JackpotText;
	public Text AtBallJackpotText;
	public Text NbrCard;
	public Text PriceCard;
	public Text TotalPrice;
	public Text NbrPrint;
	public Text AlertText;
	public Text ValidateText;
	public Text PrebuyMoneyText;
	public Text MoneyText;
	public Text Avertiss;
	public Text GlobalParticipants;
	public Text TitleNbrCardsBuy;
	public Text TitleCardsPrice;
	public Text TitlePriceTotal;
	public Text TextBuyButton;
	public Text pseudoText;

	[Header("All GameObjects")]
	public GameObject[] Panels;
	public GameObject BackgroundPanelGame;
	public GameObject LeftBlock;
	public GameObject BottomBlock;
	public GameObject Loading;
	public GameObject BlockTimer;
	public GameObject Top5Block;
	public GameObject BingoWindows;
	public GameObject RecapWindows;
	public GameObject AllCards;
	public GameObject AlertForNextGame;
	public GameObject BallPrint;
	public GameObject WindowsBuy;
	public GameObject WindowsAlert;
	public GameObject BlockValidateBuy;
	public GameObject RefreshBtn;

	[Header("Others")]
	public Button btn_play;
	public InputField loginInput;
	public Button validateBuy;


	void Awake () {
		m_instance = this;
	}

	public void ShowSelectBingoPanels(){
		Panels[0].SetActive (false);
		Panels[1].SetActive (true);
	}

	public void ShowGamePanel(){
		Panels [1].SetActive (false);
		Panels [2].SetActive (true);
	}

	public void CloseWindowsBuy(){
		GlobalUI.m_instance.WindowsBuy.SetActive (false);
		GlobalUI.m_instance.validateBuy.interactable = true;
	}

	public void CloseWindowsAlert(){
		GlobalUI.m_instance.WindowsAlert.SetActive (false);
	}

	public IEnumerator HideLoading(){
		yield return new WaitForSeconds (1f);
		if (Loading.activeSelf) {
			Loading.SetActive (false);
		}
		StopCoroutine (HideLoading());
	}

}
