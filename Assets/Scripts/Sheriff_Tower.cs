using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheriff_Tower : Tower {

    protected List<GameObject> allies;
    public float rangeBoost;
    public float fireRateBoost;

	// Use this for initialization
	void Start () {
        enemies = new List<GameObject>();
        allies = new List<GameObject>();
		
	}
	
	// Update is called once per frame
	 void  Update () {
        Aim();
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
        if (other.gameObject.tag == "Tower"){
            removeBoost(other.gameObject.GetComponent<Tower>());
        }
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

    public override void Sell()
    {
        for (int i = 0; i < allies.Count; i++)
        {
            if (allies[i] != null)
            {
                removeBoost(allies[i].GetComponent<Tower>());
                i--;
            } 
        }

        Destroy(this.gameObject);
     }

    protected void Boost(Tower tower){
        tower.range += rangeBoost;
        tower.fireRate -= fireRateBoost;
    }

    protected void removeBoost(Tower tower){
        tower.range -= rangeBoost;
        tower.fireRate += fireRateBoost;
        allies.Remove(tower.gameObject);
    }

    public void RangeStay(Collider other){
        /*
        if (other.gameObject.tag == "Tower"){
            allies.Add(other.gameObject);
        }*/
    }
}
