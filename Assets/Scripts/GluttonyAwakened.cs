using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GluttonyAwakened : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy; //agent
    [SerializeField] private GameObject _player;
    [SerializeField] private GameplayManager _GameplayManager;

    public LayerMask isGrounded;

    [SerializeField] private float Speed;
    [SerializeField] private float SpeedZero;

    [SerializeField] private RaycastHit _RaycastPlayer;
    [SerializeField] private float raycastRange;

    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        enemy.GetComponent<NavMeshAgent>().speed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        Attacking();
        //PlayerIsLookingMe();
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
    /*
    public void PlayerIsLookingMe()
    {
        enemy.GetComponent<NavMeshAgent>().speed = SpeedZero;
    }

    public void PlayerIsDoesntLookingMe()
    {
        enemy.GetComponent<NavMeshAgent>().speed = Speed;
    }
    */
    public void OnTriggerStay(Collider other)
    {
        if(_GameplayManager.GetComponent<GameplayManager>().LanternActivated() == true)
        {
            enemy.GetComponent<NavMeshAgent>().speed = SpeedZero;
        }
        else if(_GameplayManager.GetComponent<GameplayManager>().LanternActivated() == false)
        {
            enemy.GetComponent<NavMeshAgent>().speed = Speed;
        }
    }

    public void OnTriggerExit(Collider other) 
    {
        enemy.GetComponent<NavMeshAgent>().speed = Speed;
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
