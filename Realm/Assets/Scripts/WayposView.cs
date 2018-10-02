using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayposView : MonoBehaviour {
    
    public GameObject MenuButtonPF;

    public void OnEnable()
    {
        if (RealmManager.realmManager.realm.waypos.Count > 0)
        {
            // do something with existing waypose
            Debug.Log(RealmManager.realmManager.realm.waypos.Count + " waypos already exist");
        }
        else
        {
            CreateDefaultWaypos();
            DataManager.SaveData();
        }
        PopulateDynamicButtons();
    }

    private void OnDisable()
    {
        RemoveDynamicButtons();
    }

    private void CreateDefaultWaypos()
    {
        if (RealmManager.realmManager.realm.waypos.Count <= 0)
        {
            RealmManager.realmManager.realm.waypos = new List<RObject>();
            int i = 0;
            while (i < 5)
            {
                RObject newWaypo = new RObject();
                string waypoNumber = (i + 1).ToString();
                string title = "WP-" + waypoNumber;
                newWaypo.name = title;
                newWaypo.imageName = "New";
                newWaypo.rObjectType = RObject.RObjectType.Waypo;
                RealmManager.realmManager.realm.waypos.Add(newWaypo);
                i += 1;
            }
        }
    }

    private void RemoveDynamicButtons()
    {
        GameObject menuButtonsParent = transform.GetComponentInChildren<GridLayoutGroup>().gameObject;
        Debug.Log("menuButtonsParent name: " + menuButtonsParent.name);

        foreach (Transform childObject in menuButtonsParent.transform) {
            Debug.Log("childObject name: " + childObject.name);
            Destroy(childObject.gameObject);
        }
    }

    private void PopulateDynamicButtons()
    {
        for (int i = 0; i < RealmManager.realmManager.realm.waypos.Count; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            RObject waypo = RealmManager.realmManager.realm.waypos[i];
            GameObject newMenuButton = (GameObject)Instantiate(MenuButtonPF, transform);
            MenuButton menuButton = newMenuButton.GetComponent<MenuButton>();
            menuButton.InitializeButtonProperties(waypo.name, waypo.imageName, waypo.name);
            newMenuButton.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
            if (!waypo.hasBeenPlaced)
            {
                menuButton.navTarget = "Place_Waypo";
            } else {
                menuButton.navTarget = "waypo.Title";
            }
            menuButton.menuPanelObject = gameObject;
            menuButton.rObject = waypo;
        }
    }
}
