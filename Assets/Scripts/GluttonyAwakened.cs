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
        PlayerIsLookingMe();
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

    public void PlayerIsLookingMe()
    {
        //
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
