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
        transform.name = imageName;

        // Set Title
        Text titleText = transform.GetComponentInChildren<Text>();
        titleText.text = title;

        // Set Default Image
        Button button = transform.GetComponentInChildren<Button>();
        button.image.sprite = Resources.Load<Sprite>("Images/" + imageName);

        // Set Pressed Image
        Sprite imagePressed = Resources.Load<Sprite>("Images/" + imageName + "Pressed");
        SpriteState spriteState = new SpriteState();
        spriteState.pressedSprite = imagePressed;
        button.spriteState = spriteState;
    }

    public void PerformSegueToTarget()
    {
        if (rObject.rObjectType != RObject.RObjectType.Default)
        {
            RealmManager.realmManager.RegisterActiveObject(rObject);
            CreateDisplayIfNecessary();
        }
        ViewManager.viewManager.performSegue(navTarget, menuPanelObject);
    }

    public void CreateDisplayIfNecessary() {
        if (title == "Create") {
            Debug.Log("creating...");
            RealmManager.realmManager.displays.Add(rObject);
            RealmManager.realmManager.lastDisplayNumber = rObject.displayNumber;
            DataManager.SaveData();
        } 
    }
}
