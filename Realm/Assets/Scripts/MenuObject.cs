using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuObject : MonoBehaviour {

    [Header("Object Info")]
    public GameObject viewManager;
    public MenuName menuName;
    public MenuDisplayType menuDisplayType;
    public MenuUseType menuUseType;
    public List<MenuName> subMenuItems;
    public bool rightButtonExists;
    //public bool backButtonExists;
    public bool leftButtonExists;

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

    public enum MenuName { 
        Welcome,
        Enter,
        Menu, 
        Profile, 
        Displays, 
        Back,
        Info,
        New, 
        Edit,
        Wayposts,
        Anchor,
        Next
    };

    public enum MenuDisplayType {
        MenuPanel,
        MenuPanelShort,
        MenuPanelTall
    }


    public enum MenuUseType {
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
        transform.GetComponentInParent<MenuManager>().ToggleMenuPanelBackground(transform);
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
        if (subMenuItems.Contains(MenuName.Next) && scrollWidth != canvasWidth)
        {
            newMaxAdjustment = -100;
        }
        else if (!subMenuItems.Contains(MenuName.Next) && scrollWidth == canvasWidth)
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
        int i = 0;
        int timesThrough = subMenuItems.Count;

        if (leftButtonExists) {
            MenuButtonObjectInitializer menuButton = new MenuButtonObjectInitializer(leftButton, subMenuItems[0].ToString());
            leftButton.name = subMenuItems[0].ToString();
            leftButton.SetActive(true);
            i += 1;
        } else {
            leftButton.SetActive(false);
        }

        if (rightButtonExists) {
            string menuButtonName = subMenuItems[timesThrough - 1].ToString();
            MenuButtonObjectInitializer rightMenuButton = new MenuButtonObjectInitializer(rightButton, menuButtonName);
            rightButton.name = menuButtonName;
            rightButton.SetActive(true);
            timesThrough -= 1;
        } else {
            rightButton.SetActive(false);
        }

        for (int index = i; index < timesThrough; index++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            GameObject newMenuButton = (GameObject)Instantiate(MenuButtonPF, transform);
            MenuButtonObjectInitializer menuButtons = new MenuButtonObjectInitializer(newMenuButton, subMenuItems[index].ToString());
            newMenuButton.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
            newMenuButton.name = subMenuItems[index].ToString();
            newMenuButton.GetComponent<MenuButton>().menuPanelObject = gameObject;
        }
    }
}
