using UnityEngine;
using System.Collections;

//use the Generic system here to make use of a Flocker list later on
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
// vehicle class to contain all methods for steering the vehicles/seekers
abstract public class Vehicle : MonoBehaviour
{

    //get access to Character Controller component
    CharacterController charControl;

    // access to gamemanager script
   // protected GameManager gm;

    //fields necessry for movement
    protected Vector3 acceleration;
    protected Vector3 velocity;
    public Vector3 Velocity
    {
        get { return velocity; }
    }
    protected Vector3 desired;
    protected Vector3 steer;

    //fields 
    public float maxSpeed = 10.0f;
    public float maxForce = 12.0f;
    public float sepWeight = 30f; // weight of separation
    public float aliWeight = 30f; // weight of alignment
    public float cohWeight = 30f; // weight of cohesion
    public float boundsWeight = 120f; // weight for staying in bounds
    public float radius = 1.0f;
    public float mass = 1.0f;
    public float gravity = 20.0f;

    abstract protected void CalcSteeringForces();

    // initialize values
    virtual public void Start()
    {
        acceleration = Vector3.zero;
        velocity = transform.forward;
        charControl = GetComponent<CharacterController>();
        //gm = GameObject.Find("GameManagerGO").GetComponent<GameManager>();
    }


    // Update is called once per frame
    protected void Update()
    {
        CalcSteeringForces();

        //"movement formula"
        velocity += acceleration * Time.deltaTime;
        velocity.y = 0;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.forward = velocity.normalized;
        //if(velocity != Vector3.zero){
        //transform.forward = velocity.normalized;
        //}

        charControl.Move(velocity * Time.deltaTime); // moves the character based on velocity calculations

        acceleration = Vector3.zero; // reset acceleration
    }

    protected void ApplyForce(Vector3 steeringForce)
    { // applies steering force to acceleration
        acceleration += steeringForce / mass;
    }

    protected Vector3 Seek(Vector3 targetPos)
    { // Method to seek a point based on the vector that's passed in
        desired = targetPos - transform.position;
        desired.Normalize();
        desired = desired * maxSpeed;
        steer = desired - velocity;
        steer.y = 0;
        return steer;
    }

    protected Vector3 AvoidObstacle(GameObject ob, float safe) // Method to avoid any obstacles that may be in the way of the vehicle
    {

        //reset desired velocity
        desired = Vector3.zero;
        //get radius from obstacle's script
        float obRad = ob.GetComponent<ObstacleScript>().Radius;
        //get vector from vehicle to obstacle
        Vector3 vecToCenter = ob.transform.position - transform.position;
        //zero-out y component (only necessary when working on X-Z plane)
        vecToCenter.y = 0;
        //if object is out of my safe zone, ignore it
        if (vecToCenter.magnitude > safe)
        {
            return Vector3.zero;
        }
        //if object is behind me, ignore it
        if (Vector3.Dot(vecToCenter, transform.forward) < 0)
        {
            return Vector3.zero;
        }
        //if object is not in my forward path, ignore it
        if (Mathf.Abs(Vector3.Dot(vecToCenter, transform.right)) > obRad + radius)
        {
            return Vector3.zero;
        }

        //if we get this far, we will collide with an obstacle!
        //object on left, steer right
        if (Vector3.Dot(vecToCenter, transform.right) < 0)
        {
            desired = transform.right * maxSpeed;
            //debug line to see if the dude is avoiding to the right
            Debug.DrawLine(transform.position, ob.transform.position, Color.red);
        }
        else
        {
            desired = transform.right * -maxSpeed;
            //debug line to see if the dude is avoiding to the left
            Debug.DrawLine(transform.position, ob.transform.position, Color.green);
        }
        return desired;
    }

    protected Vector3 Separate(float separationDistance, List<GameObject> flk) // method to calculate flock separation based on the desired separation distance passed in 
    {
        Vector3 steer = new Vector3(0, 0, 0);
        List<GameObject> tooClose = new List<GameObject>();
        foreach (GameObject flkr in flk) // get a list of other flockers that are too close to the flocker
        {
            if (flkr != null)
            {
                if (Vector3.Distance(flkr.transform.position, transform.position) < separationDistance && flkr != gameObject) // do not include this flocker itself in the list
                {
                    tooClose.Add(flkr); // add flockers to the list
                }
            }
        }
        Vector3 sum = new Vector3(0, 0, 0);
        foreach (GameObject tC in tooClose) // for each flocker that's too close, flee the flocker until it gets the desired separation distance away
        {
            Vector3 tempFlee = -Seek(tC.transform.position);
            sum += tempFlee * (1 / Vector3.Distance(tC.transform.position, transform.position));
        }
        return sum;

    }
    protected Vector3 Align(Vector3 alignVector) // method to keep the flockers aligned/ facing the same way
    {
        //float neighborDist = 50;
        Vector3 sum = new Vector3(0, 0, 0);
        sum = alignVector * maxSpeed;
        Vector3 steer = sum + velocity;
        Vector3.ClampMagnitude(steer, maxForce);
        return steer;
    }
    protected Vector3 Cohesion(Vector3 cohesionVector) // method to keep cohesion in the flock based on the centroid, or wherever the flock is cohering to
    {
        return Seek(cohesionVector);
    }

    public Vector3 StayInBounds(float radius, Vector3 center) // method to keep the vehicle from going out of bounds if it goes too far
    {
        Vector3 steer = new Vector3(0, 0, 0);
        if (Vector3.Distance(transform.position, center) > radius) // if the vehicle goes out of bounds
        {
            steer = Seek(center); // steer it back towards the center
        }
        return steer;
    }

    

    public Vector3 Arrive(Vector3 targetPos)
    {
        // Method to seek a point based on the vector that's passed in
        desired = targetPos - transform.position;
        float d = desired.magnitude;
        desired.Normalize();
        if (d < 100)
        {
            float m = Map(d, 0, 100, 0, maxSpeed);
            desired = desired * m;
        }
        else { desired = desired * maxSpeed; }
        steer = desired - velocity;
        steer.y = 0;
        Vector3.ClampMagnitude(steer, maxForce);
        return steer;
    }

    public float Map(float variable1, float mn1, float mx1,float mn2, float mx2) // maps values like processing function
    {
        float output;
        output = mn2 + (mx2 - mn2) * ((variable1 - mn1) / (mx1 - mn1));
        return output;
    }

    protected bool InLeadersWay(GameObject leader, Vector3 leaderForward) // determines if the vehicle is in the leader's way so they can get out of it
    {
        if(Vector3.Distance(leaderForward,transform.position) <= 2 || Vector3.Distance(leader.transform.position,transform.position) <= 2){
            return true;
        }
        else { return false; }
    }
}


