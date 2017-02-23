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
    public GameObject tower;
    //how often the player can shoot
    public float playerShootFrequency;
    public int clickDamage;

    private float timerCountdown;
    private int enemyCountdown;
    private int waveCountdown;

    // timer tracking how long it has been since the player shot
    private float playerShootTimer;

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

        playerShootTimer = playerShootFrequency;

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

        //decrease player shoot timer
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
        }

        //place tower
        if (Input.GetMouseButtonDown(0) && placer.Placing != TowerType.NONE)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(ray, out hit))
            {
                //if the ray hits the terrain, place the tower
                if (hit.collider.gameObject.name == "Terrain")
                {
                    //instantiate tower at ray x and z
                    Instantiate(tower, new Vector3(hit.point.x, 3.0f, hit.point.z), Quaternion.identity);

					money -= placer.Placing.Cost();
					SetPlacer (TowerType.NONE);

                }
            }
            //Instantiate(tower, new Vector3(mousePosInWorld.x, 3, mousePosInWorld.z), Quaternion.identity);
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
