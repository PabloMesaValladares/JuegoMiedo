using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gluttony : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _AwakenGluttony;
    [SerializeField] private GameObject _GameplayManager;
    private InventorySystem _Inventory;

    [SerializeField] private bool umbralCheck, umbralCheck1, umbralCheck2;
    [SerializeField] private float timerOn;
    [SerializeField] private float delay;
    [SerializeField] private int umbral, umbral1, umbral2;

    // Start is called before the first frame update
    void Awake()
    {
        _GameplayManager = GameObject.FindGameObjectWithTag("GameplayManager");
        _player = GameObject.FindGameObjectWithTag("Player");
        _Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();

        delay = _GameplayManager.GetComponent<GameplayManager>().gluttonyCooldown;
        umbral = _GameplayManager.GetComponent<GameplayManager>().gluttonyRoar;
        umbral1 = _GameplayManager.GetComponent<GameplayManager>().gluttonyRoar1;
        umbral2 = _GameplayManager.GetComponent<GameplayManager>().gluttonyRoar2;

        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        timerOn -= 1 * Time.deltaTime;

        if (timerOn <= umbral && umbralCheck != true)
        {
            Debug.Log("Grito numero 1");
            umbralCheck = true;
            //primera adventencia sonora
        }
        else if (timerOn <= umbral1 && umbralCheck1 != true) 
        {
            Debug.Log("Grito numero 2");
            umbralCheck1 = true;
            //segunda adventencia sonora
        }
        else if (timerOn <= umbral2 && umbralCheck2 != true)
        {
            Debug.Log("Grito numero 3");
            umbralCheck2 = true;
            //tercera adventencia sonora
        }
        else if (timerOn <= 0)
        {
            Debug.Log("CAGASTE!!!");
            Reset();
            TriggeringTheEvent();
        }
    }

    public void Reset()
    {
        timerOn = delay;
        umbralCheck = false;
        umbralCheck1 = false;
        umbralCheck2 = false;
    }

    public void FeedMe()
    {
        if(_Inventory.CheckObjects(0) == true)
        {
            _Inventory.RemoveObjects(0);
            Reset();
            Debug.Log("Me alimentaste");
        }

        //No tienes carne
    }

    public void TriggeringTheEvent()
    {
        gameObject.SetActive(false);
        _AwakenGluttony.SetActive(true);
    }
}
