using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour {

	Tower tower;

	void Start() {
		tower = transform.parent.gameObject.GetComponent<Tower>();

		SphereCollider rangeCollider = GetComponent<SphereCollider> ();
		rangeCollider.radius = tower.range;
	}

	void OnTriggerEnter(Collider other) {
		tower.RangeEntered (other);
	}

	void OnTriggerExit(Collider other) {
		tower.RangeExited (other);
	}
}

