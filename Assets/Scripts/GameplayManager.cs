using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //Enemies
    [SerializeField] private GameObject Pride;
    [SerializeField] private GameObject Gluttony;
    [SerializeField] private GameObject Greed;
    [SerializeField] private GameObject Jack;
    [SerializeField] private GameObject TwinWeaver, TwinWeaver1;
    [SerializeField] private GameObject Windows;

    //Enemies Can Spawn
    [SerializeField] private bool PridePlay = false;
    [SerializeField] private bool GluttonyPlay = false;
    [SerializeField] private bool GreedPlay = false;
    [SerializeField] private bool TwinWeaversPlay = false;
    [SerializeField] private bool JackPlay = false;
    [SerializeField] private bool WindowsPlay = false;

    //Enemies Cooldowns
    [SerializeField] private float prideCooldown;
    [SerializeField] private float prideCooldownSaved;
    [SerializeField] private float gluttonyCooldown;
    [SerializeField] private int gluttonyRoar, gluttonyRoar1, gluttonyRoar2;
    [SerializeField] public float jackCooldown;
    [SerializeField] public float twinWeaversCooldown;

    //Enemies Spawns
    [SerializeField] private List<GameObject> _prideSpawns;

    //Enemies Other Things
    [SerializeField] private bool enemiesGetOut;
    [SerializeField] private int prideRandomNumber;
    [SerializeField] private int prideRandomNumberMax;
    [SerializeField] public bool prideHasSpawnded;
    [SerializeField] private bool prideIsActive;
    [SerializeField] public int WindowsProbability;
    [SerializeField] public float WindowsCooldown;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 1;
        playerIsHidden = false;
        playerCantHidden = false;
        lanternActivated = false;
        prideRandomNumber = 0;

        //Enemies Global Things
        enemiesGetOut = false;
        CheckEnemiesThatCanSpawn();
        GluttonyShoutsSet();

        //Pride
        prideCooldownSaved = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].prideCooldown;
        prideCooldown = prideCooldownSaved;
        prideHasSpawnded = false;
        prideRandomNumber = 0;

        //Gluttony
        SpawningGluttony();

        //Greed
        //SpawningGreed();

        //TwinWeavers
        SpawningWeavers();

        //Jack
        SpawningJack();

        //Windows
        SpawningWindows();


    }

    // Update is called once per frame
    void Update()
    {
        SpawningPride();
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
        if (!prideHasSpawnded)
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
        gluttonyCooldown = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].GluttonyDelay;
        gluttonyRoar = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].GluttonyUmbral;
        gluttonyRoar1 = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].GluttonyUmbral1;
        gluttonyRoar2 = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].GluttonyUmbral2;
    }

    public void SpawningGluttony()
    {
        if(GluttonyPlay == true)
        {
            Gluttony.SetActive(true);
        }
    }

    //Greed
    public void SpawningGreed()
    {
        if (GreedPlay == true)
        {
            Greed.SetActive(true);
        }
    }

    //Jack
    public void SpawningJack()
    {
        if (JackPlay == true)
        {
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
            WindowsCooldown = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].windowsCooldown;
            WindowsProbability = _DayManager.GetComponent<DayManager>()._days[_DayManager.GetComponent<DayManager>().day].windowsProbability;
            Windows.SetActive(true);
        }
    }
}
