using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDetailView : MonoBehaviour {

    public GameObject MenuButtonPF;
    private bool buttonsExist = false;

    private void OnEnable()
    {
        PopulateHeader();

        if (!buttonsExist) {
            PopulateDynamicButtons();
            buttonsExist = true;
        }
    }

    private void PopulateHeader() {
        // this is where we'll set the name of the detail display to the name if this item
        //We'll need to set the activeObject in the DisplaysView when the button is pressed. That's how we'll determine the title, too.

        RObject activeDisplay = RealmManager.realmManager.activeObject;
        string headerTitle = activeDisplay.name;

        ViewController viewController = transform.GetComponent<ViewController>();
        viewController.headerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/DisplaysIcon");
        viewController.headerTitle.GetComponent<Text>().text = headerTitle;
    }

    private void PopulateDynamicButtons()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject newMenuButton = (GameObject)Instantiate(MenuButtonPF, transform);
            MenuButton menuButton = newMenuButton.GetComponent<MenuButton>();

            switch (i)
            {
                case 0:
                    menuButton.InitializeButtonProperties("Position", "Position", "Position_Display");
                    break;
                case 1:
                    menuButton.InitializeButtonProperties("Adjust", "Edit", "Adjust_Display");
                    break;
                case 2:
                    menuButton.InitializeButtonProperties("Image", "Image", "Image_Select");
                    break;
                case 3:
                    menuButton.InitializeButtonProperties("Details", "Details", "Display_Info");
                    break;
                case 4:
                    // Delete_Popup will have to trigger a popup to confirm, the "back" nav, and handle the delete function
                    menuButton.InitializeButtonProperties("Delete", "Delete", "Delete_Popup");
                    break;
                default:
                    Debug.Log("Default called");
                    break;
            }

            newMenuButton.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
            menuButton.menuPanelObject = gameObject;
        }

        //We'll need to set the activeObject in the DisplaysView when the button is pressed. That's how we'll determine the title, too.
        //RObject display = RealmManager.realmManager.activeObject;
        //menuButton.rObject = display;
    }
}
