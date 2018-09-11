using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextfieldOverlay : MonoBehaviour {
    
    public GameObject viewManager;
    bool contentIsAdjustedForKeyboard = false;

    private void Start()
    {
        viewManager = transform.GetComponentInParent<MenuManager>().gameObject;
    }

    void Update()
    {
        InputField inputField = transform.GetComponentInParent<InputField>();
        // MOBILE
        TouchScreenKeyboard keyboard = inputField.touchScreenKeyboard;
        if (inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Visible && !contentIsAdjustedForKeyboard) {
            ShowOverlay();
        }
        //if (inputField.touchScreenKeyboard.status != TouchScreenKeyboard.Status.Visible && contentIsAdjustedForKeyboard)
        //{
        //    HideOverlay();
        //}

        // EDITOR
        if (inputField.isFocused && !contentIsAdjustedForKeyboard)
        {
            ShowOverlay();
        }
        //if (!inputField.isFocused && contentIsAdjustedForKeyboard)
        //{
        //    HideOverlay();
        //}
    }

    public void ShowOverlay() {
        GameObject textfieldOverlay = transform.GetComponentInParent<MenuManager>().TextfieldOverlay.gameObject;
        textfieldOverlay.SetActive(true);
        contentIsAdjustedForKeyboard = true;
    }

    public void HideOverlay() {
        GameObject textfieldOverlay = transform.GetComponentInParent<MenuManager>().TextfieldOverlay.gameObject;
        textfieldOverlay.SetActive(false);
        contentIsAdjustedForKeyboard = false;
    }
}
