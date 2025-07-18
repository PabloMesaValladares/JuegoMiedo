using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lantern : MonoBehaviour
{
    [SerializeField] GameObject _Lantern;
    [SerializeField] AudioSource turnOn;
    [SerializeField] AudioSource turnOff;

    [SerializeField] bool on;

    [SerializeField] private float lanternTimer;
    [SerializeField] private float lanternTimerVelocity;

    [SerializeField] private GameplayManager _GameplayManager;
    [SerializeField] private GameObject _sliderVisibility;
    [SerializeField] private Slider sliderCharge;

    // Start is called before the first frame update
    void Start()
    {
        on = false;
        _Lantern.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(on == false && Input.GetKeyDown(KeyCode.F) && lanternTimer > 1)
        {
            _Lantern.SetActive(true);
            //turnOn.Play();
            on = true;
            _GameplayManager.LanternIsActivatted(true);
        }
        else if(on == true && Input.GetKeyDown(KeyCode.F) && lanternTimer > 1)
        {
            _Lantern.SetActive(false);
            //turnOff.Play();
            on = false;
            _sliderVisibility.SetActive(false);
            _GameplayManager.LanternIsActivatted(false);
        }

        if(on == true && lanternTimer > 1)
        {
            lanternTimer -= lanternTimerVelocity * Time.deltaTime;
            changeSliderAmount();
            _sliderVisibility.SetActive(true);
        }
        if(on == true && lanternTimer <= 1)
        {
            lanternTimer = 0;
            _Lantern.SetActive(false);
            //turnOff.Play();
            on = false;
            _sliderVisibility.SetActive(false);
            _GameplayManager.LanternIsActivatted(false);
        }
    }
    public void changeSliderAmount()
    {
        sliderCharge.value = lanternTimer;
    }

    public void GetLanternTimer(float charge)
    {
        //lanternTimer += lanternTimer + charge;
        lanternTimer += charge;
    }

    public float SeeLanternTimer()
    {
        return lanternTimer;
    }
}
