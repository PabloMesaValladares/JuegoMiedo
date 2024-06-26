using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenMinigame : MonoBehaviour
{
    [SerializeField] private GameObject _MinigameUI;
    [SerializeField] private Slider sliderA, sliderD;

    [SerializeField] private float timer;
    [SerializeField] private float timerSaved;
    [SerializeField] public bool startMinigame;
    [SerializeField] private bool pulseA, pulseD;
    [SerializeField] private bool thereIsAPulseA, thereIsAPulseD;
    [SerializeField] public float keyAAmount, keyDAmount;
    [SerializeField] private float keyAAmountS, keyDAmountS, randomizerA, randomizerD;
    [SerializeField] private int minRange, maxRange;

    [SerializeField] private GameplayManager _GameplayManager;
    [SerializeField] private GameObject _Pride;
    // Start is called before the first frame update
    void Start()
    {
        timerSaved = timer;
        startMinigame = false;
        thereIsAPulseA = false;
        thereIsAPulseD = false;
        pulseA = false;
        pulseD = false;
        keyAAmountS = keyAAmount;
        keyDAmountS = keyDAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if(startMinigame) 
        {
            checkPulse();
            _MinigameUI.SetActive(true);
            timer -= 1 * Time.deltaTime;
            changeSliderAmountA();
            changeSliderAmountD();

            if (timer <= 0)
            {
                timer = timerSaved;
                startMinigame = false;
                _MinigameUI.SetActive(false);
                //_GameplayManager.GetComponent<GameplayManager>().PrideIsOut();
                _Pride.GetComponent<Pride>().PlayerCompletedTheMiniGame();
            }
        }
    }

    public void MinigameStart()
    {
        startMinigame = true;
    }

    public void changeSliderAmountA()
    {
        sliderA.value = keyAAmount;
    }

    public void changeSliderAmountD()
    {
        sliderD.value = keyDAmount;
    }

    private void checkPulse()
    {
        //El lado derecho tiene un BUG, no funciona.

        if (!thereIsAPulseA)
        {
            thereIsAPulseA = true;
            randomizerA = Random.Range(minRange, maxRange);
        }

        if (!thereIsAPulseD)
        {
            thereIsAPulseD = true;
            randomizerD = Random.Range(minRange, maxRange);
        }

        if(thereIsAPulseA) //Tecla A
        {
            keyAAmount -= randomizerA * 10 * Time.deltaTime;

            if (keyAAmount <= 1.5f || (!pulseA && Input.GetKeyDown(KeyCode.A)))
            {
                keyAAmount = keyAAmountS;
                thereIsAPulseA = false;
                pulseA = false;
                _GameplayManager.GameOver();
                //El jugador pierde y muere
            }
            if(keyAAmount <= 50)
            {
                pulseA = true;
            }

            if (pulseA && Input.GetKeyDown(KeyCode.A))
            {
                pulseA = false;
                keyAAmount = keyAAmountS;
                thereIsAPulseA = false;
            }          
        }

        if (thereIsAPulseD) //Tecla D
        {
            keyDAmount -= randomizerD * 10 * Time.deltaTime;

            if (keyDAmount <= 1.5f || (!pulseD && Input.GetKeyDown(KeyCode.D)))
            {
                keyDAmount = keyDAmountS;
                thereIsAPulseD = false;
                pulseD = false;
                _GameplayManager.GameOver();
                //El jugador pierde y muere
            }
            if (keyDAmount <= 50)
            {
                pulseD = true;
            }

            if (pulseD && Input.GetKeyDown(KeyCode.D))
            {
                pulseD = false;
                keyDAmount = keyDAmountS;
                thereIsAPulseD = false;
            }
        }
    }
}
