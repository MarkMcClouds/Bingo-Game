using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeCardBoard : MonoBehaviour {

	public GameObject[] Ball;
	public bool isSelected = false;
	public int remaningBallsCard = 15;
	public Sprite squareRound;
	public Sprite circle;

	int step = 0;

	void Start(){
		initCardBoard ();
	}

	public void initCardBoard () {
		
		//RANDOM NUMBER BALL 1 TO 9
		List<int> Random1To9 = new List<int>(9);
		for (int i = 1; i < 10; i++) {
			Random1To9.Add(i);
		}
		var NumbersBall01To03 = new int[3];
		for (int i = 0; i < NumbersBall01To03.Length; i++) {
			int thisNumber = Random.Range(0, Random1To9.Count);
			NumbersBall01To03[i] = Random1To9[thisNumber];
			Random1To9.RemoveAt(thisNumber);
		}
		for (int i = 0; i < 3; i++) {
			Ball[i].GetComponent<Ball>().textBall.text = NumbersBall01To03[i].ToString("0");
		}

		//RANDOM NUMBER BALL 10 TO 20//////////////////////////////////////////////////////////
		List<int> Random10To19 = new List<int>(10);
		for (int i = 10; i < 20; i++) {
			Random10To19.Add(i);
		}
		var NumbersBall10To20 = new int[3];
		for (int i = 0; i < NumbersBall10To20.Length; i++) {
			int thisNumber = Random.Range(0, Random10To19.Count);
			NumbersBall10To20[i] = Random10To19[thisNumber];
			Random10To19.RemoveAt(thisNumber);
		}
		for (int i = 3; i < 6; i++) {
			Ball[i].GetComponent<Ball>().textBall.text = NumbersBall10To20[i-3].ToString("0");
		}

		//RANDOM NUMBER BALL 20 TO 30//////////////////////////////////////////////////////////
		List<int> Random20To29 = new List<int>(10);
		for (int i = 20; i < 30; i++) {
			Random20To29.Add(i);
		}
		var NumbersBall20To29 = new int[3];
		for (int i = 0; i < NumbersBall20To29.Length; i++) {
			int thisNumber = Random.Range(0, Random20To29.Count);
			NumbersBall20To29[i] = Random20To29[thisNumber];
			Random20To29.RemoveAt(thisNumber);
		}
		for (int i = 6; i < 9; i++) {
			Ball[i].GetComponent<Ball>().textBall.text = NumbersBall20To29[i-6].ToString("0");
		}

		//RANDOM NUMBER BALL 30 TO 39//////////////////////////////////////////////////////////
		List<int> Random30To39 = new List<int>(10);
		for (int i = 30; i < 40; i++) {
			Random30To39.Add(i);
		}
		var NumbersBall30To39 = new int[3];
		for (int i = 0; i < NumbersBall30To39.Length; i++) {
			int thisNumber = Random.Range(0, Random30To39.Count);
			NumbersBall30To39[i] = Random30To39[thisNumber];
			Random30To39.RemoveAt(thisNumber);
		}
		for (int i = 9; i < 12; i++) {
			Ball[i].GetComponent<Ball>().textBall.text = NumbersBall30To39[i-9].ToString("0");
		}

		//RANDOM NUMBER BALL 40 TO 49//////////////////////////////////////////////////////////
		List<int> Random40To49 = new List<int>(10);
		for (int i = 40; i < 50; i++) {
			Random40To49.Add(i);
		}
		var NumbersBall40To49 = new int[3];
		for (int i = 0; i < NumbersBall40To49.Length; i++) {
			int thisNumber = Random.Range(0, Random40To49.Count);
			NumbersBall40To49[i] = Random40To49[thisNumber];
			Random40To49.RemoveAt(thisNumber);
		}
		for (int i = 12; i < 15; i++) {
			Ball[i].GetComponent<Ball>().textBall.text = NumbersBall40To49[i-12].ToString("0");
		}

		//RANDOM NUMBER BALL 50 TO 59//////////////////////////////////////////////////////////
		List<int> Random50To59 = new List<int>(10);
		for (int i = 50; i < 60; i++) {
			Random50To59.Add(i);
		}
		var NumbersBall50To59 = new int[3];
		for (int i = 0; i < NumbersBall50To59.Length; i++) {
			int thisNumber = Random.Range(0, Random50To59.Count);
			NumbersBall50To59[i] = Random50To59[thisNumber];
			Random50To59.RemoveAt(thisNumber);
		}
		for (int i = 15; i < 18; i++) {
			Ball[i].GetComponent<Ball>().textBall.text = NumbersBall50To59[i-15].ToString("0");
		}

		//RANDOM NUMBER BALL 60 TO 69//////////////////////////////////////////////////////////
		List<int> Random60To69 = new List<int>(10);
		for (int i = 60; i < 70; i++) {
			Random60To69.Add(i);
		}
		var NumbersBall60To69 = new int[3];
		for (int i = 0; i < NumbersBall60To69.Length; i++) {
			int thisNumber = Random.Range(0, Random60To69.Count);
			NumbersBall60To69[i] = Random60To69[thisNumber];
			Random60To69.RemoveAt(thisNumber);
		}
		for (int i = 18; i < 21; i++) {
			Ball[i].GetComponent<Ball>().textBall.text = NumbersBall60To69[i-18].ToString("0");
		}

		//RANDOM NUMBER BALL 70 TO 79//////////////////////////////////////////////////////////
		List<int> Random70To79 = new List<int>(10);
		for (int i = 70; i < 80; i++) {
			Random70To79.Add(i);
		}
		var NumbersBall70To79 = new int[3];
		for (int i = 0; i < NumbersBall70To79.Length; i++) {
			int thisNumber = Random.Range(0, Random70To79.Count);
			NumbersBall70To79[i] = Random70To79[thisNumber];
			Random70To79.RemoveAt(thisNumber);
		}
		for (int i = 21; i < 24; i++) {
			Ball[i].GetComponent<Ball>().textBall.text = NumbersBall70To79[i-21].ToString("0");
		}

		//RANDOM NUMBER BALL 80 TO 90//////////////////////////////////////////////////////////
		List<int> Random80To90 = new List<int>(11);
		for (int i = 80; i < 91; i++) {
			Random80To90.Add(i);
		}
		var NumbersBall80To90 = new int[3];
		for (int i = 0; i < NumbersBall80To90.Length; i++) {
			int thisNumber = Random.Range(0, Random80To90.Count);
			NumbersBall80To90[i] = Random80To90[thisNumber];
			Random80To90.RemoveAt(thisNumber);
		}
		for (int i = 24; i < 27; i++) {
			Ball[i].GetComponent<Ball>().textBall.text = NumbersBall80To90[i-24].ToString("0");
		}

		//DISABLE 12 BALLS BY CARDBOARD 4 BY LINE//////////////////////////////////////////////////////////

		List<int> idBallLine1 = new List<int> { 0, 3, 6, 9, 12, 15, 18, 21, 24 };
		List<int> idBallLine2 = new List<int> { 1, 4, 7, 10, 13, 16, 19, 22, 25 };
		List<int> idBallLine3 = new List<int> { 2, 5, 8, 11, 14, 17, 20, 23, 26 };

		for (int i = 0; i < 4; i++) {
			int Line1thisNumber = Random.Range (0, idBallLine1.Count);
			int Line1idBallDisable = idBallLine1 [Line1thisNumber];
			idBallLine1.RemoveAt (Line1thisNumber);
			Ball [Line1idBallDisable].GetComponent<Ball> ().textBall.text = "";

			int Line2thisNumber = Random.Range (0, idBallLine2.Count);
			int Line2idBallDisable = idBallLine2 [Line2thisNumber];
			idBallLine2.RemoveAt (Line2thisNumber);
			Ball [Line2idBallDisable].GetComponent<Ball> ().textBall.text = "";

			int Line3thisNumber = Random.Range (0, idBallLine3.Count);
			int Line3idBallDisable = idBallLine3 [Line3thisNumber];
			idBallLine3.RemoveAt (Line3thisNumber);
			Ball [Line3idBallDisable].GetComponent<Ball> ().textBall.text = "";
		}
	}

	public void CheckIfNumber(int nbr){
		foreach (GameObject Ballnbr in Ball) {
			if (Ballnbr.GetComponent<Ball> ().textBall.text == nbr.ToString ()) {
				Ballnbr.GetComponent<Ball> ().imgBall.color = TemplateColor.m_instance.ColorsList[GlobalVariables.m_instance.IDTemplate].ColorBallsSelected;
				Ballnbr.GetComponent<Ball> ().imgBall.sprite = circle;
				Ballnbr.GetComponent<Ball> ().textBall.color = Color.white;
				Ballnbr.GetComponent<Ball> ().isSelected = true;
			}
		}
		CheckIfBingo ();
	}

	public void CheckIfBingo(){
		int count = 0;

		for (int i = 0; i < 27; i++) {
			if (Ball [i].GetComponent<Ball> ().isSelected) {
				count++;
				remaningBallsCard = 15 - count;
			}
		}

		if (remaningBallsCard == 1) {
			ChangeColor ("NoBingo");
		}else if (remaningBallsCard == 0) {
			ChangeColor ("Bingo");
		}
	}

	void ChangeColor (string id){
		if (id == "NoBingo") {
			foreach (GameObject Ballnbr in Ball) {
				if (Ballnbr.GetComponent<Ball> ().isSelected) {
					Ballnbr.GetComponent<Ball> ().imgBall.color = TemplateColor.m_instance.ColorsList[GlobalVariables.m_instance.IDTemplate].ColorBallsNearBingo;
				}
			}
			this.transform.parent.gameObject.GetComponent<Image> ().color = TemplateColor.m_instance.ColorsList[GlobalVariables.m_instance.IDTemplate].ColorCardNearBingo;
		} else if(id == "Bingo"){
			foreach (GameObject Ballnbr in Ball) {
				Ballnbr.GetComponent<Ball> ().imgBall.color = Color.white;
				Ballnbr.GetComponent<Ball> ().imgBall.sprite = circle;
				Ballnbr.GetComponent<Ball> ().textBall.color = new Color(0.2F, 0.2F, 0.2F, 1F);
				Ballnbr.GetComponent<Ball> ().isSelected = true;
			}
			this.transform.parent.gameObject.GetComponent<Image> ().color = TemplateColor.m_instance.ColorsList[GlobalVariables.m_instance.IDTemplate].ColorCardsNoSelected;
			GameManager.m_instance.Bingo ();
		}
	}

	public void ResetCard(){
		foreach (GameObject Ballnbr in Ball) {
			Ballnbr.GetComponent<Ball> ().imgBall.color = Color.white;
			Ballnbr.GetComponent<Ball> ().imgBall.sprite = circle;
			Ballnbr.GetComponent<Ball> ().textBall.color = new Color(0.2F, 0.2F, 0.2F, 1F);
			Ballnbr.GetComponent<Ball> ().isSelected = false;
		}
		this.transform.parent.gameObject.GetComponent<Image> ().color = TemplateColor.m_instance.ColorsList[GlobalVariables.m_instance.IDTemplate].ColorCardsNoSelected;
		this.transform.parent.gameObject.GetComponent<Button> ().interactable = true;
		remaningBallsCard = 15;
		isSelected = false;
		GlobalVariables.m_instance.CardBoardSelected = 0;
		GlobalUI.m_instance.PrebuyMoneyText.text = "Total : $0";
		initCardBoard ();
	}

	public int sendRemaning(){
		return remaningBallsCard;
	}
}
