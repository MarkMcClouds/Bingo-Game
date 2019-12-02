using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerifLogin : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (GlobalUI.m_instance.loginInput.text.Length >= 4) {//it checks if the number of characters is greater than or equal to 4
			GlobalUI.m_instance.btn_play.interactable = true;
		} else if (GlobalUI.m_instance.loginInput.text.Length < 4) {//it checks if the number of characters is less than 4
			GlobalUI.m_instance.btn_play.interactable = false;
		}
	}
}
