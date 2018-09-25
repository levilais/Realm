using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayposView : MonoBehaviour {

    //public GameObject menuButtonParent;
    public List<WaypoName> waypoNames;
    public List<string> waypoNameStrings;
    public GameObject MenuButtonPF;

    public enum WaypoName
    {
        Back,
        New,
        Waypo,
        Anchor
    }

    public void OnEnable()
    {
        PopulateDynamicButtons();
    }

    private void OnDisable()
    {
        RemoveDynamicButtons();
    }

    private void RemoveDynamicButtons()
    {
        GameObject menuButtonsParent = transform.GetComponentInChildren<GridLayoutGroup>().gameObject;
        Debug.Log("menuButtonsParent name: " + menuButtonsParent.name);

        foreach (Transform childObject in menuButtonsParent.transform) {
            Debug.Log("childObject name: " + childObject.name);
            Destroy(childObject.gameObject);
        }
        waypoNameStrings.Clear();
    }

    private void PopulateDynamicButtons()
    {

        foreach (WaypoName waypoNameString in waypoNames)
        {
            waypoNameStrings.Add(waypoNameString.ToString());
        }

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
                menuButton.navTarget = "PlacementView";
            } else {
                menuButton.navTarget = "waypo.Title";
            }
            menuButton.menuPanelObject = gameObject;
        }
    }
}
