using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextfieldOverlayInitializer : MonoBehaviour
{
    
    public GameObject titleText;
    public GameObject textfield;
    public GameObject placeholderText;
    public string menuItemName;
    public string currentText;

    private void OnEnable()
    {
        titleText.GetComponent<Text>().text = menuItemName;
        textfield.GetComponent<InputField>().text = currentText;
        placeholderText.GetComponent<Text>().text = "Enter " + menuItemName + "...";
    }

    private void OnDisable()
    {
        currentText = "";
    }

    private void Update()
    {
        textfield.GetComponent<InputField>().text = currentText;
    }
}
