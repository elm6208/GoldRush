using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float maxSpeed;
    public int hp;
    Vector3 move;
    float DistanceTravelled;

	public int value = 1;


	// Use this for initialization
	void Start () {
        hp = 10;
        maxSpeed = 0.1f;
        DistanceTravelled = 0;
        //move = new Vector3(maxSpeed, 0, 0);

		
	}
	
	// Update is called once per frame
	protected void Update () {
        //  transform.Translate(move);
        //  DistanceTravelled += move.magnitude;
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    protected void OnTriggerEnter(Collider collision){
        GameObject thing = collision.gameObject;
        if(thing.tag == "End"){
            //getting gamecontroller and reducing life
            GameObject.FindWithTag("MainCamera").GetComponent<GameController>().loseLife();

            Destroy(this.gameObject);



            
        }



        //collision handling
    }

    public virtual void takeDamage(int dmg, bool pierce){
        hp -= dmg;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        if (hp <= 0){
			onDeath ();
        }
    }

	 protected void onDeath(){
		Destroy(this.gameObject);
		GameObject.FindWithTag ("MainCamera").GetComponent<GameController>().money += value;
	}

    public float getDist(){
        return DistanceTravelled;
    }
}
