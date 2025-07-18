using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject _Player;
    [SerializeField] private GameObject _PlayerCamera;
    [SerializeField] private bool playerIsHidden;
    [SerializeField] private bool playerCantHidden;
    [SerializeField] private bool lanternActivated;
    [SerializeField] private GameObject _GameOverUI;
    [SerializeField] private GameObject _OthersUI;
    [SerializeField] private GameObject _DayManager;

    public UnityEvent startMinigame;

    [SerializeField] public float NightGameTimer;
    [SerializeField] public float NightGameTimerSaved;
    [SerializeField] public float InitialTimePeace;
    [SerializeField] public GameObject StartNightObject;
    [SerializeField] private bool GameHasStarted = false;

    //Enemies
    [SerializeField] private GameObject Pride;
    [SerializeField] private GameObject Gluttony;
    [SerializeField] private GameObject Greed;
    [SerializeField] private GameObject Jack;
    [SerializeField] private GameObject TwinWeaver, TwinWeaver1;
    [SerializeField] private GameObject Windows;

    //Enemies Can Spawn
    [SerializeField] public bool PridePlay = false;
    [SerializeField] public bool GluttonyPlay = false;
    [SerializeField] public bool GreedPlay = false;
    [SerializeField] public bool TwinWeaversPlay = false;
    [SerializeField] public bool JackPlay = false;
    [SerializeField] public bool WindowsPlay = false;

    //Enemies Cooldowns
    [SerializeField] public float prideCooldown;
    [SerializeField] public float prideCooldownSaved;
    [SerializeField] public float gluttonyCooldown;
    [SerializeField] public float gluttonyCooldownSaved;
    [SerializeField] public float gluttonyDelay;
    [SerializeField] public int gluttonyRoar, gluttonyRoar1, gluttonyRoar2;
    [SerializeField] public float jackCooldown;
    [SerializeField] public float twinWeaversCooldown;
    [SerializeField] public float windowsCooldown;

    //Enemies Spawns
    [SerializeField] private List<GameObject> _prideSpawns;

    //Enemies Other Things
    [SerializeField] private int prideRandomNumber;
    [SerializeField] private int prideRandomNumberMax;
    [SerializeField] public bool prideHasSpawnded;
    [SerializeField] private bool prideIsActive;
    [SerializeField] public bool gluttonyHasSpawnded;
    [SerializeField] public bool gluttonyDessertPicked;
    [SerializeField] public int gluttonyStage;
    [SerializeField] private GameObject Dessert;
    [SerializeField] private int DessertSpawnNumber;
    [SerializeField] public int windowsProbability;
    [SerializeField] public int greedProbability;

    // Start is called before the first frame update
    
    void Awake()
    {
        /*
        Time.timeScale = 1;
        playerIsHidden = false;
        playerCantHidden = false;
        lanternActivated = false;
        NightGameTimer = NightGameTimerSaved;

        //Enemies Global Things
        CheckEnemiesThatCanSpawn();

        //Pride
        prideCooldownSaved = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].prideCooldown;
        prideCooldown = prideCooldownSaved;
        prideHasSpawnded = false;

        //Gluttony
        gluttonyCooldownSaved = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].GluttonyCooldown;
        gluttonyCooldown = gluttonyCooldownSaved;
        gluttonyHasSpawnded = false;
        GluttonyShoutsSet();
        SpawningGluttony();

        //Greed
        SpawningGreed();

        //TwinWeavers
        SpawningWeavers();

        //Jack
        SpawningJack();

        //Windows
        SpawningWindows();

        GameHasStarted = true;
        */

        StartGame();

    }
    
    // Update is called once per frame
    void Update()
    {
        if(GameHasStarted == true)
        {
            SpawningPride();
            SpawningGluttony();

            NightGameTimer -= 1 * Time.deltaTime;

            if (NightGameTimer <= 0)
            {
                NightGameTimer = NightGameTimerSaved;
                StartCoroutine(StartNightObject.GetComponent<StartNight>().NightIsEnding());
            }
        }
    }

    public void PlayerHidden()
    {
        if(playerIsHidden == false && playerCantHidden == false)
        {
            playerIsHidden = true;
            _Player.SetActive(false);
            _PlayerCamera.SetActive(false);
        }
        else if(playerIsHidden == true) 
        {
            playerIsHidden = false;
            _Player.SetActive(true);
            _PlayerCamera.SetActive(true);
        }
    }

    public void LanternIsActivatted(bool l)
    {
        lanternActivated = l;
    }

    public bool LanternActivated()
    {
        return lanternActivated;
    }

    public void PlayerCantHiddenBecauseGreedIsHere(bool c)
    {
        playerCantHidden = c;
    }

    public bool PlayerCantHiddenConfirmation()
    {
        return playerCantHidden;
    }

    public void StartHiddenMinigame()
    {
        startMinigame.Invoke();
    }

    public void GameOver()
    {
        _Player.SetActive(false);
        _GameOverUI.SetActive(true);
        _OthersUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;

        //Time.timeScale = 0;
        //demas cositas
    }

    //Enemies Functions

    public void CheckEnemiesThatCanSpawn()
    {
        if (_DayManager.GetComponent<DayManager>().prideCanSpawn == true) PridePlay = true;
        if (_DayManager.GetComponent<DayManager>().gluttonyCanSpawn == true) GluttonyPlay = true;
        if (_DayManager.GetComponent<DayManager>().greedCanSpawn == true) GreedPlay = true;
        if (_DayManager.GetComponent<DayManager>().jackCanSpawn == true) JackPlay = true;
        if (_DayManager.GetComponent<DayManager>().weaverCanSpawn == true) TwinWeaversPlay = true;
        if (_DayManager.GetComponent<DayManager>().windowCanSpawn == true) WindowsPlay = true;
    }

    //Pride
    public void SpawningPride()
    {
        if (!prideHasSpawnded && PridePlay == true)
        {
            prideCooldown -= 1 * Time.deltaTime;

            if (prideCooldown <= 0)
            {
                PrideIsComing();
                prideHasSpawnded = true;
                prideCooldown = prideCooldownSaved;
            }
        }
    }

    public void PrideIsComing()
    {
        int i = 0;
        prideRandomNumber = UnityEngine.Random.Range(0, prideRandomNumberMax);

        Pride.GetComponent<Pride>().Reset();
        Pride.gameObject.transform.position = _prideSpawns[i].gameObject.transform.position;
        Pride.SetActive(true);
        prideIsActive = true;
    }

    public void PrideIsLeaving()
    {
        int i = 0;
        prideRandomNumber = UnityEngine.Random.Range(0, prideRandomNumberMax);

        //Pride.gameObject.transform.position = _prideSpawns[i].gameObject.transform.position;
        Pride.GetComponent<Pride>().OutOfHere(_prideSpawns[i].gameObject.transform.position);
        prideIsActive = false;
    }

    public void PrideIsOut()
    {
        prideHasSpawnded = false;
    }

    public bool PrideIsActive()
    {
        return prideIsActive;
    }

    //Gluttony
    public void GluttonyShoutsSet()
    {
        gluttonyCooldown = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].GluttonyCooldown;
        gluttonyDelay = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].GluttonyDelay;
        gluttonyRoar = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].GluttonyUmbral;
        gluttonyRoar1 = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].GluttonyUmbral1;
        gluttonyRoar2 = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].GluttonyUmbral2;
        DessertSpawnNumber = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].DessertSpawnInt;
        gluttonyStage = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].GluttonyStage;
    }

    public void SpawningGluttony()
    {
        if(!gluttonyHasSpawnded && GluttonyPlay == true)
        {
            gluttonyCooldown -= 1 * Time.deltaTime;

            if(gluttonyCooldown <= 0)
            {
                Gluttony.GetComponent<Gluttony>().SpawnInRandomPlace();
                Gluttony.SetActive(true);
                Dessert.GetComponent<DessertScript>().SpawnTheDessert(DessertSpawnNumber);
                gluttonyDessertPicked = false;
                gluttonyHasSpawnded = true;
                gluttonyCooldown = gluttonyCooldownSaved;
            }
        }
    }

    //Greed
    public void SpawningGreed()
    {
        if (GreedPlay == true)
        {
            greedProbability = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].greedProbability;
        }
    }

    //Jack
    public void SpawningJack()
    {
        if (JackPlay == true)
        {
            SetJackCooldowns();
            Jack.SetActive(true);
        }
    }

    public void SetJackCooldowns()
    {
        jackCooldown = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].theatreTimer;
    }

    //Weavers
    public void SpawningWeavers()
    {
        if (TwinWeaversPlay == true)
        {
            SetWeaversCooldowns();
            TwinWeaver.SetActive(true);
            TwinWeaver1.SetActive(true);
        }
    }
    public void SetWeaversCooldowns()
    {
        twinWeaversCooldown = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].spiderCooldown;
    }

    //Windows
    public void SpawningWindows()
    {
        if (WindowsPlay == true)
        {
            windowsCooldown = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].windowsCooldown;
            windowsProbability = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].windowsProbability;
            Windows.SetActive(true);
        }
    }

    //GLOBAL RESET

    public void StartGame()
    {
        
        Time.timeScale = 1;
        playerIsHidden = false;
        playerCantHidden = false;
        lanternActivated = false;
        NightGameTimer = NightGameTimerSaved;

        //Enemies Global Things
        CheckEnemiesThatCanSpawn();

        //Pride
        prideCooldownSaved = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].prideCooldown;
        prideCooldown = prideCooldownSaved;
        prideHasSpawnded = false;

        //Gluttony
        gluttonyCooldownSaved = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].GluttonyCooldown;
        gluttonyCooldown = gluttonyCooldownSaved;
        gluttonyHasSpawnded = false;
        GluttonyShoutsSet();
        SpawningGluttony();

        //Greed
        SpawningGreed();

        //TwinWeavers
        SpawningWeavers();

        //Jack
        SpawningJack();

        //Windows
        SpawningWindows();

        GameHasStarted = true;    
    }

    public void FinishGame()
    {
        GameHasStarted = false;

        //Pride
        Pride.GetComponent<Pride>().Reset();
        Pride.SetActive(false);

        //Gluttony
        Gluttony.GetComponent<Gluttony>().Reset();
        Gluttony.SetActive(false);

        //Greed
        //Greed.GetComponent<Greed>().Reset();
        Greed.SetActive(false);

        //Knights
        //Knights.GetComponent<Knights>().Reset();
        //Knights.SetActive(false);

        //Windows
        //Windows.GetComponent<Window>().Reset();
        Windows.SetActive(false);

        //TwinWeavers
        TwinWeaver.GetComponent<Weaver>().Reset();
        TwinWeaver.SetActive(false);
        TwinWeaver1.GetComponent<Weaver>().Reset();
        TwinWeaver1.SetActive(false);

        //Sloth
        //De Momento nada

    }
}
