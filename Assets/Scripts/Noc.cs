using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class Noc : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy; //agent
    [SerializeField] private FieldViewEnemies sensor;
    [SerializeField] private GameObject _player;

    public LayerMask isGrounded;
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    [SerializeField] private int monsterPhase;
    [SerializeField] private bool playerIsHidden;
    [SerializeField] private float shoutTimer;
    [SerializeField] private float shoutTimerSaved;
    [SerializeField] private float savedSpeed;
    [SerializeField] private float attackSpeed;
    [SerializeField] private bool hasCried;

    public bool playerSpotted;
    public bool asumadre;

    public List<GameObject> Hides = new List<GameObject>();
    public Vector3 hideMoreNear;

    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        sensor = GetComponentInChildren<FieldViewEnemies>();
        _player = GameObject.FindGameObjectWithTag("Player");
        shoutTimerSaved = shoutTimer;

        playerSpotted = false;
        SearchWalkPoint();
        savedSpeed = enemy.speed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckThings();
    }

    public void CheckThings()
    {
        if (monsterPhase == 0) 
        {
            Vector3 distanceToPlayer = transform.position - _player.transform.position;

            if (sensor.IsInSight(_player) == true)
            {
                monsterPhase = 1;
                Debug.Log("Acabo de ver al jugador asique cambio a Fase: 1");
            }
            if (distanceToPlayer.magnitude < 15f && playerIsHidden == true)
            {
                monsterPhase = 2;
                Debug.Log("He llegado al ultimo spot del Jugador y no lo veo, asique cambio a Fase: 2");
            }

            Patroling();
            Debug.Log("Estoy en Fase: 0 y estoy siguendo la ultima posicion del Jugador");
        }

        else if(monsterPhase == 1) 
        {
            if (hasCried == false)
            {
                Shouting();
                Debug.Log("Estoy en Fase: 1 y estoy gritando");
            }
            else if (hasCried == true)
            {
                Attacking();
                Debug.Log("Estoy en Fase: 1 y ya he gritado, voy a por el jugador");
            }
        }

        //Pausado, Neccesito acabar la mecanica del escondite primero.
        else if(monsterPhase == 2) 
        {
            SearchInHides();
        }

        
        if (sensor.Objects.Count == 0 && playerSpotted == false)
        {
            Patroling();
            Debug.Log("me muevo");
        }
        else if(sensor.IsInSight(_player) == true && sensor.Objects.Count > 0)
        {
            if(hasCried==false)
            {
                enemy.speed = 0;
                walkPoint = new Vector3(0,0,0);
                walkPointSet = false;
                playerSpotted = true;
                Shouting();
                Debug.Log("veo al jugador");
            }
            else if(hasCried==true) 
            {
                Attacking();
                enemy.speed = savedSpeed;
                Debug.Log("voy a por el");
            }
        }
        else if(sensor.Objects.Count == 0 && playerSpotted == true && Hides.Count > 0 && asumadre == false)
        {
            enemy.speed = savedSpeed;
            SearchInHides();
            asumadre = true;
            Debug.Log("busca escondites");
        }
        else if(sensor.Objects.Count == 0 && playerSpotted == true && Hides.Count <= 0)
        {
            enemy.speed = savedSpeed;
            Debug.Log("Se fue");
            gameObject.SetActive(false);
        }
    }

     //detectar escondites
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hide")
        {
            Hides.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hide")
        {
            Hides.Remove(other.gameObject);
        }
    }
    
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

        walkPoint = new Vector3(_player.transform.position.x + randomizerX, transform.position.y, _player.transform.position.z + randomizerZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, isGrounded)) walkPointSet = true;  
    }

    private void Shouting()
    {
        enemy.speed = 0;
        walkPointSet = false;
        walkPoint = new Vector3(0, 0, 0);

        shoutTimer -= 1 * Time.deltaTime;

        if (shoutTimer <= 0)
        {
            shoutTimer = shoutTimerSaved;
            enemy.speed = attackSpeed;
            hasCried = true;
        }
    }

    private void SearchInHides()
    {
        enemy.speed = savedSpeed;
        Vector3 nearHide = new Vector3(0, 0, 0);
        hideMoreNear = Hides[0].gameObject.transform.position;

        for (int i = 0; i < Hides.Count; i++) 
        {
            nearHide = Hides[i].gameObject.transform.position;

            if(hideMoreNear.x > gameObject.transform.position.x - nearHide.x
            && hideMoreNear.z > gameObject.transform.position.z - nearHide.z)
            {
                hideMoreNear = nearHide;
                //hideMoreNear = gameObject.transform.position - nearHide;
            }
        }

        enemy.SetDestination(hideMoreNear);

        if (hideMoreNear.magnitude <= 0.5f)
        {
            Debug.Log("Estoy revisando el escondite");
            gameObject.SetActive(false);
            //Siguente Fase
        }
    }

    private void Attacking()
    {
        enemy.SetDestination(_player.transform.position);

        transform.LookAt(_player.transform);

        Vector3 distanceToReseachPlayer = transform.position - _player.transform.position;

        if(distanceToReseachPlayer.magnitude <= 1.5f && playerIsHidden == false) 
        {
            Debug.Log("Consegui matar al jugador, hihihi");
            Jumpscare();
        }
        else if(distanceToReseachPlayer.magnitude <= 1.5f && playerIsHidden == true)
        {
            Debug.Log("El cbr del jugador se escondio, voy a revisar el escondite mas cercano");
            monsterPhase = 2;
        }

    }

    private void SearchForPlayer() 
    {
        if(playerIsHidden == false)
        {

        }
    }

    private void Jumpscare()
    {
        Debug.Log("Susto");
        gameObject.SetActive(false);
    }
}
