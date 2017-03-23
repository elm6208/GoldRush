using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorEnemy : Enemy {
    public int armor;
    float distanceTravelled;

	// Use this for initialization
	void Start () {
        hp = 10;
        armor = 5;
        maxSpeed = 0.05f;
        distanceTravelled = 0;
		
	}

    protected override void Update()
    {
        damageTimer -= 1;
        if (damageTimer <= 0)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    public override void takeDamage(int dmg, bool pierce){
        //checking if armor is gone, then taking dmg like normal if it is
        if (armor <= 0)
        {
            hp -= dmg;
            damageTimer = 5;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            if (hp <= 0)
            {
                onDeath();
            }
        }
        //taking armor dmg if atk is piercing
        else if (armor > 0 && pierce == true){
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            armor -= dmg;
        }
        //takes no dmg is atk isn't piercing
    }
}
