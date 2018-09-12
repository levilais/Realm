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
        SetTitleAndPlaceholder(transform.name);
        textField.GetComponent<TextfieldOverlay>().viewManager = viewManager;
    }

    public void SetTitleAndPlaceholder(string menuName) {
        titleText.GetComponent<Text>().text = menuName;
        placeholderText.GetComponent<Text>().text = "Enter " + menuName + "...";
    }
}
