using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Day", menuName = "NewDay")]
public class DaysInfo : ScriptableObject
{
    //cosas globales
    public int day;
    public float timePeacefully;

    //pride
    public float prideCooldown;

    //gluttony
    public float GluttonyCooldown;
    public float GluttonyDelay;
    public int GluttonyUmbral, GluttonyUmbral1, GluttonyUmbral2;
    public int DessertSpawnInt;
    public int GluttonyStage;

    //greed
    public bool greedCanSpawn;
    public int greedProbability;

    //spider
    public float spiderCooldown;

    //theatreEnemy
    public float theatreTimer;

    //windowsEnemy
    public float windowsCooldown;
    public int windowsProbability;
}
