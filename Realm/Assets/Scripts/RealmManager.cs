using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealmManager : MonoBehaviour {

    [Header("Instance Declaration")]
    public static RealmManager realmManager = null;

    public bool anchorExists;
    public string realmName;
    public RObject anchor;
    public List<Waypo> waypos = new List<Waypo>();

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
    }

    private void Start()
    {
        RealmData.LoadWaypos();

        if (waypos.Count > 0) {
            // do something with existing waypose
            Debug.Log(waypos.Count + " waypos already exist");
        } else {
            int i = 0;
            while (i < 5)
            {
                Waypo newWaypo = new Waypo();
                string waypoNumber = (i + 1).ToString();
                Debug.Log(waypoNumber);
                newWaypo.title = "WayPoint-" + waypoNumber;
                Debug.Log(newWaypo.title);
                waypos.Add(newWaypo);
                i += 1;
            }
            RealmData.SaveWaypos();
        }
    }
}

