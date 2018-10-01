using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Realm {

    public bool anchorExists;
    public string realmName;
    public string companyName;
    public string email;
    public string phone;
    public RObject anchor;
    public List<RObject> waypos;
    public List<RObject> displays;
    public double lastDisplayNumber;

    public Realm()
    {
        anchorExists = false;
        realmName = "Default";
        companyName = "Default";
        email = "Default";
        phone = "Default";
        RObject anchor = new RObject();
        waypos = new List<RObject>();
        displays = new List<RObject>();
        lastDisplayNumber = 0;
    }
}
