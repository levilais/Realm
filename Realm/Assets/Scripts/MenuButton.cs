using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject menuPanelObject;
    public GameObject menuPanelPF;

    public void NavigateToMenuClicked()
    {
        var menuPanelScript = menuPanelObject.GetComponent<MenuPanel>();
        if (transform.name != "Back") {
            menuPanelScript.navigationHistory.Add(menuPanelObject.name);
            menuPanelScript.menuName = transform.name;
            menuPanelObject.name = transform.name;
        } else {
            var lastName = menuPanelScript.navigationHistory[menuPanelScript.navigationHistory.Count - 1];
            Debug.Log("name: " + lastName);
            menuPanelScript.menuName = lastName;
            menuPanelObject.name = lastName;
            menuPanelScript.navigationHistory.RemoveAt(menuPanelScript.navigationHistory.Count - 1);
        }

        menuPanelObject.GetComponent<MenuPanel>().NavigateToMenu();
    }
}
