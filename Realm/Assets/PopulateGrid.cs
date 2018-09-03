using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public GameObject menuButtonStandardPrefab; // This is our prefab object that will be exposed in the inspector

    public int numberOfButtons; // number of objects to create. Exposed in inspector

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

        for (int i = 0; i < numberOfButtons; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            newObj = (GameObject)Instantiate(menuButtonStandardPrefab, transform);

            //// Randomize the color of our image
            //newObj.GetComponent().color = Random.ColorHSV();
        }

    }
}
