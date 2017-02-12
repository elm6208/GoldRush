using UnityEngine;
using System.Collections;
//add using System.Collections.Generic; to use the generic list format
using System.Collections.Generic;

public class Car : Vehicle
{

    //what is this Seeker going after?
    
    //weighting
    public float seekWeight = 75.0f;
    public float safeDistance = 10.0f;
    public float avoidWeight = 100.0f;
    //what is my steering force at the moment?
    Vector3 steeringForce;
    GameObject carTarget;
    List<GameObject> waypoints;
    bool wp1 = false;
    bool wp2 = false;
    bool wp3 = false;
    bool wp4 = false;
    bool wp5 = false;
    bool wp6 = false;
    // Call Inherited Start and then do our own
    override public void Start()
    {
        //call parent's start
        base.Start();
        //initialize the steering force
        steeringForce = Vector3.zero;
        waypoints = new List<GameObject>();
        waypoints.Add(GameObject.FindGameObjectWithTag("Waypoint1"));
        waypoints.Add(GameObject.FindGameObjectWithTag("Waypoint2"));
        waypoints.Add(GameObject.FindGameObjectWithTag("Waypoint3"));
        waypoints.Add(GameObject.FindGameObjectWithTag("Waypoint4"));
        waypoints.Add(GameObject.FindGameObjectWithTag("Waypoint5"));
        waypoints.Add(GameObject.FindGameObjectWithTag("Waypoint6"));
        wp1 = false;
        wp2 = false;
        wp3 = false;
        wp4 = false;
        wp5 = false;
        wp6 = false;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (wp1 == false && Vector3.Distance(transform.position, waypoints[0].transform.position) < 5)
        {
            wp1 = true;
            wp2 = false;
        }
        if (wp2 == false && wp1 == true && Vector3.Distance(transform.position, waypoints[1].transform.position) < 5)
        {
            wp2 = true;
            wp3 = false;
        }
        if (wp3 == false && wp2 == true && Vector3.Distance(transform.position, waypoints[2].transform.position) < 5)
        {
            wp3 = true;
            wp4 = false;
        }
        if (wp4 == false && wp3 == true && Vector3.Distance(transform.position, waypoints[3].transform.position) < 5)
        {
            wp4 = true;
            wp5 = false;
        }
        if (wp5 == false && wp4 == true && Vector3.Distance(transform.position, waypoints[4].transform.position) < 5)
        {
            wp5 = true;
            wp6 = false;
        }


    }

    // calculate all of the steering forces on the seeker
    protected override void CalcSteeringForces()
    {
        
        if (wp1 == false)
        {
            steeringForce += Seek(waypoints[0].transform.position);
        }
        if (wp1 == true && wp2 == false)
        {
            steeringForce += Seek(waypoints[1].transform.position);
        }
        if (wp2 == true && wp3 == false)
        {
            steeringForce += Seek(waypoints[2].transform.position);
        }
        if (wp3 == true && wp4 == false)
        {
            steeringForce += Seek(waypoints[3].transform.position);
        }
        if (wp4 == true && wp5 == false)
        {
            steeringForce += Seek(waypoints[4].transform.position);
        }
        if (wp5 == true && wp6 == false)
        {
            steeringForce += Seek(waypoints[5].transform.position);
        }
            //limit the 1 steering force (ultimate force) 
            steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);

            //apply them as 1 force (ultimate force) in ApplyForce()
            ApplyForce(steeringForce);
        


    }
    
    protected bool CheckCollisions(GameObject obj)
    {
        float distance = 100;
        if (obj != null)
        {
            distance = Vector3.Distance(obj.transform.position, this.gameObject.transform.position);
        }
        if (obj != null)
        {
            if (Vector3.Distance(obj.transform.position, this.gameObject.transform.position) < 5)
            {
                return true;
            }
        }
        return false;
    }
    
}

