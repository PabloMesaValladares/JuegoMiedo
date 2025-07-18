using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pride : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy; //agent
    [SerializeField] private FieldViewEnemies sensor;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameplayManager _GameplayManager;
    [SerializeField] private Vector3 _targetAggro;

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
    [SerializeField] private bool playerIsInMinigame;

    public Vector3 LastPlayerPosition;

    public List<GameObject> Hides = new List<GameObject>();
    public Vector3 hideMoreNear;
    public int SavedHideNumber;

    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        sensor = GetComponentInChildren<FieldViewEnemies>();
        _player = GameObject.FindGameObjectWithTag("Player");
        shoutTimerSaved = shoutTimer;
        LastPlayerPosition = new Vector3(0, 0, 0);

        playerIsInMinigame = false;
        SearchWalkPoint();
        savedSpeed = enemy.speed;
        SavedHideNumber = -1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckThings();
    }

    public void CheckThings()
    {
        if (monsterPhase == 0) //Fase de Patrullar
        {
            Vector3 distanceToPlayer = transform.position - _player.transform.position;

            //if (sensor.IsInSight(_player) == true)

            if (sensor.Objects.Count >= 1)
            {
                monsterPhase = 1;
                Debug.Log("Acabo de ver al jugador asique cambio a Fase: 1");
            }
            /*
            if (distanceToPlayer.magnitude < 15f && playerIsHidden == true)
            {
                monsterPhase = 2;
                Debug.Log("He llegado al ultimo spot del Jugador y no lo veo, asique cambio a Fase: 2");
            }
            */
            Patroling();
            Debug.Log("Estoy en Fase: 0 y estoy dando vueltas por la casa");
        }

        else if(monsterPhase == 1) //Fase de Perseguir
        {
            if (hasCried == false)
            {
                Shouting();
                Debug.Log("Acabo de cambiar a Fase: 1 y Grito");
            }
            else if (sensor.Objects.Count >= 1 && hasCried == true)
            {
                LastPlayerPosition = _player.transform.position;
                Attacking();
                Debug.Log("Estoy en Fase: 1, veo al jugador y voy a por el jugador");
            }
            else if (sensor.Objects.Count <= 0 && hasCried == true)
            {
                LookingForPlayer();
                Debug.Log("Estoy en Fase: 1, no veo al jugador, asi que miro en su ultima posicion");
            }
        }

        //Pausado, Neccesito acabar la mecanica del escondite primero.
        else if(monsterPhase == 2) //Fase de Buscar en Escondites
        {
            //si no encuentra escondites que haga otra cosa.

            SearchInHides();

            if(sensor.Objects.Count >= 1)
            {
                monsterPhase = 1;
            }
        }

        else if(monsterPhase == 3) //Fase de Despawnear
        {
            _GameplayManager.GetComponent<GameplayManager>().PrideIsLeaving();

            if (sensor.Objects.Count >= 1)
            {
                monsterPhase = 1;
            }
        }

        else if(monsterPhase == 4) //Fase de esperar al jugador escondido
        {
            if(playerIsInMinigame == false) 
            {
                monsterPhase = 3;
            }
        }

        else if(monsterPhase == 5)
        {
            AggroMoment();

            if (sensor.Objects.Count >= 1)
            {
                monsterPhase = 1;
            }        
        }
        /*
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
        */
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

    private void Attacking()
    {
        enemy.SetDestination(_player.transform.position);

        transform.LookAt(_player.transform);

        Vector3 distanceToReseachPlayer = transform.position - _player.transform.position;

        if (distanceToReseachPlayer.magnitude <= 1.5f && playerIsHidden == false)
        {
            Debug.Log("Consegui matar al jugador, hihihi");
            Jumpscare();
        }
    }

    private void LookingForPlayer()
    {
        enemy.SetDestination(LastPlayerPosition);

        transform.LookAt(LastPlayerPosition);

        Vector3 distanceToReseachPlayer = transform.position - LastPlayerPosition;

        if (distanceToReseachPlayer.magnitude <= 1.5f)
        {
            Debug.Log("Perdi al jugador, voy a revisar el escondite mas cercano y cambio a Fase: 2");
            monsterPhase = 2;
        }
    }

    private void SearchInHides()
    {
        enemy.speed = savedSpeed;

        if(SavedHideNumber == -1)
        {
            Vector3 nearHide = new Vector3(0, 0, 0);
            hideMoreNear = Hides[0].gameObject.transform.position;

            for (int i = 0; i < Hides.Count; i++)
            {
                nearHide = Hides[i].gameObject.transform.position;

                if (hideMoreNear.x > gameObject.transform.position.x - nearHide.x
                && hideMoreNear.z > gameObject.transform.position.z - nearHide.z || i == 0)
                {
                    hideMoreNear = nearHide;
                    SavedHideNumber = i;
                    Debug.Log("He selecionado el escondite mas cercano");
                    //hideMoreNear = gameObject.transform.position - nearHide;
                }
            }
        }

        enemy.SetDestination(hideMoreNear);

        Vector3 distanceToReseachHiden = transform.position - hideMoreNear;

        if (distanceToReseachHiden.magnitude <= 1.5f)
        {
            Debug.Log("Estoy revisando el escondite");
            if (Hides[SavedHideNumber].GetComponentInParent<Closet>().playerIsInside() == true)
            {
                _GameplayManager.StartHiddenMinigame();
                playerIsInMinigame = true;
                monsterPhase = 4;
            }
            else
            {
                monsterPhase = 3;
            }
        }
    }

    public void OutOfHere(Vector3 position)
    {
        enemy.SetDestination(position);

        //transform.LookAt(position);

        Vector3 distanceToReseachDestination = transform.position - position;

        if (distanceToReseachDestination.magnitude <= 1.5f)
        {
            Debug.Log("Me fui del mapa");
            _GameplayManager.GetComponent<GameplayManager>().PrideIsOut();
            gameObject.SetActive(false);
        }
    }

public void AgggroTarget()
    {
        LastPlayerPosition = _player.transform.position;
        _targetAggro = LastPlayerPosition;
        monsterPhase = 5;
    }

    private void AggroMoment()
    {       
        enemy.SetDestination(_targetAggro);

        Vector3 distanceToReseachDestination = transform.position - _targetAggro;

        if (distanceToReseachDestination.magnitude <= 1.5f)
        {
            Debug.Log("No encontre a nadie asique vuelvo a patrullar.");
            monsterPhase = 0;
        }
    }

    private void Jumpscare()
    {
        Debug.Log("Susto");
        gameObject.SetActive(false);
    }

    public void PlayerCompletedTheMiniGame()
    {
        playerIsInMinigame = false;
    }

    public void Reset()
    {
        monsterPhase = 0;
        playerIsHidden = false;
        walkPointSet = false;
        hasCried = false;
        playerIsInMinigame = false;
    }
}
