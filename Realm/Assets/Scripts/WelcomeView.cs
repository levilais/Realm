using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeView : MonoBehaviour {

    public GameObject realmImage;

    private void Start()
    {
        string oldName = RealmManager.realmManager.realmName;
        Debug.Log("realm name (old):" + oldName);
        Debug.Log("realm name prior to load: " + SaveLoad.realmName);

        SaveLoad.Load();
        Debug.Log("realm name after to load: " + SaveLoad.realmName);

        string newName = "Realm name 2";
        Debug.Log("realm name (new): " + newName);
        RealmManager.realmManager.realmName = newName;
        SaveLoad.Save();
        Debug.Log("realm name (new) at point of save: " + SaveLoad.realmName);
    }

    private void OnEnable()
    {
        realmImage.SetActive(true);
    }

    private void OnDisable()
    {
        realmImage.SetActive(false);
    }
}
