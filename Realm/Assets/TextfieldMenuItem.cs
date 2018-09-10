using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextfieldMenuItem : MonoBehaviour
{
    public GameObject viewManager;
    public GameObject titleText;
    public GameObject placeholderText;
    public GameObject textField;

    void Start() {   
        titleText.GetComponent<Text>().text = transform.name;
        placeholderText.GetComponent<Text>().text = "Enter " + transform.name + "...";
        textField.GetComponent<TextfieldOverlay>().viewManager = viewManager;
    }
}
