using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	Text moneyText;
	Text livesText;
	Text waveText;
	UnityEngine.UI.Button tower1Button;
	UnityEngine.UI.Button tower2Button;
	UnityEngine.UI.Button tower3Button;
	UnityEngine.UI.Button tower4Button;
	UnityEngine.UI.Button promoteButton;
	UnityEngine.UI.Button sellButton;
	GameObject selectedUnitImg;
	Text unitNameText;
    Text fireRateText;
    Text damageText;
    Text rangeText;

    Tower DisplayedTower;

	public Tower GetDisplayTower() {
		return DisplayedTower;
	}

	GameController gameController;

	ColorBlock buttonColorEnabled;
	ColorBlock buttonColorDisabled;

	// keeping previous money value so we can update stuff only when this changes
	int prevMoney = -1;

	// Use this for initialization
	void Start () {
		buttonColorEnabled = new ColorBlock ();
		buttonColorDisabled = new ColorBlock ();
		buttonColorEnabled.normalColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		buttonColorDisabled.normalColor = new Color(0.64f, 0.46f, 0.46f, 1.0f);
		buttonColorEnabled.highlightedColor = new Color (1.0f, 1.0f, 0.0f, 1.0f);
		buttonColorDisabled.highlightedColor = new Color (0.5f, 0.5f, 0.5f, 1.0f);
		buttonColorEnabled.colorMultiplier = 1.0f;
		buttonColorDisabled.colorMultiplier = 1.0f;

		moneyText = transform.Find("MoneyText").GetComponent<Text>();
		livesText = transform.Find ("LivesText").GetComponent<Text>();
		waveText = transform.Find ("WaveText").GetComponent<Text>();
		tower1Button = transform.Find ("Tower1Button").gameObject.GetComponent<UnityEngine.UI.Button>();
		tower2Button = transform.Find ("Tower2Button").gameObject.GetComponent<UnityEngine.UI.Button>();
		tower3Button = transform.Find ("Tower3Button").gameObject.GetComponent<UnityEngine.UI.Button>();
		tower4Button = transform.Find ("Tower4Button").gameObject.GetComponent<UnityEngine.UI.Button>();
		selectedUnitImg = transform.Find ("SelectedUnitImg").gameObject;
		unitNameText = transform.Find ("UnitNameText").GetComponent<Text>();
        fireRateText = transform.Find ("FireRateText").GetComponent<Text>();
        damageText = transform.Find ("DamageText").GetComponent<Text>();
        rangeText = transform.Find ("RangeText").GetComponent<Text>();
        promoteButton = transform.Find("PromoteButton").gameObject.GetComponent<UnityEngine.UI.Button>();
        sellButton = transform.Find("SellButton").gameObject.GetComponent<UnityEngine.UI.Button>();

        gameController = GameObject.FindWithTag ("MainCamera").GetComponent<GameController>();


		tower1Button.onClick.AddListener(() => {
			SelectTower(TowerType.BASIC);
		});
		tower2Button.onClick.AddListener(() => {
			SelectTower(TowerType.DYNAMITE);
		});
		tower3Button.onClick.AddListener(() => {
			SelectTower(TowerType.TOWER3);
		});
		tower4Button.onClick.AddListener(() => {
			SelectTower(TowerType.TOWER4);
		});

        promoteButton.onClick.AddListener(() => {
            upgradeTower();
        });
        sellButton.onClick.AddListener(() => {
           sellTower();
        });
    }

	private void SelectTower(TowerType towerType) {
		if (gameController.money >= towerType.Cost()) {
			gameController.SetPlacer (towerType);
		}
	}

    private void upgradeTower(){
        DisplayedTower.Promote();
    }

    private void sellTower(){
        DisplayedTower.Sell();
    }

	
	// Update is called once per frame
	void Update () {
		livesText.text = "lives: " + gameController.lives;
		waveText.text = "wave: " + gameController.CurrentWave;

		int money = gameController.money;
		if (money != prevMoney) {
			moneyText.text = "ca$h money: " + money;
			tower1Button.colors = money >= TowerType.BASIC.Cost() ? buttonColorEnabled : buttonColorDisabled;
			tower2Button.colors = money >= TowerType.DYNAMITE.Cost() ? buttonColorEnabled : buttonColorDisabled;
			tower3Button.colors = money >= TowerType.TOWER3.Cost() ? buttonColorEnabled : buttonColorDisabled;
			tower4Button.colors = money >= TowerType.TOWER4.Cost() ? buttonColorEnabled : buttonColorDisabled;
		}



        prevMoney = money;
	}

    public void updateTowerDisplay(Tower newTower){
        DisplayedTower = newTower;

        //selectedUnitImg = 
        unitNameText.text = "Name:" + DisplayedTower.towerName;
        fireRateText.text = "Fire Rate: " + DisplayedTower.fireRate;
        damageText.text = "Damage: " + DisplayedTower.damage;
        rangeText.text = "Range: " + DisplayedTower.range;

    }
}
