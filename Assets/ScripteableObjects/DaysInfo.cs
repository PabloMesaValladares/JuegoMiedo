using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Day", menuName = "NewDay")]
public class DaysInfo : ScriptableObject
{
    //cosas globales
    public int day;

    //pride
    public float prideCooldown;

    //gluttony
    public float GluttonyDelay;
    public int GluttonyUmbral, GluttonyUmbral1, GluttonyUmbral2;

    //greed
    public bool greedCanSpawn;

    //spider
    public float spiderCooldown;

    //theatreEnemy
    public float theatreTimer;

    //windowsEnemy
    public float windowsCooldown;
    public float windowsProbability;
}
