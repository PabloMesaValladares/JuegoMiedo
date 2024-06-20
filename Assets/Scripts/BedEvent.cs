using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedEvent : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private BedEvent _thisBed;

    [SerializeField] private int maxProbability;
    [SerializeField] private int triggerNumber;
    [SerializeField] private bool isInside;
    [SerializeField] private float timerOn;
    [SerializeField] private float delay;

    [SerializeField] private bool GreedGo;
    [SerializeField] public GameObject assignedDoor;

    [SerializeField] private Greed _Greed;

    // Start is called before the first frame update
    void Awake()
    {
        GreedGo = false;
        isInside = false;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(isInside && GreedGo == false) 
        {
            timerOn -= 1 * Time.deltaTime;

            if (timerOn <= 0)
            {
                timerOn = delay;
                TriggeringTheEvent();
            }
        }
    }

    public void OnTriggerEnter(Collider _player)
    {
        isInside = true;
        timerOn = delay;
    }

    public void OnTriggerExit(Collider _player)
    {
            isInside = false;
    }

    public void TriggeringTheEvent()
    {
        if (isInside == true)
        {
            triggerNumber = Random.Range(0, maxProbability);

            if(triggerNumber == 1)
            {
                _Greed.AssingTheDoor(gameObject);
                _Greed.SetSpawnGreed(this.gameObject.transform.position);
                GreedGo = true;
                //AnimationDoor();
            }
        }
    }

    public void AnimationDoor()
    {
        assignedDoor.GetComponent<DoorsScript>().AnimationBlockDoor();
    }

}
