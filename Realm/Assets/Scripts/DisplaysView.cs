using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaysView : MonoBehaviour {

    public GameObject MenuButtonPF;

    public void OnEnable()
    {
        Debug.Log("ActiveObject title on enable: " + RealmManager.realmManager.activeObject.name);
        PopulateDynamicButtons();
    }

    private void OnDisable()
    {
        RemoveDynamicButtons();
    }

    private void RemoveDynamicButtons()
    {
        GameObject menuButtonsParent = transform.GetComponentInChildren<GridLayoutGroup>().gameObject;

        foreach (Transform childObject in menuButtonsParent.transform)
        {
            Destroy(childObject.gameObject);
        }
    }

    private void PopulateDynamicButtons()
    {
        Debug.Log("displays count " + RealmManager.realmManager.realm.displays.Count);

        // create "New" button
        GameObject newMenuButton = (GameObject)Instantiate(MenuButtonPF, transform);
        MenuButton menuButton = newMenuButton.GetComponent<MenuButton>();

        // "PlacementView" will need to be a Display creation secquence
        menuButton.InitializeButtonProperties("Create", "New", "Display_Detail");
        newMenuButton.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
        menuButton.menuPanelObject = gameObject;

        RObject createButton = new RObject();
        createButton.displayNumber = RealmManager.realmManager.realm.lastDisplayNumber + 1;
        createButton.name = "Display " + (createButton.displayNumber).ToString();
        createButton.rObjectType = RObject.RObjectType.Display;
        menuButton.rObject = createButton;

        // populate the rest
        if (RealmManager.realmManager.realm.displays.Count > 0) {
            for (int i = 0; i < RealmManager.realmManager.realm.displays.Count; i++)
            {
                // Create new instances of our prefab until we've created as many as we specified
                RObject display = RealmManager.realmManager.realm.displays[i];
                GameObject newDisplayButton = (GameObject)Instantiate(MenuButtonPF, transform);
                MenuButton displayMenuButton = newDisplayButton.GetComponent<MenuButton>();

                string buttonImageName;

                if (!display.imageExists)
                {
                    buttonImageName = "Displays";
                }
                else
                {
                    buttonImageName = display.imageName;
                }

                displayMenuButton.InitializeButtonProperties(display.name, buttonImageName, display.name);
                newDisplayButton.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
                displayMenuButton.navTarget = "Display_Detail";
                displayMenuButton.menuPanelObject = gameObject;
                displayMenuButton.rObject = display;
                displayMenuButton.rObject.rObjectType = RObject.RObjectType.Display;
            }
        }
    }
}
