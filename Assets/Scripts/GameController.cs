using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float spawnTimer;
    public int enemiesInWave;
    public int numWaves;
    public GameObject enemy;
    public float spawnX;
    public float spawnY;
    public float spawnZ;
    public GameObject tower;

    private float timerCountdown;
    private int enemyCountdown;
    private int waveCountdown;

	// Use this for initialization
	void Start () {
        timerCountdown = spawnTimer;
        enemyCountdown = enemiesInWave;
        waveCountdown = numWaves;
	}
	
	// Update is called once per frame
	void Update () {

        //place tower
        if (Input.GetMouseButtonDown(0))
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

}
