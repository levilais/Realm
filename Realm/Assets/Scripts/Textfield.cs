using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textfield : MonoBehaviour
{

    public GameObject viewManager;

    public string textFieldTitle;
    public string textFieldText;
    public string textFieldPlaceholderText;

    TextfieldOverlay textfieldOverlay;
    InputField inputField;

    string textfieldObjectName;
    bool contentIsAdjustedForKeyboard = false;

    private void Start()
    {
        InitializeObjects();
        textfieldObjectName = transform.parent.name;
    }

    private void InitializeObjects()
    {
        textfieldOverlay = transform.GetComponentInParent<ViewManager>().TextfieldOverlay.gameObject.GetComponent<TextfieldOverlay>();
        inputField = transform.GetComponent<InputField>();

        if (textFieldText != "Default") {
            inputField.text = textFieldText;
        }

        viewManager = transform.GetComponentInParent<ViewManager>().gameObject;
    }

    void Update()
    {
        // EDITOR
#if UNITY_EDITOR
        if (inputField.isFocused && !contentIsAdjustedForKeyboard)
        {
            UpdateText();
            ShowOverlay();
        }
        if (!inputField.enabled && contentIsAdjustedForKeyboard)
        {
            HideOverlay();
        }

        // MOBILE
#else
        TouchScreenKeyboard keyboard = inputField.touchScreenKeyboard;
        if (inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Visible && !contentIsAdjustedForKeyboard) {
            UpdateText();    
            ShowOverlay();
        }
#endif
    }

    public void UpdateText() {
        textfieldOverlay.currentText = inputField.text;
    }

    public void ShowOverlay() {
        textfieldOverlay.GetComponent<TextfieldOverlay>().textfield = gameObject.GetComponent<Textfield>();
        textfieldOverlay.gameObject.SetActive(true);
        contentIsAdjustedForKeyboard = true;
    }

    public void HideOverlay() {
        textfieldOverlay.gameObject.SetActive(false);
        contentIsAdjustedForKeyboard = false;
    }
}
