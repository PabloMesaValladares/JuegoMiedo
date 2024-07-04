using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DayManager : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private GameplayManager _GameplayManager;

    [SerializeField] private int day;
    [SerializeField] public float dayTimer = 600;
    [SerializeField] private float dayTimerMulti;
    [SerializeField] private bool enemiesGetOut;

    //Enemies Cooldown
    [SerializeField] private float prideCooldown;
    [SerializeField] private float gluttonyCooldown;

    [SerializeField] private float prideCooldownSaved;

    //Enemies bools
    public bool prideHasSpawnded;

    [SerializeField] private List<DaysInfo> _days;
  
    // Start is called before the first frame update
    void Awake()
    {
        enemiesGetOut = false;

        day = _GameManager.Day;

        prideCooldownSaved = _days[day].prideCooldown;

        prideCooldown = prideCooldownSaved;

        prideHasSpawnded = false;
}


    //Poner toda la info, probar los enemigos.

    // Update is called once per frame
    void Update()
    {
        dayTimer -= dayTimerMulti * Time.deltaTime;
        DaysManagament();
    }
    

    public void DaysManagament()
    {
        if(day >= 0 && day <= 6 && day != 7) //Primeros dias
        {
            SpawningPride();
        }
        else if(day >= 8 && day <= 13 && day != 14) //Segundos dias (se desbloquea el piso inferior)
        {

        }
        else if(day >= 15 && day <= 20 && day != 21) //Ultimos dias (se desbloquea toda la casa)
        {

        }
        else if(day == 7) // 1ra Luna Sangrienta
        {

        }
        else if (day == 14)// 2na Luna Sangrienta
        {

        }
        else if (day == 21)// 3ra Luna Sangrienta
        {

        }
    }

    public void SpawningPride()
    {
        if(!prideHasSpawnded)
        {
            prideCooldown -= 1 * Time.deltaTime;

            if (prideCooldown <= 0)
            {
                _GameplayManager.PrideIsComing();
                prideHasSpawnded = true;
                prideCooldown = prideCooldownSaved;
            }
        }
    }

    public void PrideHasDespawnded()
    {
        prideHasSpawnded = false;
    }
}
