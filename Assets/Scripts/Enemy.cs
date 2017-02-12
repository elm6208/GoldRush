using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public GameObject currTarget;
    public float maxSpeed;
    public int hp;
    Vector3 move;
    float DistanceTravelled;


	// Use this for initialization
	void Start () {
        hp = 10;
        maxSpeed = 0.1f;
        DistanceTravelled = 0;
        //move = new Vector3(maxSpeed, 0, 0);

		
	}
	
	// Update is called once per frame
	void Update () {
      //  transform.Translate(move);
      //  DistanceTravelled += move.magnitude;
		
	}

    void onTriggerEnter(Collider collision){
        GameObject thing = collision.gameObject;

        if(thing.tag == "target"){
            //updateTarget(thing.nextTarg);
        }



        //collision handling
    }

    public void takeDamage(int dmg){
        hp -= dmg;

        if(hp <= 0){
            Destroy(this.gameObject);
        }
    }

    public void moveToTarget(){

    }

    

    public float getDist(){
        return DistanceTravelled;
    }
}
