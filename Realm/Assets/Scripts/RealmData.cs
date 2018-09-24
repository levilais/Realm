using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public static class SaveLoad
{
    public static string realmName = RealmManager.realmManager.realmName;
    //I want to have 50 lists (or arrays)   

    public static void Save()
    {
        realmName = RealmManager.realmManager.realmName;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/realmName.gd");
        bf.Serialize(file, SaveLoad.realmName);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/realmName.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/realmName.gd", FileMode.Open);
            SaveLoad.realmName = (string)bf.Deserialize(file);
            file.Close();
        }
    }
}