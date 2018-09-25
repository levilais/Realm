using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RObject {
    public string title;
    public string imageName;
    public string uid;
    public RPoint rPoint;

    public RObject() {
        title = "Default";
        imageName = "Default";
        uid = "0";
        rPoint = new RPoint();
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

[System.Serializable]
public class Waypo : RObject {
    public bool hasBeenPlaced;

    public Waypo() {
        hasBeenPlaced = false;
    }
}