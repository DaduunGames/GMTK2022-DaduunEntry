using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class OurEnemy : MonoBehaviour
{

    // enemyvalues

    public Transform target;
    float movementSpeed = 200f;

    [SerializeField] private float fov = 90f;
    [SerializeField] private float viewDistance = 50f;
    [SerializeField] private FieldOfView fieldofview;
    Vector3 LookDir;
    public float waitTimer = 3f;
    
    // 2dmovement
    Path path;
    Seeker seeker;

    // pathfinding
    [SerializeField] private CharacterControllor player;
    int currentWaypoint = 0;
    bool reachEndOfPath = false;
    public GameObject[] waypoints;
    int wayPointIndex = 0;
    public float nextWaypointDistance = 3f;

    Rigidbody2D rb;
    private enum State
    {
        Idle,
        Walk,
        Chasing,
    }
    private State state;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        transform.position = waypoints[wayPointIndex].transform.position;
        //InvokeRepeating("UpdatePath", 0f, .5f);
        target = waypoints[wayPointIndex].transform;
        seeker.StartPath(rb.position, target.position, OnPathComplete);
        

        fieldofview.SetFoV(fov);
        fieldofview.SetViewDistance(viewDistance);
    }
    //void UpdatePath()
    //{
    //    if (seeker.IsDone())
    //    {
    //        seeker.StartPath(rb.position, target.position, OnPathComplete);
    //    }
    //}
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }

        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (state)
        {
            default:
            case State.Walk:
                HandleMovement();
                break;
        }
        fieldofview.SetOrigin(transform.position);
        fieldofview.SetAimDirection(LookDir);

        if(path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachEndOfPath = true;
            return;
        }
        else
        {
            reachEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * movementSpeed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[wayPointIndex]);
       
        for (int i = 0; i < wayPointIndex; i++)
        {
            waypoints.GetValue(wayPointIndex);
        }

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void HandleMovement()
    {
        if(waypoints == null)
        {
            return;
        }

        // if player is in the viewpoint
        if (Vector3.Distance(transform.position, player.transform.position) < viewDistance)
        {
            Chase();
            return;
        }

        //if(transform.position == waypoints[wayPointIndex].transform.position)
        if (Vector3.Distance(transform.position, waypoints[wayPointIndex].transform.position) < 0.2f)
        {
            wayPointIndex += 1;
        }

        if (wayPointIndex == waypoints.Length)
        {
            wayPointIndex = 0;
        }

        target = waypoints[wayPointIndex].transform;
    }

    void Idle()
    {
        
        if (waitTimer <= 3)
        {
            // add animation idle
        }

        else if (waitTimer <= 0)
        {
            waitTimer = 3;
            return;
        }
        
    }

    void Chase()
    {
        target = player.gameObject.transform;
    }
}
