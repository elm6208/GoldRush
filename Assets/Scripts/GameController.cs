using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public int money;
	public int lives;

    private Tower selectedTower;
    public float spawnTimer;
    public int enemiesInWave;
    public int numWaves;
    public GameObject enemy;
    public float spawnX;
    public float spawnY;
    public float spawnZ;
    //how often the player can shoot
    //public float playerShootFrequency;
    //public int clickDamage;

    private float timerCountdown;
    public int enemyCountdown;
    private int waveCountdown;

    //pause between waves
    private bool wavePaused;

    // timer tracking how long it has been since the player shot
    //private float playerShootTimer;

	public GameObject placerPrefab;
    public GameObject UICanvasPrefab;
    private UIManager ui;
	private Placer placer;

	protected List<Enemy> enemies;

	// stuff to toggle for tutorial
	protected GameObject enemyEndHelp;
	protected GameObject hireSomePeeps;
	protected GameObject selectTowerHelp;

	public int CurrentWave
	{
		// not perfect, but we can change it when we add pauses
		// between the waves
		get { return numWaves - waveCountdown + 1; }
	}

	// Use this for initialization
	void Start () {

		enemies = new List<Enemy>();
        timerCountdown = spawnTimer;
        enemyCountdown = enemiesInWave;
        waveCountdown = numWaves;
        wavePaused = false;

       // playerShootTimer = playerShootFrequency;

		placer = Instantiate(placerPrefab, new Vector3(), Quaternion.identity).GetComponent<Placer> ();
        ui = Instantiate(UICanvasPrefab, new Vector3(), Quaternion.identity).GetComponent<UIManager>();

				enemyEndHelp = GameObject.Find("EnemyEndHelp");
				enemyEndHelp.SetActive(false);

    }

		public void onEnemyBreach(Enemy enemy) {
			enemies.Remove(enemy);
			loseLife();
			CheckWaveEnded();
		}

    public void loseLife(){
        lives--;
        if(lives <= 0){
            Application.LoadLevel("Lose");
        }
    }

	// Update is called once per frame
	void Update () {
        checkWin();

		if (Input.GetKeyDown (KeyCode.Escape)) {
			SetPlacer (TowerType.NONE);
		}

        //decrease player shoot timer
        /*
        if(playerShootTimer > 0.0f)
        {
            playerShootTimer -= Time.deltaTime;
        }


        //attacking enemy
        if (Input.GetMouseButtonDown(0) && placer.Placing == TowerType.NONE && playerShootTimer <= 0.0f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {

                //if the ray hits an enemy, attack the enemy
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<Enemy>().takeDamage(clickDamage, false);

                    playerShootTimer = playerShootFrequency;

                }

            }
        }*/

				// check if guide UI buttons are pressed
				if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit)){
								print(hit.collider);
                if(hit.collider.gameObject.tag == "GuideUIButton"){
                    progressTutorial(1);
                }
            }

        }

        if (Input.GetMouseButtonDown(0) && placer.Placing == TowerType.NONE)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit)){
                if(hit.collider.gameObject.tag == "Tower"){
                    GameObject thing = hit.collider.gameObject;
                    ui.SetDisplayTower(thing.GetComponent<Tower>());
                }
            }

        }

        if (!wavePaused){
            //do this until the last wave ends
            if (waveCountdown > 0)
            {
                //change timer
                timerCountdown -= Time.deltaTime;

                //if timer reaches zero
                if (timerCountdown <= 0f)
                {
										print("spawn " + enemyCountdown + " | " + enemiesInWave);
                    //spawn an enemy
                    GameObject newEnemy = Instantiate(enemy, new Vector3(spawnX, spawnY, spawnZ), Quaternion.identity);
										enemies.Add(newEnemy.GetComponent<Enemy>());

                    newEnemy.GetComponent<Enemy>().hp = 10 + 2 * (CurrentWave/2);

                    //decrease enemy countdown
                    enemyCountdown--;

                    //if this is the end of the wave
                    if (enemyCountdown <= 0)
                    {
											//increase enemies in wave by 2
											enemiesInWave += 2;
											//reset enemy countdown if not final wave
											if(waveCountdown > 1){
													enemyCountdown = enemiesInWave;
											}

											//set longer timer for in between waves
											timerCountdown = 5;

											//decrease wave countdown
											waveCountdown--;

											//pause wave
											wavePaused = true;

											CheckWaveEnded();
                    }
                    //if this is not the end of the wave
                    else
                    {
                        //reset timer
                        timerCountdown = spawnTimer;
                    }

                }
            }
        }

	}

	public void SetPlacer(TowerType towerType) {
		placer.Placing = towerType;
	}
	public bool ShouldShowRange(Tower tower) {
		return placer.Placing != TowerType.NONE || ui.GetDisplayTower () == tower;
	}

    public void checkWin(){
        if(waveCountdown <= 0 && enemyCountdown <= 0){
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0){
                Application.LoadLevel("Win");
            }
        }
    }

	private void CheckWaveEnded() {
			if (wavePaused && enemies.Count == 0) {
				onWaveEnd();
			}
	}

	private void onWaveEnd() {
		ui.SetNextWaveButtonActive(true);
	}

	public void onEnemyDeath(Enemy enemy) {
		enemies.Remove(enemy);
		money += enemy.value;
		CheckWaveEnded();
	}

	public void StartWave() {
		wavePaused = false;
		timerCountdown = 0;
		ui.SetNextWaveButtonActive(false);
	}

	/*
	tutorial stages:
	 0: baddies are tryna get to the end thing!
	 1: buy towers!!!
	 2: what the towers do!
	 3: stats help
	 4: click on a tower to select it!!
	*/

	private int tutorialStage = 0;

	public void progressTutorial(int nextStage) {

		// pause game if tutorial is in progress
		Time.timeScale = (nextStage == -1) ? 1 : 0;

		enemyEndHelp.SetActive(nextStage == 0);
		ui.SetTutorialStage(nextStage);

		tutorialStage = nextStage;
	}

}
