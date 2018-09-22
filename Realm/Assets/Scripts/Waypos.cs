using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypos : MonoBehaviour {

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

        for (int i = 0; i < waypoNameStrings.Count; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            string menuButtonName = waypoNameStrings[i];
            GameObject newMenuButton = (GameObject)Instantiate(MenuButtonPF, transform);
            MenuButton menuButton = newMenuButton.GetComponent<MenuButton>();
            string title = "WP-" + i;
            menuButton.InitializeButtonProperties(title, menuButtonName, title);
            newMenuButton.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
            menuButton.menuPanelObject = gameObject;
        }
    }
}
