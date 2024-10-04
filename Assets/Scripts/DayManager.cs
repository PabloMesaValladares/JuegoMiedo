using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class DayManager : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private GameplayManager _GameplayManager;

    [SerializeField] public int day;

    //Enemies bools
    public bool prideCanSpawn = false;
    public bool gluttonyCanSpawn = false;
    public bool greedCanSpawn = false;
    public bool jackCanSpawn = false;
    public bool weaverCanSpawn = false;
    public bool windowCanSpawn = false;

    [SerializeField] public List<DaysInfo> _days;

    // Start is called before the first frame update
    void Awake()
    {
        _GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        day = _GameManager.Day;
        DaysManagament();
    }

    //Poner toda la info, probar los enemigos.

    public void DaysManagament()
    {
        if (day == 0)
        {
            prideCanSpawn = true;
            gluttonyCanSpawn = false;
            greedCanSpawn = false;
            jackCanSpawn = false;
            weaverCanSpawn = false;
            windowCanSpawn = false;
        }
        else if (day == 1)
        {
            prideCanSpawn = true;
            gluttonyCanSpawn = true;
            greedCanSpawn = true;
            jackCanSpawn = true;
            weaverCanSpawn = true;
            windowCanSpawn = true;
        }
    
        /*
        if(day >= 0 && day <= 6 && day != 7) //Primeros dias
        {
            
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
        */
    }
}
