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
        //SetTitleAndPlaceholder(transform.name);
        textField.GetComponent<TextfieldOverlay>().viewManager = viewManager;
    }

    public void PopulateTextfield(string title, string text, string placeholder)
    {
        Debug.Log("5");
        titleText.GetComponent<Text>().text = title;
        Debug.Log("6");
        if (text == "Default")
        {
            Debug.Log("7");
            placeholderText.GetComponent<Text>().text = placeholder;
        } else {
            Debug.Log("8");
            textField.GetComponentInChildren<Text>().text = text;
            Debug.Log("9");
        }

        // TODO: Here is where we'll set the placeholder if the value doesn't exist and where we'll set the actual value if not
        //  InputField inputField = gameObject.GetComponent<InputField>();
        //  inputField.text = "some text";
    }

    //public void SetTitleAndPlaceholder(string menuName) {
    //    titleText.GetComponent<Text>().text = menuName;

    //    // TODO: Here is where we'll set the placeholder if the value doesn't exist and where we'll set the actual value if not
    //    //  InputField inputField = gameObject.GetComponent<InputField>();
    //    //  inputField.text = "some text";

    //    placeholderText.GetComponent<Text>().text = "Enter " + menuName + "...";
    //}
}
