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
        RealmManager.realmManager.RegisterActiveObject(title);
        ViewManager.viewManager.performSegue(navTarget, menuPanelObject);
    }
}
