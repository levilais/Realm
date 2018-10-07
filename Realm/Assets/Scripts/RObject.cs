using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RObject {
    public string name;
    public string imageName;
    public System.Guid uid;
    public string displayID;
    public double displayNumber;
    public RPoint rPoint;
    //public string currentImageAddress;

    public bool hasBeenPlaced;
    public bool imageExists;

    public enum RObjectType {Default, Waypo, Display, Anchor}
    public RObjectType rObjectType;

    public RObject() {
        name = "Default";
        imageName = "Default";
        uid = System.Guid.NewGuid();
        displayID = "Default";
        hasBeenPlaced = false;
        imageExists = false;
        rPoint = new RPoint();
        rObjectType = RObjectType.Default;
        //currentImageAddress = "DefaultImage";
    }

    public void SaveActiveObject() {
        if (RealmManager.realmManager.activeObject.rObjectType == RObjectType.Display) {
            int i = 0;
            foreach (RObject display in RealmManager.realmManager.realm.displays)
            {
                if (display.uid == RealmManager.realmManager.activeObject.uid)
                {
                    RealmManager.realmManager.realm.displays[i] = RealmManager.realmManager.activeObject;
                }
                i += 1;
            }
        }
        else if (RealmManager.realmManager.activeObject.rObjectType == RObjectType.Waypo) {
            int i = 0;
            foreach (RObject waypo in RealmManager.realmManager.realm.waypos)
            {
                if (waypo.uid == RealmManager.realmManager.activeObject.uid)
                {
                    RealmManager.realmManager.realm.waypos[i] = RealmManager.realmManager.activeObject;
                }
                i += 1;
            }
        }
        DataManager.SaveData();
    }

    public void PlaceObject()
    {
        RealmManager.realmManager.activeObject.hasBeenPlaced = true;
        // here is where we assign

        switch (RealmManager.realmManager.activeObject.rObjectType) {
            case RObjectType.Default:
                Debug.Log("Default type");
                break;
            case RObjectType.Anchor:
                Debug.Log("Anchor type");
                break;
            case RObjectType.Waypo:
                RealmManager.realmManager.activeObject.imageName = "Waypos";
                SaveActiveObject();
                break;
            case RObjectType.Display:
                Debug.Log("Display type");
                break;
            default:
                Debug.Log("Couldn't find type");
                break;
        }

        RealmManager.realmManager.activeObject = new RObject();
        DataManager.SaveData();
    }
}

[System.Serializable]
public class RPoint {
    public float rx;
    public float ry;
    public float rz;

    public RPoint() {
        rx = 0.0f;
        ry = 0.0f;
        rz = 0.0f; 
    }
}