using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RObject {
    public string title;
    public string imageName;
    public System.Guid uid;
    public double displayNumber;
    public RPoint rPoint;

    public bool hasBeenPlaced;
    public bool imageExists;

    public enum RObjectType {Default, Waypo, Display, Anchor}
    public RObjectType rObjectType;

    public RObject() {
        title = "Default";
        imageName = "Default";
        uid = System.Guid.NewGuid();
        hasBeenPlaced = false;
        imageExists = false;
        rPoint = new RPoint();
        rObjectType = RObjectType.Default;
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

                int i = 0;
                Debug.Log("trying to find match");
                foreach (RObject waypo in RealmManager.realmManager.waypos) {
                    if (waypo.uid == RealmManager.realmManager.activeObject.uid) {
                        RealmManager.realmManager.waypos[i] = RealmManager.realmManager.activeObject;
                        Debug.Log("match found");
                    }
                    i += 1;
                }
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