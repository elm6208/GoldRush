using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public int money;
	public int lives;

    public float spawnTimer;
    public int enemiesInWave;
    public int numWaves;
    public GameObject enemy;
    public float spawnX;
    public float spawnY;
    public float spawnZ;

    private float timerCountdown;
    private int enemyCountdown;
    private int waveCountdown;

	public GameObject placerPrefab;
	private Placer placer;

	public int CurrentWave
	{
		// not perfect, but we can change it when we add pauses
		// between the waves
		get { return numWaves - waveCountdown + 1; }
	}

	// Use this for initialization
	void Start () {
        timerCountdown = spawnTimer;
        enemyCountdown = enemiesInWave;
        waveCountdown = numWaves;

		placer = Instantiate(placerPrefab, new Vector3(), Quaternion.identity).GetComponent<Placer> ();
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

        //do this until the last wave ends
        if(waveCountdown > 0)
        {
            //change timer
            timerCountdown -= Time.deltaTime;

            //if timer reaches zero
            if (timerCountdown <= 0f)
            {
                //spawn an enemy
                Instantiate(enemy, new Vector3(spawnX, spawnY, spawnZ), Quaternion.identity);

                //decrease enemy countdown
                enemyCountdown--;

                //if this is the end of the wave
                if (enemyCountdown <= 0)
                {
                    //increase enemies in wave by 2
                    enemiesInWave += 2;
                    //reset enemy countdown
                    enemyCountdown = enemiesInWave;
                    //set longer timer for in between waves
                    timerCountdown = 5;

                    //decrease wave countdown
                    waveCountdown--;

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
		
	public void SetPlacer(TowerType towerType) {
		placer.Placing = towerType;
	}

    public void checkWin(){
        if(waveCountdown <= 0){
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0){
                Application.LoadLevel("Win");
            }      
        }
    }

}
