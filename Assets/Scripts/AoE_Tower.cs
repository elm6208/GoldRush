using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoE_Tower : Tower {

    public Dynamite dynamitePrefab;
	
	// Update is called once per frame
	void Update () {
        Enemy target = Aim();

        if (target != null && fireCooldown <= 0)
        {
            Aoe_Attack(target);
        }
        else {
            fireCooldown -= Time.deltaTime;
        }
    }

    void Aoe_Attack(Enemy target)
    {
        print("Lobbing bomb");
        fireCooldown = fireRate;
        Dynamite atkr = (Dynamite)Instantiate(dynamitePrefab, transform.position, transform.rotation);
        atkr.SetTarget(target);
    }
}
