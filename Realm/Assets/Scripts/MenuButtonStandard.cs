using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonStandard : MonoBehaviour
{
    public Text titleText;
    public Button button;

    public string buttonName;
    public Sprite imageDefault;
    public Sprite imagePressed;

    public MenuButtonStandard(GameObject buttonParent, string _buttonName)
    {
        InitializeButton(buttonParent, _buttonName);
    }

    private void InitializeButton(GameObject buttonParent, string _buttonName)
    {
        SetProperties(_buttonName);
        SetButtonImagesForStates(buttonParent);
        SetButtonTitle(buttonParent, _buttonName);
    }

    private void SetProperties(string _buttonName)
    {
        buttonName = _buttonName;
        imageDefault = Resources.Load<Sprite>("Images/" + _buttonName);
        imagePressed = Resources.Load<Sprite>("Images/" + _buttonName + "Pressed");
    }

    private void SetButtonTitle(GameObject buttonParent, string _buttonName)
    {
        titleText = buttonParent.GetComponentInChildren<Text>();
        titleText.text = _buttonName;
    }

    private void SetButtonImagesForStates(GameObject buttonParent)
    {
        button = buttonParent.GetComponentInChildren<Button>();
        button.image.sprite = imageDefault;

        SpriteState spriteState = new SpriteState();
        spriteState = button.spriteState;
        if (button.spriteState.pressedSprite)
        {
            spriteState.pressedSprite = imagePressed;
        }
        else
        {
            spriteState.pressedSprite = imageDefault;
        }
        button.spriteState = spriteState;
    }
}
