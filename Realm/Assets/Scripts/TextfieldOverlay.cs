using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextfieldOverlay : MonoBehaviour
{

    public GameObject viewManager;
    GameObject textfieldOverlay;
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
        textfieldOverlay = transform.GetComponentInParent<ViewManager>().TextfieldOverlay.gameObject;
        inputField = transform.GetComponent<InputField>();
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
        TextfieldOverlayInitializer textfieldOverlayInitializer = textfieldOverlay.GetComponent<TextfieldOverlayInitializer>();
        textfieldOverlayInitializer.currentText = inputField.text;
    }

    public void ShowOverlay() {
        textfieldOverlay.GetComponent<TextfieldOverlayInitializer>().menuItemName = textfieldObjectName;
        textfieldOverlay.SetActive(true);
        contentIsAdjustedForKeyboard = true;
    }

    public void HideOverlay() {
        textfieldOverlay.SetActive(false);
        contentIsAdjustedForKeyboard = false;
    }
}
