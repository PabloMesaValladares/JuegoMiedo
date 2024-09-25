using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gluttony : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _AwakenGluttony;
    [SerializeField] private GameObject _GameplayManager;
    [SerializeField] private GameObject _Player;
    //private InventorySystem _Inventory;

    [SerializeField] private bool umbralCheck, umbralCheck1, umbralCheck2;
    [SerializeField] private float timerOn;
    [SerializeField] private float delay;
    [SerializeField] private int umbral, umbral1, umbral2;
    [SerializeField] private bool stage0, stage1, stage2, stage3;
    [SerializeField] private int triggerNumber;
    [SerializeField] private List<GameObject> _GluttonySpawnPoints;

    // Start is called before the first frame update
    void Awake()
    {
        _GameplayManager = GameObject.FindGameObjectWithTag("GameplayManager");
        _player = GameObject.FindGameObjectWithTag("Player");
        //_Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();

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

    public void SpawnInRandomPlace()
    {
        if (stage0 == true)
        {
            triggerNumber = Random.Range(0,2); //0,1,2 (Segundo Piso)
        }
        if (stage1 == true)
        {
            triggerNumber = Random.Range(0,5); //0,1,2,3,4,5 (Segundo y Primer Piso)
        }
        if (stage2 == true)
        {
            triggerNumber = Random.Range(0,6); //0,1,2,3,4,5,6 (Segundo y Primer Piso y Atico)
        }
        if (stage3 == true)
        {
            triggerNumber = Random.Range(0,8); //0,1,2,3,4,5,6,7,8 (Segundo y Primer Piso, Atico y Sotano)
        }

        gameObject.transform.position = _GluttonySpawnPoints[triggerNumber].transform.position;

    }
    
    public void FeedMe()
    {
        if(_GameplayManager.GetComponent<GameplayManager>().gluttonyDessertPicked == true)
        {
            Reset();
            _GameplayManager.GetComponent<GameplayManager>().gluttonyHasSpawnded = false;
            gameObject.SetActive(false);
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
