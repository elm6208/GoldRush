﻿using System.Collections;
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

    private float timerCountdown;
    private int enemyCountdown;
    private int waveCountdown;

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
	}

    public void loseLife(){
        lives--;
        if(lives <= 0){

        }
    }
	
	// Update is called once per frame
	void Update () {

        //place tower
        if (Input.GetMouseButtonDown(0) && money >= 5)
        {
            //take away moneys
            //money -= 5;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(ray, out hit))
            {
                //if the ray hits the terrain, place the tower
                if (hit.collider.gameObject.name == "Terrain")
                {
                    //instantiate tower at ray x and z
                    Instantiate(tower, new Vector3(hit.point.x, 3.0f, hit.point.z), Quaternion.identity);
                    money -= 5;
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

}
