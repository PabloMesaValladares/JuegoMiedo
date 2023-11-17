using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent enemy; //agent
    public Transform player;
    public FieldViewEnemies sensor;
    public GameObject _player;

    public LayerMask isGrounded, isPlayer;

    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    public float shoutTimer;
    public bool hasCried;
    public bool playerSpotted;
    [SerializeField] private float savedSpeed;
    public List<GameObject> Hides = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        playerSpotted = false;
        SearchWalkPoint();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
        savedSpeed = enemy.speed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckThings();
    }

    public void CheckThings()
    {
        if (sensor.Objects.Count == 0 && playerSpotted == false)
        {
            Patroling();
        }
        else if(sensor.IsInSight(_player) == true)
        {
            if(hasCried==false)
            {
                enemy.speed = 0;
                walkPoint = new Vector3(0,0,0);
                walkPointSet = false;
                playerSpotted = true;
                Shouting();
            }
            else if(hasCried==true) 
            {
                Attacking();
                enemy.speed = savedSpeed;
            }
        }
        else if(sensor.Objects.Count == 0 && playerSpotted == true)
        {
            Patroling();
        }
    }

    /* //detectar escondites
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hide")
        {
            Hides.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            Hides.Remove(Hides.count - 1)
        }
    }
    */
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) enemy.SetDestination(walkPoint);

        Vector3 distanceToWalkToThatPoint = transform.position - walkPoint;

        if (distanceToWalkToThatPoint.magnitude < 1f) walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomizerX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomizerZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(player.transform.position.x + randomizerX, transform.position.y, player.transform.position.z + randomizerZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, isGrounded)) walkPointSet = true;  
    }

    private void Shouting()
    {
        shoutTimer -= 1 * Time.deltaTime;
        playerSpotted = true;

        if (shoutTimer <= 0)
        {
            shoutTimer = 0;
            hasCried = true;
        }
    }

    private void Attacking()
    {
        enemy.SetDestination(player.position);

        transform.LookAt(player);

        Vector3 distanceToReseachPlayer = transform.position - player.position;

        if(distanceToReseachPlayer.magnitude < 2.5f) 
        {
            Jumpscare();
        }
    }

    private void SearchForPlayer() 
    {
        enemy.SetDestination(player.position);

        Vector3 distanceToReseachPlayer = transform.position - player.position;

        if (distanceToReseachPlayer.magnitude < 1.5f)
        {
            
        }
    }

    private void Jumpscare()
    {
        Debug.Log("Susto");
        gameObject.SetActive(false);
    }
}
