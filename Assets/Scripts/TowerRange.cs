using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour {

	Tower tower;

	protected GameController gameController;
	protected Renderer r;

	void Start() {
		tower = transform.parent.gameObject.GetComponent<Tower>();

		SphereCollider rangeCollider = GetComponent<SphereCollider> ();
		//rangeCollider.radius = tower.range;
		transform.localScale = new Vector3(tower.range, 1.0f, tower.range);
		gameController = GameObject.FindWithTag ("MainCamera").GetComponent<GameController> ();
		r = GetComponent<MeshRenderer> ();
	}

	protected void Update () {
		r.enabled = gameController.GetPlacer () != TowerType.NONE;
	}

	void OnTriggerEnter(Collider other) {
		tower.RangeEntered (other);
	}

	void OnTriggerExit(Collider other) {
		tower.RangeExited (other);
	}
}

