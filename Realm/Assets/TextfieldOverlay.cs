using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextfieldOverlay : MonoBehaviour {
    
    public GameObject viewManager;
    private GameObject textfieldOverlay;

    void Start()
    {
        MenuManager menuManager = viewManager.GetComponent<MenuManager>();
        textfieldOverlay = menuManager.TextfieldSelectedOverlay;
    }

    void Update()
    {
        //If the input field is focused, change its color to green.
        if (transform.GetComponent<InputField>().isFocused == true)
        {
            textfieldOverlay.SetActive(true);
        }
        else {
            textfieldOverlay.SetActive(false);
        }
    }
}
