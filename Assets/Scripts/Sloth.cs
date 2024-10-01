using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Sloth : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy; //agent
    [SerializeField] private GameObject _Player;
    [SerializeField] private GameplayManager _GameplayManager;

    public LayerMask isGrounded;

    [SerializeField] private float Speed;
    [SerializeField] private float SpeedZero;

    void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        _Player = GameObject.FindGameObjectWithTag("Player");
        enemy.GetComponent<NavMeshAgent>().speed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        Attacking();
        PlayerIsLookingMe();
    }

    private void Attacking()
    {
        enemy.SetDestination(_Player.transform.position);

        transform.LookAt(_Player.transform);

        Vector3 distanceToReseachPlayer = transform.position - _Player.transform.position;

        if (distanceToReseachPlayer.magnitude <= 1.5f)
        {
            Debug.Log("Consegui matar al jugador, hihihi");
            Jumpscare();
        }
    }
    
    public void PlayerIsLookingMe()
    {
        if (_Player.GetComponent<PlayerFieldView>().sensor.Objects.Count >= 1)
        {
            enemy.GetComponent<NavMeshAgent>().speed = SpeedZero;
        }
        else if(_Player.GetComponent<PlayerFieldView>().sensor.Objects.Count <= 0)
        {
            enemy.GetComponent<NavMeshAgent>().speed = Speed;
        }

    }

    private void Jumpscare()
    {
        Debug.Log("Susto");
        gameObject.SetActive(false);
    }
}
