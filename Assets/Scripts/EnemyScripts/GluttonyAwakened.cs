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
    [SerializeField] private RaycastHit _RaycastPlayer;
    [SerializeField] private float raycastRange;

    [SerializeField] private Animator _anim;
    [SerializeField] private bool Shout;

    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
        enemy.GetComponent<NavMeshAgent>().speed = Speed;

        Shout = false;

        TriggeringTheEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (Shout == true)  //Despues de acabar la animacion del State 1, pondra el Bool Shout en True y ira a por el jugador.
        {
            Attacking();
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

    private void Jumpscare()
    {
        Debug.Log("Susto");
        gameObject.SetActive(false);
    }

    public void TriggeringTheEvent()
    {
        //llamara a un nuevo efecto global que apagara las luces de la casa entera, impedira esconderse en escondites y cargar la bateria.
        _anim.SetInteger("State", 1);
    }

    public void Reset()
    {
        //Como solo spawnea 1 unica vez, y si o si acabara matando al jugador, no neccesita un Reset.
    }
}
