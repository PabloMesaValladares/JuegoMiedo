using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greed : MonoBehaviour
{
    [SerializeField] private float deathTimer;
    [SerializeField] private float deathTimerSaved;
    [SerializeField] private bool attacking;

    [SerializeField] private GameplayManager _GameplayManager;
    [SerializeField] private Lantern _Lantern;
    [SerializeField] public BedEvent _AssignedBed;

    // Start is called before the first frame update
    void Start()
    {
        deathTimer = deathTimerSaved;
        attacking = false;
        _AssignedBed = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking == true)
        {
            deathTimer -= 1 * Time.deltaTime;

            if (deathTimer <= 0)
            {
                deathTimer = deathTimerSaved;
                Jumpscare();
                attacking = false;
            }
        }
    }

    public void SetSpawnGreed(Vector3 position)
    {
        gameObject.transform.position = position;
        deathTimer = deathTimerSaved;
        attacking = true;
        gameObject.SetActive(true);
        _GameplayManager.PlayerCantHiddenBecauseGreedIsHere(true);
        _AssignedBed.AnimationDoor();
    }

    private void Jumpscare()
    {
        Debug.Log("Susto");
        gameObject.SetActive(false);
    }

    public void GetOutGreed()
    {
        if(_GameplayManager.LanternActivated() == true && _Lantern.SeeLanternTimer() > 25)
        {
            _Lantern.GetLanternTimer(-25);
            attacking = false;
            gameObject.SetActive(false);
            _GameplayManager.PlayerCantHiddenBecauseGreedIsHere(false);
            _AssignedBed.AnimationDoor();
        }
    }

    /*
    public void AssingTheDoor(BedEvent bed)
    {
        _AssignedBed = bed;
    }
    */
    
    public void AssingTheDoor(GameObject bed)
    {
        _AssignedBed = bed.GetComponent<BedEvent>();
        Debug.Log("Se ha assignado");
    }
    
}
