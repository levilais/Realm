using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject menuPanelObject;

    public string title;
    public string imageName;
    public string navTarget;
    public bool isDisplayButton;
    public RObject rObject;

    public void InitializeButtonProperties(string buttonTitle, string buttonImageName, string buttonNavTarget)
    {
        title = buttonTitle;
        imageName = buttonImageName;
        navTarget = buttonNavTarget;
        UpdateButtonProperties();
    }

    public void UpdateButtonProperties()
    {
        // Set Name
        transform.name = title;

        // Set Title
        Text titleText = transform.GetComponentInChildren<Text>();
        titleText.text = title;

        Button button = transform.GetComponentInChildren<Button>();

        string imageAddressPrefix;
        if (isDisplayButton) {
            imageAddressPrefix = "DisplayImages/";
        } else {
            // Set Default Image
            imageAddressPrefix = "Images/";
            // Set Pressed Image
            Sprite imagePressed = Resources.Load<Sprite>(imageAddressPrefix + imageName + "Pressed");
            SpriteState spriteState = new SpriteState();
            spriteState.pressedSprite = imagePressed;
            button.spriteState = spriteState;
        }

        button.image.sprite = Resources.Load<Sprite>(imageAddressPrefix + imageName);
    }

    public void PerformSegueToTarget()
    {
        Debug.Log("PerformSegueToTarget attempted");
        if (rObject.rObjectType != RObject.RObjectType.Default)
        {
            Debug.Log("2");
            RealmManager.realmManager.RegisterActiveObject(rObject);
            Debug.Log("3");
            CreateDisplayIfNecessary();
            Debug.Log("4");
        }
        ViewManager.viewManager.performSegue(navTarget, menuPanelObject);
    }

    public void CreateDisplayIfNecessary() {
        if (title == "Create") {
            Debug.Log("creating...");
            RealmManager.realmManager.realm.displays.Add(rObject);
            RealmManager.realmManager.realm.lastDisplayNumber = rObject.displayNumber;
            DataManager.SaveData();
        } 
    }
}
