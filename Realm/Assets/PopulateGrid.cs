using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public GameObject menuButtonStandardPrefab; // This is our prefab object that will be exposed in the inspector
    public GameObject menuParent;

    //List<string> buttons = new List<string>() { "New", "Edit", "Wayposts", "Anchor" };

    void Start()
    {
        CreateButtons();
    }

    void Update()
    {

    }

    void CreateButtons()
    {
        var menuButtons = menuParent.GetComponent<MenuPanelStandard>().subMenuItems;

        // We start at index 1 because the left button is first in list and separate
        GameObject newMenuButton; // Create GameObject instance
        for (int i = 1; i < menuButtons.Count; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            newMenuButton = (GameObject)Instantiate(menuButtonStandardPrefab, transform);
            MenuButtonStandardInitializer menuButton = new MenuButtonStandardInitializer(newMenuButton, menuButtons[i]);

            //// Randomize the color of our image
            //newObj.GetComponent().color = Random.ColorHSV();
        }
    }
}
