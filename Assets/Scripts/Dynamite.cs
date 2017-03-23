using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : Vehicle {

    private Enemy target;
    private float range;
    private Vector3 steeringForce;
    protected SphereCollider explosionCollider;
    protected List<GameObject> enemies;
    protected override void CalcSteeringForces()
    {
        if (target != null)
        {
            steeringForce += Seek(target.transform.position);
            //limit the 1 steering force (ultimate force) 
            steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);

            //apply them as 1 force (ultimate force) in ApplyForce()
            ApplyForce(steeringForce);
        }
        if(target == null)
        {
            Explode();
        }
    }

    // Use this for initialization
    void Start () {
        base.Start();
        
        range = 5.0f;
        explosionCollider = GetComponent<SphereCollider>();
        explosionCollider.radius = range;
        enemies = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
        
        CalcSteeringForces();
        if(target != null && Vector3.Distance(gameObject.transform.position,target.transform.position)<=1.5f)
        {
            Explode();
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
        if(target!=null && other.gameObject == target.gameObject)
        {
            Explode();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Destroy everything that leaves the trigger
        enemies.Remove(other.gameObject);
    }
    public void SetTarget(Enemy trgt)
    {
        target = trgt;
    }


    void Explode()
    {
        //print("Exploding");
        explosionCollider.radius = range * 2;
         foreach(GameObject e in enemies)
        {
            if (e != null)
            {
                Enemy enemyScript = e.GetComponent<Enemy>();
                enemyScript.takeDamage(3,true);
            }
        }
        Destroy(gameObject);
    }
    
}
