using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour {

	private GameController gameController;
	private Material material;
	public GameObject basicTower;
	public GameObject dynamiteTower;
	private bool currentlyValid = false;

	// The type of tower being placed. Null if no tower is being placed
	private TowerType placing;
	public TowerType Placing {
		get { return placing; }
		set {
			placing = value;
			this.gameObject.SetActive (placing != TowerType.NONE);
		}
	}

	private GameObject towerPrefab() {
		switch (placing) {
		default:
		case TowerType.BASIC:
			return basicTower;
		case TowerType.DYNAMITE:
			return dynamiteTower;
		}
	}

	// Use this for initialization
	void Start () {
		Placing = TowerType.NONE;
		gameController = GameObject.FindWithTag ("MainCamera").GetComponent<GameController>();
		material = gameObject.GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateLocation ();
		if (Input.GetMouseButtonDown(0) && placing != TowerType.NONE && currentlyValid)
		{
			
			Instantiate(towerPrefab(), transform.position, Quaternion.identity);
			gameController.money -= placing.Cost();
			Placing = TowerType.NONE;

		}
	}

	private void UpdateLocation() {
		if (placing == TowerType.NONE) {
			return;
		}
			
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit groundHit = new RaycastHit ();
		if(Physics.Raycast(ray, out groundHit, LayerData.GROUND_LAYER))
		{
			transform.position = new Vector3(groundHit.point.x, 0.0f, groundHit.point.z);
		}

		bool wasValid = currentlyValid;
		GameObject prefab = towerPrefab ();
		currentlyValid = !Physics.CheckSphere (transform.position, prefab.transform.localScale.x * 0.5f, LayerData.TOWER_PLACEMENT_MASK);

		if (currentlyValid != wasValid) {
			material.color = currentlyValid ? Color.white : Color.red;
		}
	}
}
