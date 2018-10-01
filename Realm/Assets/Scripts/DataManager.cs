using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class DataManager
{

    // To erase persistent data: manually change this to "true", run the app, stop the app, change it back to "false".
    public static bool eraseModeActive = false;

    public static Realm RealmData;

    public static void SaveData()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (eraseModeActive) {
            FileStream realmFile = File.Create(Application.persistentDataPath + "/realmData.gd");
            binaryFormatter.Serialize(realmFile, new Realm());
            realmFile.Close();
        } else {
            FileStream realmFile = File.Create(Application.persistentDataPath + "/realmData.gd");
            binaryFormatter.Serialize(realmFile, RealmManager.realmManager.realm);
            realmFile.Close();
        }
    }

    public static void LoadData()
    {
        if (!eraseModeActive) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            if (File.Exists(Application.persistentDataPath + "/realmData.gd"))
            {
                FileStream realmFile = File.Open(Application.persistentDataPath + "/realmData.gd", FileMode.Open);
                RealmData = (Realm)binaryFormatter.Deserialize(realmFile);
                realmFile.Close();

                RealmManager.realmManager.realm = RealmData;
            }
        }
    }
}