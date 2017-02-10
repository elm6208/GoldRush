using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	// rate of fire in seconds
	float fireRate = 1.0f;

	// damage each attack deals
	int damage = 1;

	// radius the tower can attack in
	float range = 4.0f;

	// tracks time between shots
	private float fireCooldown = 0.0f;

	private SphereCollider rangeCollider;

	private List<GameObject> enemies;

	// Use this for initialization
	void Start () {
		rangeCollider = GetComponent<SphereCollider> ();
		enemies = new List<GameObject> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			enemies.Add (other);
		}
	}

	// TODO: check if destroying enemies causes them to trigger this
	// if not, maybe remove dead enemies from list in AttemptAttack()?
	void OnTriggerExit(Collider other) {
		// Destroy everything that leaves the trigger
		enemies.Remove(other);
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

	// Checks if there is at least one enemy to attack and then attacks
	Enemy Aim() {
		Enemy target = null;
		foreach (Enemy enemy in enemies) {
			if (enemy.isAlive && (target == null || enemy.distanceTraveled > target.distanceTraveled)) {
				target = enemy;
			}
		}
		if (target != null) {
			this.transform.LookAt (target.transform);
		}
		return target;
	}

	void Attack(Enemy target) {
		fireCooldown = fireRate;
		enemy.takeDamage (damage);
	}
}
