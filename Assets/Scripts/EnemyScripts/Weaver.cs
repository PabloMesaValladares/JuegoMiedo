using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class Weaver : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy; //agent
    [SerializeField] private GameObject _player;
    [SerializeField] private GameplayManager _GameplayManager;
    [SerializeField] private GameObject _targetAggro;

    [SerializeField] private List<GameObject> _Points;

    [SerializeField] private float Speed;
    [SerializeField] private bool IsStealthing;
    [SerializeField] private bool IsJumping;
    [SerializeField] private bool IsLooking;

    [SerializeField] public float StealthTimer;
    [SerializeField] public float StealthTimerSaved;
    [SerializeField] private int triggerNumber;
    [SerializeField] private bool NumberSelected;


    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        enemy.GetComponent<NavMeshAgent>().speed = Speed;
        IsStealthing = true;
        IsLooking = false;
        IsJumping = false;

        StealthTimerSaved = _GameplayManager.twinWeaversCooldown;
        StealthTimer = StealthTimerSaved;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsLooking == false)
        {
            if (IsStealthing == false && IsJumping == false)
            {
                Patrol();
            }
            else if (IsStealthing == true && IsJumping == false)
            {
                Stealth();
            }
        }
        else if(IsLooking == false) 
        {
            AggroMoment();
        }
    }

    private void Patrol()
    {
        if(NumberSelected == false)
        {
            triggerNumber = Random.Range(0, _Points.Count);
            NumberSelected = true;
        }

        enemy.SetDestination(_Points[triggerNumber].transform.position);

        Vector3 distanceToReseachThatPoint = transform.position - _Points[triggerNumber].transform.position;

        if (distanceToReseachThatPoint.magnitude <= 1f)
        {
            NumberSelected = false;
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
            NumberSelected = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsLooking = false;
            IsStealthing = false;
            IsJumping = true;

            Jumpscare();
        }
    }

    private void Jumpscare()
    {
        enemy.GetComponent<NavMeshAgent>().speed = 0;
        Debug.Log("Susto");
        _GameplayManager.GameOver();
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        enemy.GetComponent<NavMeshAgent>().speed = Speed;
        IsStealthing = false;
        IsJumping = false;

        StealthTimer = StealthTimerSaved;
    }

     public void AgggroTarget(GameObject Target)
     {
        _targetAggro = Target;
        IsLooking = true;
     }

    private void AggroMoment()
    {
        enemy.SetDestination(_targetAggro.transform.position);

        Vector3 distanceToReseachDestination = transform.position - _targetAggro.transform.position;

        if (distanceToReseachDestination.magnitude <= 1.5f)
        {
            IsLooking = false;
            Debug.Log("No encontre a nadie asique vuelvo a patrullar.");
        }
    }
}
