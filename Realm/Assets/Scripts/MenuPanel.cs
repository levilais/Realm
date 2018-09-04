using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour {
    public string menuName;

    public GameObject menuButtonPrefab;
    public GameObject leftButton;
    public GameObject middleButtons;
    public GameObject middleButtonsContentObject;
    public GameObject rightButton;
    public GameObject headerIcon;
    public GameObject headerTitle;

    public bool backButtonExists;
    public bool nextButtonExists;
    public bool isNavigationOnly;

    public List<string> navigationHistory;
    public List<string> subMenuItems;

	// Use this for initialization
	void Start ()
    {
        transform.name = menuName;
        NavigateToMenu();
    }

    public void GoToMenuOnButtonClicked(string buttonName) {
        menuName = buttonName;
        NavigateToMenu();
    }

    public void NavigateToMenu()
    {
        rightButton.SetActive(nextButtonExists);
        PopulateButtons();
        PopulateHeader();
        SetScrollviewProperties();
    }

    void SetScrollviewProperties()
    {
        SetScrollviewElasticity();
        SetScrollviewSize();
    }

    void SetScrollviewSize()
    {
        int canvasWidth = (int)transform.GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.width;
        int scrollWidth = (int)middleButtons.GetComponent<RectTransform>().rect.width;

        float newMaxAdjustment = 0;
        if (subMenuItems.Contains("Next") && scrollWidth != canvasWidth)
        {
            newMaxAdjustment = -100;
        }
        else if (!subMenuItems.Contains("Next") && scrollWidth == canvasWidth)
        {
            newMaxAdjustment = 100;
        }

        RectTransform myRectTransform = middleButtons.GetComponent<RectTransform>();
        myRectTransform.sizeDelta += new Vector2(newMaxAdjustment, 0);
    }

    void SetScrollviewElasticity()
    {
        int scrollButtonsWidth = subMenuItems.Count * 110;
        int middleButtonsWidth = (int)Math.Round(middleButtons.GetComponent<RectTransform>().rect.width);
        if (scrollButtonsWidth > middleButtonsWidth)
        {
            transform.GetComponentInChildren<ScrollRect>().movementType = ScrollRect.MovementType.Elastic;
        }
        else
        {
            transform.GetComponentInChildren<ScrollRect>().movementType = ScrollRect.MovementType.Clamped;
        }
    }

    void PopulateHeader()
    {
        headerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/" + menuName + "Icon");
        headerTitle.GetComponent<Text>().text = menuName;
    }

    void PopulateButtons()
    {
        NavigationManager navigationManager = new NavigationManager();
        subMenuItems = navigationManager.SubMenuItmesForMenuNamed(menuName);
        if (subMenuItems.Contains("Back")) {
            backButtonExists = true;
        }

        if (subMenuItems.Contains("Next")) {
            rightButton.SetActive(true);
            nextButtonExists = true;
        } else {
            rightButton.SetActive(false);
            nextButtonExists = false;
            rightButton.name = "Next";
        }

        //leftButton.transform.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Images/" + menuName);
        MenuButtonObjectInitializer menuButton = new MenuButtonObjectInitializer(leftButton, subMenuItems[0]);
        Debug.Log("menu item count in MenuPanel script:" + subMenuItems.Count);

        // We start at index 1 because the left button is first in list and separate
        GameObject newMenuButton; // Create GameObject instance
        int timesThrough = subMenuItems.Count;
        if (nextButtonExists)
        {
            timesThrough -= 1;
        }

        foreach (Transform child in transform.GetComponentInChildren<GridLayoutGroup>().gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 1; i < timesThrough; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            newMenuButton = (GameObject)Instantiate(menuButtonPrefab, transform);
            MenuButtonObjectInitializer menuButtons = new MenuButtonObjectInitializer(newMenuButton, subMenuItems[i]);
            newMenuButton.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
            newMenuButton.name = subMenuItems[i];
            newMenuButton.GetComponent<MenuButton>().menuPanelObject = gameObject;
        }

        leftButton.name = subMenuItems[0];
    }
}
