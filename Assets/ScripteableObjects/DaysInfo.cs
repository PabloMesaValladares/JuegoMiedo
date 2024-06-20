using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Day", menuName = "NewDay")]
public class DaysInfo : ScriptableObject
{
    public int day;

    public float prideCooldown;
    public float GluttonyDelay;
    public int GluttonyUmbral, GluttonyUmbral1, GluttonyUmbral2;
    public bool greedCanSpawn;

}
