using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    MenuButtonStandard menuButton;

    void Start()
    {
        //Debug.Log("start called");
        //HardCodeName();
        ////SetButtonProperties(menuButton);
        //Debug.Log("buttonName = " + menuButton.buttonName);
    }

    void HardCodeName() 
    {
        menuButton = new MenuButtonStandard(gameObject, "Displays");
    }
}
