using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class GameSaveManager
{


    public static void SavePlayer(PlayerScript player) {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/safe.fun";

        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Game Saved at " + path);
        
    }

    public static PlayerData loadPlayer() {
        string path = Application.persistentDataPath + "/safe.fun";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            Debug.Log("Game Loaded");
            return data;
        }
        else {
            Debug.LogError("Safe file not found");
            return null;
        }
    }

}
