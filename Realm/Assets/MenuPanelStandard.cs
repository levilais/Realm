using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelStandard : MonoBehaviour {
    public string menuName;

    public GameObject menuButtonStandardPrefab;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject headerIcon;
    public GameObject headerTitle;

    public bool backButtonExists;
    public bool nextButtonExists;
    public string previousMenuPanelName;
    public bool isNavigationOnly;
    public List<string> subMenuItems;

	// Use this for initialization
	void Start () {
        rightButton.SetActive(nextButtonExists);
        PopulateButtons();
        PopulateHeader();
	}

    private void PopulateHeader()
    {
        headerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/" + menuName + "Icon");
        headerTitle.GetComponent<Text>().text = menuName;
    }

    private void PopulateButtons()
    {
        NavigationManager navigationManager = new NavigationManager();
        subMenuItems = navigationManager.SubMenuItmesForMenuNamed(menuName);
        if (subMenuItems.Contains("Back")) {
            backButtonExists = true;
        }

        //leftButton.transform.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Images/" + menuName);
        MenuButtonStandardInitializer menuButton = new MenuButtonStandardInitializer(leftButton, subMenuItems[0]);
      
    //imagePressed = Resources.Load<Sprite>("Images/" + _buttonName + "Pressed");    
        //          button = buttonParent.GetComponentInChildren<Button>();
        //button.image.sprite = imageDefault;

        //SpriteState spriteState = new SpriteState();
        //spriteState = button.spriteState;
        //if (button.spriteState.pressedSprite)
        //{
        //    spriteState.pressedSprite = imageDefault;
        //}
        //else
        //{
        //    spriteState.pressedSprite = imagePressed;
        //}
        //button.spriteState = spriteState
    }

    //// Update is called once per frame
    //void Update () {

    //}
}
