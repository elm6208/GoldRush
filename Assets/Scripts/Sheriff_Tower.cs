using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheriff_Tower : Tower {

    protected List<GameObject> allies;
    public float rangeBoost;
    public float fireRateBoost;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SheriffAim();
		
	}

    public override void RangeEntered(Collider other){
        if(other.gameObject.tag == "Tower"){
            print("RangeEntered");
            allies.Add(other.gameObject);
            Boost(other.gameObject.GetComponent<Tower>());
        }
    }

    public override void RangeExited(Collider other)
    {
        removeBoost(other.gameObject.GetComponent<Tower>());
        allies.Remove(other.gameObject);
    }

    //used instead of generic Tower aim
    protected void SheriffAim()
    {
        for (int i = 0; i < allies.Count; i++){
            if (allies[i] == null){
                allies.RemoveAt(i);
                i--;
                continue;
            }
        }
    }

    protected void Boost(Tower tower){
        tower.range += rangeBoost;
        tower.fireRate += fireRateBoost;
    }

    protected void removeBoost(Tower tower){
        tower.range -= rangeBoost;
        tower.fireRate -= fireRateBoost;
    }
}
