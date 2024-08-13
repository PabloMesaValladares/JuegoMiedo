using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataController : MonoBehaviour
{
    public GameObject dayManager;
    public string archivoDeGuardado;
    private GameData gameData = new GameData();
    private  void Awake() {
        archivoDeGuardado = Application.dataPath + "/gameData.json";
        dayManager = GameObject.Find("DayManager");
        LoadData();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.O)) {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadData();
        }
    }

    private void LoadData() {
        if (File.Exists(archivoDeGuardado)) {
            string json = File.ReadAllText(archivoDeGuardado);
            gameData = JsonUtility.FromJson<GameData>(json);
            dayManager.GetComponent<DayManager>().day = gameData.day;
            Debug.Log("Cargado");
        } else {
            Debug.LogError("No se encontro el archivo de guardado");
        }
    }

    private void SaveData() {
        GameData newData = new GameData()
        {
            day = dayManager.GetComponent<DayManager>().day
        };
        string cadenaJSON = JsonUtility.ToJson(newData);
        File.WriteAllText(archivoDeGuardado, cadenaJSON);

        Debug.Log("Guardado");
    }
}
