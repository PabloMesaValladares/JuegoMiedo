using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheatreEnemy : MonoBehaviour
{
    [SerializeField] private float timerDeath;
    [SerializeField] private float timerDeathSaved;
    private Coroutine increaseTimerCoroutine;

    // Start is called before the first frame update
    void Start()
    {
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

            // Stop the coroutine if the player is detected again
            if (increaseTimerCoroutine != null)
            {
                StopCoroutine(increaseTimerCoroutine);
                increaseTimerCoroutine = null;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Start the coroutine to increase the timer
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
        Debug.Log("Theatre Enemy has killed the player");
    }
}
