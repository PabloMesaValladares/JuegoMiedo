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
    [SerializeField] private bool startMinigame;
    [SerializeField] private bool pulseA, pulseD;
    [SerializeField] private bool thereIsAPulseA, thereIsAPulseD;
    public float keyAAmount, keyDAmount;
    [SerializeField] private float keyAAmountS, keyDAmountS, randomizerA, randomizerD;
    [SerializeField] private int minRange, maxRange;
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
            randomizerA = UnityEngine.Random.Range(minRange, maxRange);
        }

        if (!thereIsAPulseD)
        {
            thereIsAPulseD = true;
            randomizerD = UnityEngine.Random.Range(minRange, maxRange);
        }

        else if(thereIsAPulseA) //Tecla A
        {
            keyAAmount -= randomizerA * 2 * Time.deltaTime;

            if (keyAAmount <= 0 || (!pulseA && Input.GetKeyDown(KeyCode.Space)))
            {
                keyAAmount = keyAAmountS;
                thereIsAPulseA = false;
                pulseA = false;
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

        else if (thereIsAPulseD) //Tecla D
        {
            keyDAmount -= randomizerD * 2 * Time.deltaTime;

            if (keyDAmount <= 0 || (!pulseD && Input.GetKeyDown(KeyCode.Space)))
            {
                keyDAmount = keyDAmountS;
                thereIsAPulseD = false;
                pulseD = false;
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
