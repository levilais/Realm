using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealmManager : MonoBehaviour {

    [Header("Instance Declaration")]
    public static RealmManager realmManager = null;

    public bool anchorExists;
    public string realmName;
    public RObject anchor;
    public List<RObject> waypos;
    public List<RObject> displays;
    public RObject activeObject;

    private void Awake()
    {

        //Check if instance already exists
        if (realmManager == null)
            //if not, set instance to this
            realmManager = this;
        //If instance already exists and it's not this:
        else if (realmManager != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a RealmManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        // Will use persistent storage / database to determine if Anchor exists
        anchorExists = false;
        anchor = new RObject();

        RealmData.LoadRealmData();
    }

    public void RegisterActiveObject(string rObjectTitle)
    {
        int i = 0;
        Debug.Log("trying to find match to register");
        foreach (RObject waypo in RealmManager.realmManager.waypos)
        {
            if (waypo.title == rObjectTitle)
            {
                RealmManager.realmManager.activeObject = RealmManager.realmManager.waypos[i];
                Debug.Log("object registered");
            }
            i += 1;
        }
    }
}

