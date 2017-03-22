using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowdown_Tower : Tower {

    // Use this for initialization
  public float slowFactor;

	// Update is called once per frame
	void Update () {
        Aim();
        foreach(GameObject e in enemies)
        {
            if(e.GetComponent<Car>().slowFactor > this.slowFactor)
            {
                e.GetComponent<Car>().slowFactor = this.slowFactor;
            }
        }
    }

    public override TowerType GetTowerType()
    {
        return TowerType.SLOW;
    }

    public override void RangeEntered(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
            if (other.GetComponent<Car>().slowFactor > this.slowFactor) other.gameObject.GetComponent<Car>().slowFactor = this.slowFactor;
        }

    }

    public override void RangeExited(Collider other)
    {
        // Destroy everything that leaves the trigger
        other.gameObject.GetComponent<Car>().slowFactor = 1;
        enemies.Remove(other.gameObject);
    }
}
