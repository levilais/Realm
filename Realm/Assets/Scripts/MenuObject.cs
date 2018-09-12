using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuObject : MonoBehaviour {

    [Header("Object Info")]
    public GameObject menuManager;
    public MenuName menuName;
    public MenuDisplayType menuDisplayType;
    public MenuUseType menuUseType;
    public List<MenuName> subMenuItems;
    public bool nextButtonExists;
    public string nextButtonCustomTitle;
    public bool backButtonExists;

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
    public GameObject leftButtonShort;
    public GameObject leftButtonTitle;
    public GameObject rightButtonShort;
    public GameObject rightButtonTitle;
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
        //NavigationManager navigationManager = new NavigationManager();
        //subMenuItems = navigationManager.SubMenuItmesForMenuNamed(menuName.ToString());
        if (subMenuItems.Contains(MenuName.Back))
        {
            backButtonExists = true;
        }

        if (subMenuItems.Contains(MenuName.Next))
        {
            rightButton.SetActive(true);
            nextButtonExists = true;
        }
        else
        {
            rightButton.SetActive(false);
            nextButtonExists = false;

            rightButton.name = "Next";
        }

        //leftButton.transform.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Images/" + menuName.ToString());
            MenuButtonObjectInitializer menuButton = new MenuButtonObjectInitializer(leftButton, subMenuItems[0].ToString());

        // We start at index 1 because the left button is first in list and separate
        GameObject newMenuButton; // Create GameObject instance
        int timesThrough = subMenuItems.Count;
        if (nextButtonExists)
        {
            timesThrough -= 1;
        }

        for (int i = 1; i < timesThrough; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            newMenuButton = (GameObject)Instantiate(MenuButtonPF, transform);
            MenuButtonObjectInitializer menuButtons = new MenuButtonObjectInitializer(newMenuButton, subMenuItems[i].ToString());
            newMenuButton.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
            newMenuButton.name = subMenuItems[i].ToString();
            newMenuButton.GetComponent<MenuButton>().menuPanelObject = gameObject;
        }
            leftButton.name = subMenuItems[0].ToString();
    }
}
