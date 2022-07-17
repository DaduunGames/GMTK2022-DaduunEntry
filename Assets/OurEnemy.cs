using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class OurEnemy : MonoBehaviour
{

    // enemyvalues


    [SerializeField] private float fov = 90f;
    [SerializeField] private float StartChasingDist = 4f;
    [SerializeField] private float StopChasingDist = 10f;
    [SerializeField] private FieldOfView fieldofview;
    Vector3 LookDir;
    public float waitTimer = 3f;

    private Animator anim;


    // pathfinding
    AIPath AIPath;
    [SerializeField] private CharacterControllor player;
    public GameObject[] waypoints;
    int wayPointIndex = 0;
    public float nextWaypointDistance = 3f;


    public GameObject DeathSplat;

    private enum State
    {
        Patrol,
        Chasing
    }
    private State state;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        AIPath = GetComponent<AIPath>();

        AIPath.destination = waypoints[wayPointIndex].transform.position;

        fieldofview.SetFoV(fov);
        fieldofview.SetViewDistance(StartChasingDist);
    }

   
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < StartChasingDist)
        {
            state = State.Chasing;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > StopChasingDist)
        {
            state = State.Patrol;
        }


        switch (state)
        {
            default:
            case State.Patrol:
                PatrolMovement();
                break;
            case State.Chasing:
                Chase();
                break;
        }

        fieldofview.SetOrigin(transform.position);
        fieldofview.SetAimDirection(LookDir);

    }

    void PatrolMovement()
    {
        anim.SetInteger("Movement", 1);

        if (waypoints == null)
        {
            return;
        }
        
        if (Vector3.Distance(transform.position, waypoints[wayPointIndex].transform.position) < 0.2f)
        {
            wayPointIndex += 1;
        }

        if (wayPointIndex == waypoints.Length)
        {
            wayPointIndex = 0;
        }

        AIPath.destination = waypoints[wayPointIndex].transform.position;
        
    }

   

    void Chase()
    {
        AIPath.destination = player.gameObject.transform.position;
        anim.SetInteger("Movement", 2);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, StartChasingDist);
    }

    public void Kill()
    {
        Instantiate(DeathSplat, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
