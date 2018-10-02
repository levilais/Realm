using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextfieldMenuItem : MonoBehaviour
{
    public GameObject viewManager;
    public GameObject titleText;
    public GameObject placeholderText;
    public GameObject textFieldText;
    public Textfield textField;

    void Start() {
        textField.viewManager = viewManager;
    }

    public void PopulateTextfield(string title, string text, string placeholder)
    {
        textField.textFieldText = text;
        textField.textFieldTitle = title;
        textField.textFieldPlaceholderText = placeholder;

        titleText.GetComponent<Text>().text = title;

        if (text == "Default")
        {
            placeholderText.GetComponent<Text>().text = placeholder;
        } else {
            textFieldText.GetComponent<Text>().text = text;
        }
    }
}
