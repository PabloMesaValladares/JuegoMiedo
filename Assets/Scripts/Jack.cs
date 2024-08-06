using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack : MonoBehaviour
{
    [SerializeField] private GameObject _GameplayManager;

    private float timerDeath;
    private float timerDeathSaved;

    // Start is called before the first frame update
    void Start()
    {
        timerDeathSaved = _GameplayManager.GetComponent<GameplayManager>().jackCooldown;
        timerDeath = timerDeathSaved;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            timerDeath -= 1 * Time.deltaTime;

            if (timerDeath <= 0)
            {
                timerDeath = timerDeathSaved;
                Death();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //PROCESO DE SEGUNDO PLANO PARA RECUPERAR LOS SEGUNDOS
        }
    }

    public void Death()
    {

    }
}
