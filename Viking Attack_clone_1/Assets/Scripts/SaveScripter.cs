using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveScripter : MonoBehaviour
{
    int intToSave;
    float floatToSave;
    bool boolToSave;
    private String saveFileName = "/playedData.dat";

    void OnGui()
    {
         
        // Sparar int, float, och boolean baserat på vad du skapat, ska ändras till playerposition, status så som HP och vad som finns i inventoryt
        if (GUI.Button(new Rect(0, 0, 125, 50), "Raise integer")) intToSave++;
        if (GUI.Button(new Rect(0, 100, 125, 50), "Raise Float")) floatToSave++;
        if (GUI.Button(new Rect(0, 200, 125, 50), "Change bool")) boolToSave = boolToSave ? boolToSave = false : boolToSave = true;

        GUI.Label(new Rect(375, 0, 125, 50), "Integer value is " + intToSave);
        GUI.Label(new Rect(375, 100, 125, 50), "Float value is " + floatToSave.ToString("F1"));
        GUI.Label(new Rect(375, 200, 125, 50), "Bool value is " + boolToSave);

        if (GUI.Button(new Rect(750, 0, 125, 50), "Save your game")) SaveGame();
        if (GUI.Button(new Rect(750, 100, 125, 50), "Load Your Game ")) LoadGame();
    }

    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + saveFileName);
        SaveData data = new SaveData();
        data.savedInt = intToSave;
        data.savedFloat = floatToSave;
        data.savedBool = boolToSave;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    void LoadGame()
    {
        if(File.Exists(Application.persistentDataPath + saveFileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + saveFileName, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            intToSave = data.savedInt;
            floatToSave = data.savedFloat;
            boolToSave = data.savedBool;
        }
        else
        {
            Debug.LogError("There is no save data!");
        }
    }
    [Serializable]
    class SaveData
    {
        public int savedInt;
        public float savedFloat;
        public bool savedBool;
    }

}

