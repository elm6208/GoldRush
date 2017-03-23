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
	UnityEngine.UI.Button nextWaveButton;
	public Sprite gun, tnt, barrel, badge;
	Image selectedIcon;
	Text unitNameText;
  Text fireRateText;
  Text damageText;
  Text rangeText;

  Text promoteHoverText;
	Text sellHoverText;

  Tower DisplayedTower;

	public Tower GetDisplayTower() {
		return DisplayedTower;
	}

	GameController gameController;

	ColorBlock buttonColorEnabled;
	ColorBlock buttonColorDisabled;

	// keeping previous money value so we can update stuff only when this changes
	int prevMoney = -1;

	GameObject helpButton;
	GameObject hireSomePeeps;
	GameObject selectTowerHelp;

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
		nextWaveButton = transform.Find("NextWaveButton").gameObject.GetComponent<UnityEngine.UI.Button>();
		selectedIcon = transform.Find ("SelectedIcon").gameObject.GetComponent<Image>();
		unitNameText = transform.Find ("UnitNameText").GetComponent<Text>();
    fireRateText = transform.Find ("FireRateText").GetComponent<Text>();
    damageText = transform.Find ("DamageText").GetComponent<Text>();
    rangeText = transform.Find ("RangeText").GetComponent<Text>();
  	promoteButton = transform.Find("PromoteButton").gameObject.GetComponent<UnityEngine.UI.Button>();
    sellButton = transform.Find("SellButton").gameObject.GetComponent<UnityEngine.UI.Button>();

    promoteHoverText = transform.Find("PromoteHoverText").gameObject.GetComponent<Text>();
		sellHoverText = transform.Find("SellHoverText").gameObject.GetComponent<Text>();

    gameController = GameObject.FindWithTag ("MainCamera").GetComponent<GameController>();

		tower1Button.onClick.AddListener(() => {
			SelectTower(TowerType.BASIC);
		});
		tower2Button.onClick.AddListener(() => {
			SelectTower(TowerType.DYNAMITE);
		});
		tower3Button.onClick.AddListener(() => {
			SelectTower(TowerType.SLOW);
		});
		tower4Button.onClick.AddListener(() => {
           Sheriff_Tower sheriff = FindObjectOfType<Sheriff_Tower>();
            if(sheriff != null)
            {
                sheriff.Sell();
            }
			SelectTower(TowerType.TOWER4);
		});

    promoteButton.onClick.AddListener(() => {
        upgradeTower();
    });
    sellButton.onClick.AddListener(() => {
       sellTower();
    });

		nextWaveButton.onClick.AddListener(() => {
			gameController.StartWave();
		});

		hireSomePeeps = transform.Find("HireSomePeeps").gameObject;
		hireSomePeeps.transform.Find ("HirePeepsButton").gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
			gameController.progressTutorial(2);
		});

		selectTowerHelp = transform.Find("SelectTowerHelp").gameObject;
		selectTowerHelp.transform.Find ("SelectTowerHelpButton").gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
			gameController.progressTutorial(-1);
		});

		helpButton = transform.Find("HelpButton").gameObject;
		helpButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
			gameController.progressTutorial(0);
		});

		RefreshTowerDisplay();
  }

	private void SelectTower(TowerType towerType) {
		if (gameController.money >= towerType.Cost()) {
			gameController.SetPlacer (towerType);
		}
	}

  private void upgradeTower() {
      if (DisplayedTower != null && gameController.money >= DisplayedTower.promoteCost)
      {
          gameController.money -= DisplayedTower.promoteCost;
          DisplayedTower.Promote();
					RefreshTowerDisplay();
      }

  }

  private void sellTower() {
      if(DisplayedTower != null)
      {
          DisplayedTower.Sell();
					DisplayedTower = null;
					RefreshTowerDisplay();
      }

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
			tower3Button.colors = money >= TowerType.SLOW.Cost() ? buttonColorEnabled : buttonColorDisabled;
			tower4Button.colors = money >= TowerType.TOWER4.Cost() ? buttonColorEnabled : buttonColorDisabled;
		}

    prevMoney = money;
	}

  public void SetDisplayTower(Tower newTower){
		if (DisplayedTower != null) {
			DisplayedTower.onDeselect ();
    }

		newTower.onSelect ();
    DisplayedTower = newTower;

		RefreshTowerDisplay();

  }

	private void RefreshTowerDisplay() {
		if (DisplayedTower == null) {
			unitNameText.text = null;
			fireRateText.text = null;
			damageText.text = null;
			rangeText.text = null;
			selectedIcon.gameObject.SetActive(false);
			promoteHoverText.text = null;
			sellHoverText.text = null;
		} else {
			selectedIcon.gameObject.SetActive(true);
			switch(DisplayedTower.GetTowerType()) {
				case TowerType.BASIC:
					selectedIcon.sprite = gun;
					break;
				case TowerType.DYNAMITE:
					selectedIcon.sprite = tnt;
					break;
				case TowerType.SLOW:
					selectedIcon.sprite = barrel;
					break;
				case TowerType.TOWER4:
					selectedIcon.sprite = badge;
					break;
			}

			unitNameText.text = "Name:" + DisplayedTower.towerName;
			fireRateText.text = "Fire Rate: " + DisplayedTower.fireRate;
			damageText.text = "Damage: " + DisplayedTower.damage;
			rangeText.text = "Range: " + DisplayedTower.range;

			string costText = $"Cost: ${DisplayedTower.promoteCost} \n";

			float rangeDelta = DisplayedTower.GetTowerType().PromoteRangeChange();

			string rangeChangeText =
				rangeDelta == 0 ? null : $"Range { rangeDelta > 0 ? "+" : null}{rangeDelta}\n";

			float fireRateDelta = DisplayedTower.GetTowerType().PromoteFirerateChange();
			string fireRateChangeText =
				fireRateDelta == 0 ? null : $"Fire Rate { fireRateDelta > 0 ? "+" : null}{fireRateDelta}/s\n";

			promoteHoverText.text = costText + rangeChangeText + fireRateChangeText;

			sellHoverText.text = $"Sell tower for ${DisplayedTower.value}";

		}
	}

	public void SetNextWaveButtonActive(bool active) {
		nextWaveButton.gameObject.SetActive(active);
	}

	public void SetTutorialStage(int stage) {
		hireSomePeeps.SetActive(stage == 1);
		selectTowerHelp.SetActive(stage == 2);
		helpButton.SetActive(stage == -1);
	}
}
