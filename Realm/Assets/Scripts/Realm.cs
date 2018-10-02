using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Realm {

    public bool anchorExists;
    public string realmName;
    public string accountManagerName;
    public string companyName;
    public string contactEmail;
    public string contactPhone;
    public RObject anchor;
    public List<RObject> waypos;
    public List<RObject> displays;
    public double lastDisplayNumber;

    public Realm()
    {
        anchorExists = false;
        realmName = "Default";
        accountManagerName = "Default";
        companyName = "Default";
        contactEmail = "Default";
        contactPhone = "Default";
        anchor = new RObject();
        waypos = new List<RObject>();
        displays = new List<RObject>();
        lastDisplayNumber = 0;
    }
}
