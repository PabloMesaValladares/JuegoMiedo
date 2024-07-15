using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class Spider : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy; //agent
    [SerializeField] private GameObject _player;
    [SerializeField] private GameplayManager _GameplayManager;
    [SerializeField] private FieldViewEnemies sensor;

    public LayerMask isGrounded;
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    [SerializeField] private float Speed;
    [SerializeField] private bool IsStealthing;
    [SerializeField] private bool IsAttacking;
    [SerializeField] private bool IsPatroling;

    [SerializeField] public float StealthTimer;
    [SerializeField] public float StealthTimerSaved;


    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        sensor = GetComponentInChildren<FieldViewEnemies>();
        enemy.GetComponent<NavMeshAgent>().speed = Speed;
        IsStealthing = false;
        IsAttacking = false;

        StealthTimer = StealthTimerSaved;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsAttacking == false && IsStealthing == false)
        {
            Patrol();
        }
        else if(IsAttacking == false && IsStealthing == true)
        {
            Stealth();
        }
        else if(IsAttacking == true && IsStealthing == false)
        {
            Attacking();
        }

        if (sensor.Objects.Count >= 1)
        {
            IsAttacking = true;
            IsStealthing = false;
        }
    }

    private void Attacking()
    {
        enemy.SetDestination(_player.transform.position);

        transform.LookAt(_player.transform);

        Vector3 distanceToReseachPlayer = transform.position - _player.transform.position;

        if (distanceToReseachPlayer.magnitude <= 1.5f)
        {
            Debug.Log("Consegui matar al jugador, hihihi");
            Jumpscare();
        }
    }
    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) enemy.SetDestination(walkPoint);

        Vector3 distanceToWalkToThatPoint = transform.position - walkPoint;

        if (distanceToWalkToThatPoint.magnitude < 1f && sensor.Objects.Count >= 0)
        {
            walkPointSet = false;
            IsStealthing = true;
        }
    }

    private void Stealth()
    {
        StealthTimer -= 1 * Time.deltaTime;
        
        if(StealthTimer <= 0)
        {
            StealthTimer = StealthTimerSaved;
            IsStealthing = false;
        }

        Vector3 distanceToReseachPlayer = transform.position - _player.transform.position;

        if (distanceToReseachPlayer.magnitude <= 1.5f)
        {
            Debug.Log("Consegui matar al jugador, hihihi");
            Jumpscare();
        }
    }

    private void SearchWalkPoint()
    {
        float randomizerX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomizerZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(_player.transform.position.x + randomizerX, transform.position.y, _player.transform.position.z + randomizerZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGrounded)) walkPointSet = true;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && IsStealthing == false)
        {
            Attacking();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Stealth();
        }
    }

    private void Jumpscare()
    {
        Debug.Log("Susto");
        gameObject.SetActive(false);
    }

    public void Reset()
    {

    }
}
