using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealmManager : MonoBehaviour {

    [Header("Instance Declaration")]
    public static RealmManager realmManager = null;

    public Realm realm;
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

        if (DataManager.eraseModeActive) {
            DataManager.SaveData();
        }

        DataManager.LoadData();
    }

    public void RegisterActiveObject(RObject rObject)
    {
        Debug.Log("Active object is " + rObject.name);
        RealmManager.realmManager.activeObject = rObject;
    }
}

