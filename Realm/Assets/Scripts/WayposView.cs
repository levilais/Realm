using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayposView : MonoBehaviour {
    
    public GameObject MenuButtonPF;

    public void OnEnable()
    {
        if (RealmManager.realmManager.waypos.Count > 0)
        {
            // do something with existing waypose
            Debug.Log(RealmManager.realmManager.waypos.Count + " waypos already exist");
        }
        else
        {
            CreateDefaultWaypos();
            RealmData.SaveWaypos();
        }
        PopulateDynamicButtons();
    }

    private void OnDisable()
    {
        RemoveDynamicButtons();
    }

    private void CreateDefaultWaypos()
    {
        if (RealmManager.realmManager.waypos.Count <= 0)
        {
            RealmManager.realmManager.waypos = new List<RObject>();
            int i = 0;
            while (i < 5)
            {
                RObject newWaypo = new RObject();
                string waypoNumber = (i + 1).ToString();
                string title = "WP-" + waypoNumber;
                newWaypo.title = title;
                newWaypo.imageName = "New";
                newWaypo.rObjectType = RObject.RObjectType.Waypo;
                RealmManager.realmManager.waypos.Add(newWaypo);
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
        for (int i = 0; i < RealmManager.realmManager.waypos.Count; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            RObject waypo = RealmManager.realmManager.waypos[i];
            GameObject newMenuButton = (GameObject)Instantiate(MenuButtonPF, transform);
            MenuButton menuButton = newMenuButton.GetComponent<MenuButton>();
            menuButton.InitializeButtonProperties(waypo.title, waypo.imageName, waypo.title);
            newMenuButton.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
            if (!waypo.hasBeenPlaced)
            {
                menuButton.navTarget = "PlaceWaypoView";
            } else {
                menuButton.navTarget = "waypo.Title";
            }
            menuButton.menuPanelObject = gameObject;
            menuButton.rObject = waypo;
        }
    }
}
