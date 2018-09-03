using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public GameObject menuButtonStandardPrefab; // This is our prefab object that will be exposed in the inspector

    List<string> buttons = new List<string>() { "New", "Edit", "Wayposts", "Anchor" };

    void Start()
    {
        Populate();
    }

    void Update()
    {

    }

    void Populate()
    {
        GameObject newObj; // Create GameObject instance

        for (int i = 0; i < buttons.Count; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            newObj = (GameObject)Instantiate(menuButtonStandardPrefab, transform);
            MenuButtonStandard menuButton = new MenuButtonStandard(newObj, buttons[i]);

            //// Randomize the color of our image
            //newObj.GetComponent().color = Random.ColorHSV();
        }

    }
}
