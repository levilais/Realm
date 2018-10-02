using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextfieldOverlay : MonoBehaviour
{
    public Textfield textfield;
    public GameObject textFieldObj;
    public GameObject titleText;
    public GameObject placeholderText;
    public string currentText;


    private void OnEnable()
    {
        titleText.GetComponent<Text>().text = textfield.textFieldTitle;
        textfield.gameObject.GetComponent<InputField>().text = currentText;
        placeholderText.GetComponent<Text>().text = textfield.textFieldPlaceholderText;
        GetComponentInParent<Canvas>().sortingOrder = 2;
    }

    private void OnDisable()
    {
        currentText = "";
        GetComponentInParent<Canvas>().sortingOrder = 0;
    }

    private void Update()
    {
        textFieldObj.GetComponent<InputField>().text = currentText;
    }
}
