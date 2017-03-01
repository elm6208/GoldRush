using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	// rate of fire in seconds
	public float fireRate = 1.0f;

	// damage each attack deals
	public int damage = 1;

	// radius the tower can attack in
	public float range = 4.0f;

	// tracks time between shots
	protected float fireCooldown = 0.0f;

	protected List<GameObject> enemies;

    public int value;
    public int promoteCost;

    public string towerName;

	// Use this for initialization
	protected void Start () {
		value = GetType().Cost();
        promoteCost = 3;
		enemies = new List<GameObject> ();
	}

	public virtual TowerType GetType() {
		return TowerType.BASIC;
	}

	public void RangeEntered(Collider other) {
		if (other.gameObject.tag == "Enemy") {
			enemies.Add (other.gameObject);
		}
	}
		
	public void RangeExited(Collider other) {
		// Destroy everything that leaves the trigger
		enemies.Remove(other.gameObject);
	}
	
	// Update is called once per frame
	protected void Update () {
		Enemy target = Aim ();

        if (target != null && fireCooldown <= 0) {
			Attack (target);
		} else {
			fireCooldown -= Time.deltaTime;
		}
	}
		
	protected Enemy Aim() {
        
		Enemy target = null;
		for(int i = 0; i < enemies.Count; i++) {
            
			// check if enemy has been killed
			if (enemies[i] == null) {
				enemies.RemoveAt (i);
				i--;
				continue;
			}

            //getting enemy component of gameObjects in enemy list
			Enemy enemy = enemies[i].GetComponent<Enemy>();

			if (target == null || enemy.getDist() > target.getDist()) {
                target = enemy;
			}
		}
		if (target != null) {
			Vector3 t = target.transform.position;
			t.y = transform.position.y;
			this.transform.LookAt (t);
		}
		return target;
	}

	void Attack(Enemy target) {
		fireCooldown = fireRate;
        //attack is not piercing so bool is false
		target.takeDamage (damage, false);
	}

    public void Promote()
    {
        fireRate -= 0.25f;
        range += 1;
        value += promoteCost;
        promoteCost += 3;

    }

    public void Settle()
    {
        Destroy(this.gameObject);
        GameObject.FindWithTag("MainCamera").GetComponent<GameController>().money += value;
    }
}
