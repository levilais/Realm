using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour {

    [Header("Object Info")]
    public GameObject viewManager;
    public ViewName menuName;
    public ViewMenuType menuDisplayType;
    public ViewType menuUseType;
    public List<ViewName> subMenuItems;
    public List<string> subMenuItemNames;
    public bool rightButtonExists;
    public bool leftButtonExists;
    public bool hasDynamicMenuButtonTitles;

    [Space(6)]
    [Header("Header Objects")]
    public GameObject headerIcon;
    public GameObject headerTitle;
    public GameObject exitButton;

    [Space(6)]
    [Header("Standard & Tall Panel Objects")]
    public GameObject leftButton;
    public GameObject middleButtons;
    public GameObject rightButton;

    [Space(2)]
    [Header("Short Panel Objects")]
    public GameObject messageText;

    [Space(2)]
    [Header("Tall Panel Objects")]
    public GameObject headerMessageText;
    public GameObject inputScrollView;
    public List<TextfieldMenuItemTitle> textfields;

    [Space(6)]
    [Header("Prefabs")]
    public GameObject MenuButtonPF;

    public enum ViewName { 
        Welcome,
        Enter,
        Menu, 
        Profile, 
        Displays, 
        Back,
        Info,
        New, 
        Edit,
        Waypos,
        Anchor,
        Next,
        BEGIN,
        Exit,
        Realm,
        PlacementView
    };

    public enum ViewMenuType {
        MenuPanel,
        MenuPanelShort,
        MenuPanelTall
    }


    public enum ViewType {
        Navigation,
        Input,
        Tool
    }

    public enum TextfieldMenuItemTitle
    {
        Realm_Name,
        Account_Manager_Name,
        Company_Name,
        Contact_Email,
        Contact_Phone,
        Dumby
    }

    string menuItemTitleString(TextfieldMenuItemTitle textfieldMenuItemTitle)
    {
        return textfieldMenuItemTitle.ToString().Replace("_", " ");
    }

    void Start()
    {
        transform.name = menuName.ToString();
        InstantiateMenuObject();
    }

    private void OnEnable()
    {
        transform.GetComponentInParent<ViewManager>().ToggleMenuPanelBackground(transform);
    }

    public void InstantiateMenuObject() {
        rightButton.SetActive(rightButtonExists);
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
        if (subMenuItems.Contains(ViewName.Next) && scrollWidth != canvasWidth)
        {
            newMaxAdjustment = -100;
        }
        else if (!subMenuItems.Contains(ViewName.Next) && scrollWidth == canvasWidth)
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
        headerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/" + menuName.ToString() + "Icon");
        headerTitle.GetComponent<Text>().text = menuName.ToString();
    }

    void PopulateButtons()
    {
        if (!hasDynamicMenuButtonTitles)
        {
            foreach (ViewName subMenuItem in subMenuItems)
            {
                subMenuItemNames.Add(subMenuItem.ToString());
            }
        }

        int i = 0;
        int timesThrough = subMenuItemNames.Count;

        if (leftButtonExists)
        {
            string menuButtonName = subMenuItemNames[0];
            MenuButton leftMenuButton = leftButton.GetComponent<MenuButton>();
            leftMenuButton.InitializeButtonProperties(menuButtonName, menuButtonName, menuButtonName);
            leftButton.SetActive(true);
            i += 1;
        }
        else
        {
            leftButton.SetActive(false);
        }

        if (rightButtonExists)
        {
            string menuButtonName = subMenuItemNames[timesThrough - 1];
            MenuButton rightMenuButton = rightButton.GetComponent<MenuButton>();
            rightMenuButton.InitializeButtonProperties(menuButtonName, menuButtonName, menuButtonName);
            rightButton.SetActive(true);
            timesThrough -= 1;
        }
        else
        {
            rightButton.SetActive(false);
        }

        for (int index = i; index < timesThrough; index++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            string menuButtonName = subMenuItemNames[index];
            GameObject newMenuButton = (GameObject)Instantiate(MenuButtonPF, transform);
            MenuButton menuButton = newMenuButton.GetComponent<MenuButton>();
            menuButton.InitializeButtonProperties(menuButtonName, menuButtonName, menuButtonName);
            newMenuButton.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
            menuButton.menuPanelObject = gameObject;
        }
    }

    //void PopulateButtons()
    //{
    //    if (!hasDynamicMenuButtonTitles) {
    //        foreach (ViewName subMenuItem in subMenuItems)
    //        {
    //            subMenuItemNames.Add(subMenuItem.ToString());
    //        }
    //    }

    //    int i = 0;
    //    int timesThrough = subMenuItemNames.Count;

    //    if (leftButtonExists) {
    //        string menuButtonName = subMenuItemNames[0];
    //        MenuButtonObjectInitializer menuButton = new MenuButtonObjectInitializer(leftButton, menuButtonName);
    //        leftButton.name = menuButtonName;
    //        leftButton.SetActive(true);
    //        i += 1;
    //    } else {
    //        leftButton.SetActive(false);
    //    }

    //    if (rightButtonExists) {
    //        string menuButtonName = subMenuItemNames[timesThrough - 1];
    //        MenuButtonObjectInitializer rightMenuButton = new MenuButtonObjectInitializer(rightButton, menuButtonName);
    //        rightButton.name = menuButtonName;
    //        rightButton.SetActive(true);
    //        timesThrough -= 1;
    //    } else {
    //        rightButton.SetActive(false);
    //    }

    //    for (int index = i; index < timesThrough; index++)
    //    {
    //        // Create new instances of our prefab until we've created as many as we specified
    //        GameObject newMenuButton = (GameObject)Instantiate(MenuButtonPF, transform);
    //        MenuButtonObjectInitializer menuButtons = new MenuButtonObjectInitializer(newMenuButton, subMenuItemNames[index]);
    //        newMenuButton.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
    //        newMenuButton.name = subMenuItemNames[index];
    //        newMenuButton.GetComponent<MenuButton>().menuPanelObject = gameObject;
    //    }
    //}
}
