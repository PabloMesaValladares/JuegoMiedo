using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class InventorySystem : MonoBehaviour
{
    public bool[] isFull;
    public int[] slots;
    public GameObject[] slots_UI;

    //Objects
    // ID 0 - Carne
    // ID 1 - Llave segunda planta

    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = -1;
            isFull[i] = false;
        }
    }
    public void AddObjects(int ID)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] != true)
            {
                slots[i] = ID;
                isFull[i] = true;
                break;
            }
        }
        //Inventario lleno
    }

    public void RemoveObjects(int ID) 
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == ID && isFull[i] == true)
            {
                slots[i] = -1;
                isFull[i] = false;
                break;
            }
        }
    }

    public bool CheckObjects(int ID)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == ID && isFull[i] == true)
            {
                return true;
            }
        }

        return false;
    }

}
