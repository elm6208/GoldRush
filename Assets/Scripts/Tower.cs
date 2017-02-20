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
	private float fireCooldown = 0.0f;

	private SphereCollider rangeCollider;

	private List<GameObject> enemies;

	// Use this for initialization
	void Start () {
		rangeCollider = GetComponent<SphereCollider> ();
        rangeCollider.radius = range;
		enemies = new List<GameObject> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Enemy") {
			enemies.Add (other.gameObject);
		}
	}
		
	void OnTriggerExit(Collider other) {
		// Destroy everything that leaves the trigger
		enemies.Remove(other.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		Enemy target = Aim ();

        if (target != null && fireCooldown <= 0) {
			Attack (target);
		} else {
			fireCooldown -= Time.deltaTime;
		}
	}
		
	Enemy Aim() {
        
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
        print("Attacking");
		fireCooldown = fireRate;
        //attack is not piercing so bool is false
		target.takeDamage (damage, false);
	}
}
