using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemplateColor : MonoBehaviour {

	public static TemplateColor m_instance;
	public List<MColors> ColorsList = new List<MColors> ();

	// Use this for initialization
	void Awake () {
		m_instance = this;
	}
		
	public void initTemplate(){
		GlobalUI.m_instance.BackgroundPanelGame.GetComponent<Image> ().color = ColorsList[GlobalVariables.m_instance.IDTemplate].ColorBackground;
		GlobalUI.m_instance.BuyWindowsBackground.GetComponent<Image> ().color = ColorsList[GlobalVariables.m_instance.IDTemplate].ColorWindowsBuy;
		GlobalUI.m_instance.AlertWindowsBackground.GetComponent<Image> ().color = ColorsList[GlobalVariables.m_instance.IDTemplate].ColorWindowsAlert;
		GlobalUI.m_instance.LeftBlock.GetComponent<Image> ().color = ColorsList[GlobalVariables.m_instance.IDTemplate].ColorLeftBlock;
		GlobalUI.m_instance.BottomBlock.GetComponent<Image> ().color = ColorsList[GlobalVariables.m_instance.IDTemplate].ColorBottomBlock;
		GlobalUI.m_instance.LogoTemplateRoom.sprite = GlobalUI.m_instance.logosSkinRoom [GlobalVariables.m_instance.IDTemplate];
		GlobalUI.m_instance.validateBuy.gameObject.GetComponent<Image> ().color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorAllButton;
		GlobalUI.m_instance.pseudoText.text = GlobalVariables.m_instance.pseudo;
		foreach(RangeCardBoard Cards in AllCardBoards.m_instance.CardsBoards){
			Cards.transform.parent.GetComponent<Image> ().color = TemplateColor.m_instance.ColorsList [GlobalVariables.m_instance.IDTemplate].ColorCardsNoSelected;
			foreach(GameObject ball in Cards.Ball){
				ball.GetComponent<Ball>().imgBall.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorBallsInCard;
				ball.GetComponent<Ball>().textBall.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorTextBallsInCard;
			}
		}
		GlobalUI.m_instance.JackpotImg.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorJackpotImg;
		GlobalUI.m_instance.JackpotImg.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.Avertiss.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorBlockTimer;
		GlobalUI.m_instance.Avertiss.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.JackpotText.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorJackpotText;
		GlobalUI.m_instance.JackpotText.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.AtBallJackpotText.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorAtBallText;
		GlobalUI.m_instance.AtBallJackpotText.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.ValidateText.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorValidateButtonText;
		GlobalUI.m_instance.ValidateText.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.PrebuyMoneyText.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorPrebuyMoneyText;
		GlobalUI.m_instance.PrebuyMoneyText.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.NbrPrint.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorNbrPrint;
		GlobalUI.m_instance.NbrPrint.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.ClockImg.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorClockImg;
		GlobalUI.m_instance.ClockImg.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.AlertForNextGame.GetComponent<Text>().color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorClockImg;
		GlobalUI.m_instance.AlertForNextGame.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.ParticipantIcon.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorParticipantIcon;
		GlobalUI.m_instance.ParticipantIcon.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.GlobalParticipants.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorGlobalParticipants;
		GlobalUI.m_instance.GlobalParticipants.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.MoneyText.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorMoneyText;
		GlobalUI.m_instance.MoneyText.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.Top5Title.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorTop5Title;
		GlobalUI.m_instance.Top5Title.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.putInPlay.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorputInPlay;
		GlobalUI.m_instance.putInPlay.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.putInPlayTitle.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorputInPlayTitle;
		GlobalUI.m_instance.putInPlayTitle.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.ChangeCardsTitle.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorChangeCardsTitle;
		GlobalUI.m_instance.ChangeCardsTitle.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.TitleNbrCardsBuy.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorTitleNbrCardsBuy;
		GlobalUI.m_instance.TitleNbrCardsBuy.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.TitleCardsPrice.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorTitleCardsPrice;
		GlobalUI.m_instance.TitleCardsPrice.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.TitlePriceTotal.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorTitlePriceTotal;
		GlobalUI.m_instance.TitlePriceTotal.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.BuyButton.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorAllButton;

		GlobalUI.m_instance.TextBuyButton.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorTextButtonBuy;
		GlobalUI.m_instance.TextBuyButton.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.AlertText.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorAlertText;
		GlobalUI.m_instance.AlertText.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

		GlobalUI.m_instance.pseudoText.color = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorPseudoText;
		GlobalUI.m_instance.pseudoText.gameObject.GetComponent<Shadow>().effectColor = ColorsList [GlobalVariables.m_instance.IDTemplate].ColorShadow;

	}
}

[System.Serializable]
public class MColors
{
	public Color ColorCardIsBuy = new Color();

	public Color ColorCardNearBingo = new Color();
	public Color ColorBallsNearBingo = new Color();

	public Color ColorBallsInCard = new Color();
	public Color ColorTextBallsInCard = new Color();
	public Color ColorBallsSelected = new Color();
	public Color ColorCardsSelected = new Color();
	public Color ColorCardsNoSelected = new Color();

	public Color ColorLeftBlock = new Color();
	public Color ColorBottomBlock = new Color();
	public Color ColorBackground = new Color();
	public Color ColorBlockTimer = new Color();
	public Color ColorJackpotImg = new Color();
	public Color ColorJackpotText = new Color();
	public Color ColorAtBallText = new Color();
	public Color ColorPseudoText = new Color();
	public Color ColorValidateButtonText = new Color();
	public Color ColorPrebuyMoneyText = new Color();
	public Color ColorNbrPrint = new Color();
	public Color ColorClockImg = new Color();
	public Color ColorAlertForNextGame = new Color();
	public Color ColorParticipantIcon = new Color();
	public Color ColorGlobalParticipants = new Color();
	public Color ColorMoneyText = new Color();
	public Color ColorTop5Title = new Color();
	public Color ColorTop5Bar = new Color();
	public Color ColorTop5BarText = new Color();
	public Color ColorTop5BarTextOneBall = new Color();
	public Color ColorTop5BarOneBall = new Color();
	public Color ColorputInPlay = new Color();
	public Color ColorputInPlayTitle = new Color();
	public Color ColorChangeCardsTitle = new Color();
	public Color ColorTitleNbrCardsBuy = new Color();
	public Color ColorTitleCardsPrice = new Color();
	public Color ColorTitlePriceTotal = new Color();
	public Color ColorWindowsBuy = new Color();
	public Color ColorWindowsAlert = new Color();
	public Color ColorTextButtonBuy = new Color();
	public Color ColorAlertText = new Color();
	public Color ColorAllButton = new Color();
	public Color ColorShadow = new Color();
}