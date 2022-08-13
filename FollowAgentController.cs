using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowAgentController : MonoBehaviour
{

    NavMeshAgent agent; //
    [Header("Stages")]
    public bool Patrol = true; //
    public bool Chase = false; //
    public bool Attack = false; //
    public float radius;

    [Header("Direção")]
    public Transform target; //Algo do agente = Player
    public float playerDist;
    [SerializeField] Transform dir;
    float stoppingDistance = 1f;

    [Header("WayPoints")]
    public float dist;
    [SerializeField] Transform wayPointZero;
    [SerializeField] Transform wayPointUm;
    [SerializeField] Transform wayPointDois;
    [SerializeField] Transform wayPointTres;
    [SerializeField] Transform wayPointQuatro;
    [SerializeField] Transform wayPointCinco;

    [Header("Attack")]
    public float timeAttack;
    public Transform fireSpotPositionWorld;
    public GameObject bulletPrefab;
    public bool bulletEnabled = false;
    public float forceIntensity = 30f;
    public float timeNextBullet = 1.5f;
    

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        dir = wayPointZero;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWaypoint();
        VerifierPlayer();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AttackPlayer();
        }
        if (bulletEnabled)
        {
            timeNextBullet -= Time.deltaTime;
        }
        if (timeNextBullet <= 0)
        {
            Vector3 fireSpotPosition = new Vector3(fireSpotPositionWorld.position.x, fireSpotPositionWorld.position.y, fireSpotPositionWorld.position.z);
            GameObject bullet = Instantiate(bulletPrefab, fireSpotPosition, Quaternion.identity);
            Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();
            rbBullet.AddForce(fireSpotPositionWorld.forward * 30f, ForceMode.Impulse);

            timeNextBullet = 1.5f;
            bulletEnabled = false;
        }
    }
    private void FixedUpdate()
    {
        // if(Chase && !Patrol && !Attack)
        // {
        //     agent.SetDestination(target.position);
        // }
    }
    void ChangeWaypoint()
    {
        dist = Vector3.Distance(dir.position, transform.position);
        if (dist <= 0.9f)
        {
            if (dir == wayPointZero)
            {
                dir = wayPointUm;
            }
            else if (dir == wayPointUm)
            {
                dir = wayPointDois;
            }
            else if (dir == wayPointDois)
            {
                dir = wayPointTres;
            }
            else if (dir == wayPointTres)
            {
                dir = wayPointQuatro;
            }
            else if (dir == wayPointQuatro)
            {
                dir = wayPointCinco;
            }
            else if (dir == wayPointCinco)
            {
                dir = wayPointZero;
            }
        }

    }
    void VerifierPlayer()
    {
        playerDist = Vector3.Distance(target.position, transform.position);
        if (playerDist > 6f)
        {
            Chase = false;
            Patrol = true;
            Attack = false;
            agent.SetDestination(dir.position);
        }
        else if (playerDist > 3 && playerDist < 6)
        {
            Chase = true;
            Patrol = false;
            Attack = false;
            agent.isStopped = false;
            agent.SetDestination(target.position);

        }
        else if (playerDist < 3)
        {
            Chase = false;
            Patrol = false;
            Attack = true;
            agent.isStopped = true;
            agent.stoppingDistance = 3f;
            // timeAttack = 1.5f;
            AttackPlayer();
            print("Atacando...");
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius + 1.5f);
    }

    void AttackPlayer()
    {
        bulletEnabled = true;
    }
}
