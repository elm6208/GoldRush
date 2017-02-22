using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour {

	// The type of tower being placed. Null if no tower is being placed
	private TowerType placing;
	public TowerType Placing {
		get { return placing; }
		set {
			placing = value;
			this.gameObject.SetActive (placing != TowerType.NONE);
		}
	}

	// Use this for initialization
	void Start () {
		Placing = TowerType.NONE;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateLocation ();
	}

	private void UpdateLocation() {
		if (placing == TowerType.NONE) {
			return;
		}
			
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		if(Physics.Raycast(ray, out hit))
		{
			transform.position = new Vector3(hit.point.x, 3.0f, hit.point.z);
		}
	}
}
