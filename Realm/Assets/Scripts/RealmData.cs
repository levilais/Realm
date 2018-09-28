﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class RealmData
{

    // To erase persistent data: manually change this to "true", run the app, stop the app, change it back to "false".
    public static bool eraseModeActive = false;

    public static List<RObject> WaypoObjects; // setup to check if they exist and if they do update RealmManager.realmManager
    public static List<RObject> DisplayObjects;

    public static void SaveData()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (eraseModeActive) {
            FileStream wayposFile = File.Create(Application.persistentDataPath + "/waypos.gd");
            binaryFormatter.Serialize(wayposFile, new List<RObject>());
            wayposFile.Close();

            FileStream displaysFile = File.Create(Application.persistentDataPath + "/displays.gd");
            binaryFormatter.Serialize(displaysFile, new List<RObject>());
            displaysFile.Close();
        } else {
            FileStream wayposFile = File.Create(Application.persistentDataPath + "/waypos.gd");
            binaryFormatter.Serialize(wayposFile, RealmManager.realmManager.waypos);
            wayposFile.Close();

            FileStream displaysFile = File.Create(Application.persistentDataPath + "/displays.gd");
            binaryFormatter.Serialize(displaysFile, RealmManager.realmManager.displays);
            displaysFile.Close();
        }
    }

    public static void LoadRealmData()
    {
        if (!eraseModeActive) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            if (File.Exists(Application.persistentDataPath + "/waypos.gd"))
            {
                FileStream wayposFile = File.Open(Application.persistentDataPath + "/waypos.gd", FileMode.Open);
                WaypoObjects = (List<RObject>)binaryFormatter.Deserialize(wayposFile);
                wayposFile.Close();

                RealmManager.realmManager.waypos = WaypoObjects;
            }

            if (File.Exists(Application.persistentDataPath + "/displays.gd"))
            {
                FileStream displaysFile = File.Open(Application.persistentDataPath + "/displays.gd", FileMode.Open);
                DisplayObjects = (List<RObject>)binaryFormatter.Deserialize(displaysFile);
                displaysFile.Close();

                RealmManager.realmManager.displays = DisplayObjects;
            }
        }
    }
}