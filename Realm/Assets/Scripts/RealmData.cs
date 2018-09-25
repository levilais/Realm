using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class RealmData
{
    // To erase persistent data: manually change this to "true", run the app, stop the app, change it back to "false".
    public static bool eraseModeActive = false;

    public static List<RObject> RObjects; // setup to check if they exist and if they do update RealmManager.realmManager

    public static void SaveWaypos()
    {
        if (eraseModeActive) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/waypos.gd");
            bf.Serialize(file, new List<RObject>());
            file.Close();
        } else {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/waypos.gd");
            bf.Serialize(file, RealmManager.realmManager.waypos);
            file.Close();
        }
    }

    public static void LoadWaypos()
    {
        if (!eraseModeActive) {
            if (File.Exists(Application.persistentDataPath + "/waypos.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/waypos.gd", FileMode.Open);
                RObjects = (List<RObject>)bf.Deserialize(file);
                file.Close();

                RealmManager.realmManager.waypos = RObjects;
            }
        }
    }
}