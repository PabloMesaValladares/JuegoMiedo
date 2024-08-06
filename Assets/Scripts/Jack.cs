using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack : MonoBehaviour
{
    [SerializeField] private GameObject _GameplayManager;

    [SerializeField] private float timerDeath;
    [SerializeField] private float timerDeathSaved;
    private Coroutine increaseTimerCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        timerDeathSaved = _GameplayManager.GetComponent<GameplayManager>().jackCooldown;
        timerDeath = timerDeathSaved;

        if (increaseTimerCoroutine != null)
        {
            StopCoroutine(increaseTimerCoroutine);
            increaseTimerCoroutine = null;
        }
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
            increaseTimerCoroutine = StartCoroutine(IncreaseTimer());
        }
    }

    private IEnumerator IncreaseTimer()
    {
        while (timerDeath < timerDeathSaved)
        {
            timerDeath += 1 * Time.deltaTime;
            yield return null;
        }
        timerDeath = timerDeathSaved;
    }

    public void Death()
    {

    }
}
