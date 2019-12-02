using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalVariables : MonoBehaviour {

	public static GlobalVariables m_instance;

	public string pseudo;
	public float Money = 100000f;
	public float Jackpot = 200f;
	public float AtBallJackpot = 33f;
	public float stakesSelected = 0f;
	public bool GameIsReady = false;
	public bool InCurrentGame = false;
	public bool IsReady = false;
	public bool Bingo = false;
	public int CardBoardSelected = 0;
	public int CardBoardBuy = 0;
	public int RemainingBalls = 15;
	public float PutInPlay = 0f;
	public int NbrPrint = 0;
	public int NbrPlayersIsReady = 0;
	public int IDTemplate = 0;

	void Awake () {
		m_instance = this;
	}

	public void initJackpot(){
		GlobalUI.m_instance.JackpotText.text = "$" + GlobalVariables.m_instance.Jackpot.ToString ();
		GlobalUI.m_instance.AtBallJackpotText.text = "IN " + GlobalVariables.m_instance.AtBallJackpot.ToString () + " BALLS!";
	}
}
