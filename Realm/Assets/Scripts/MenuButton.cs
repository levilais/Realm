using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    GameObject button = gameObject;
    MenuButtonStandard menuButton;

    void Start()
    {
        Debug.Log("start called");
        HardCodeName();
        Debug.Log("buttonName = " + menuButton.buttonName);
    }

    void HardCodeName() 
    {
        menuButton = new MenuButtonStandard("Display");
    }

    void SetButtonProperties()
    {
        button.GetComponent(<"Text">).get   
    }
}
