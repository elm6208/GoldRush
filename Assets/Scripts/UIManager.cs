using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	Text moneyText;
	Text livesText;
	Text waveText;
	GameObject tower1Button;
	GameObject tower2Button;
	GameObject tower3Button;
	GameObject tower4Button;
	GameObject promoteButton;
	GameObject sellButton;
	GameObject selectedUnitImg;
	GameObject unitNameText;
	GameObject fireRateText;
	GameObject damageText;
	GameObject rangeText;

	GameController gameController;

	// Use this for initialization
	void Start () {
		moneyText = transform.Find("MoneyText").GetComponent<Text>();
		livesText = transform.Find ("LivesText").GetComponent<Text>();
		waveText = transform.Find ("WaveText").GetComponent<Text>();
		tower1Button = transform.Find ("Tower1Button").gameObject;
		tower2Button = transform.Find ("Tower2Button").gameObject;
		tower3Button = transform.Find ("Tower3Button").gameObject;
		tower4Button = transform.Find ("Tower4Button").gameObject;
		selectedUnitImg = transform.Find ("SelectedUnitImg").gameObject;
		unitNameText = transform.Find ("SelectedUnitImg").gameObject;
		fireRateText = transform.Find ("FireRateText").gameObject;
		damageText = transform.Find ("DamageText").gameObject;
		rangeText = transform.Find ("RangeText").gameObject;

		gameController = GameObject.FindWithTag ("MainCamera").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = "ca$h money: " + gameController.money;
		livesText.text = "lives: " + gameController.lives;
		waveText.text = "wave: " + gameController.CurrentWave;
	}
}
