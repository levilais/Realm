using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject menuPanelObject;

    public void NavigateToMenuClicked()
    {
        ViewManager.menuManager.performSegue(transform.name, menuPanelObject);
    }


}
