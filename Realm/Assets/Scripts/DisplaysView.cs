using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaysView : MonoBehaviour {

    public GameObject MenuButtonPF;

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

        foreach (Transform childObject in menuButtonsParent.transform)
        {
            Debug.Log("childObject name: " + childObject.name);
            Destroy(childObject.gameObject);
        }
    }

    private void PopulateDynamicButtons()
    {
        // create "New" button
        GameObject newMenuButton = (GameObject)Instantiate(MenuButtonPF, transform);
        MenuButton menuButton = newMenuButton.GetComponent<MenuButton>();

        // "PlacementView" will need to be a Display creation secquence
        menuButton.InitializeButtonProperties("Create", "New", "Display_Detail");
        newMenuButton.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
        menuButton.menuPanelObject = gameObject;

        // populate the rest
        if (RealmManager.realmManager.displays.Count > 0) {
            for (int i = 0; i < RealmManager.realmManager.displays.Count; i++)
            {
                // Create new instances of our prefab until we've created as many as we specified
                RObject display = RealmManager.realmManager.displays[i];
                GameObject newDisplayButton = (GameObject)Instantiate(MenuButtonPF, transform);
                MenuButton displayMenuButton = newDisplayButton.GetComponent<MenuButton>();
                displayMenuButton.InitializeButtonProperties(display.title, display.imageName, display.title);
                newDisplayButton.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
                displayMenuButton.navTarget = display.title;
                displayMenuButton.menuPanelObject = gameObject;
                displayMenuButton.rObject = display;
            }
        }
    }
}
