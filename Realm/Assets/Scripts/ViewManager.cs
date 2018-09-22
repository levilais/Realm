using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour {

    [Header("Instance Declaration")]
    public static ViewManager viewManager = null;

    [Space(6)]
    [Header("Singleton Properties (Runtime)")]
    public List<GameObject> views;
    public List<GameObject> navigationHistory;

    [Header("Connections")]
    public GameObject AlertPF;

    [Space(6)]
    [Header("Object Access")]
    public GameObject TextfieldOverlay;
    public GameObject MenuPanelObj;
    public GameObject MenuPanelShortObj;
    public GameObject MenuPanelTallObj;

    //Awake is always called before any Start functions
    private void Awake()
    {
        //Check if instance already exists
        if (viewManager == null)
            //if not, set instance to this
            viewManager = this;
        //If instance already exists and it's not this:
        else if (viewManager != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a MenuManager.
            Destroy(gameObject);
        
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        // Add menu 
        foreach (GameObject menuObject in gameObject.transform) {
            viewManager.views.Add(menuObject);
        }
    }

    public void performSegue(string toMenu, GameObject fromMenu) {

        bool skipAddingToNavHistory = false;

        if (fromMenu.name == "Anchor")
        {
            skipAddingToNavHistory = true;
        }

        Debug.Log("toMenu: " + toMenu);

        string targetMenu;
        switch (toMenu) {
            case "Back":
                targetMenu = navigationHistory[navigationHistory.Count - 1].name;
                navigationHistory.RemoveAt(navigationHistory.Count - 1);
                skipAddingToNavHistory = true;
                break;
            case "Next":
                // todo: Segue to next menu item in sequence... for now do nothing.
                PresentAlert("Oops!", "This option hasn't finished being created yet.  Check back later.", "OK");
                targetMenu = toMenu;
                skipAddingToNavHistory = true;
                break;
            case "BEGIN":
                if (RealmManager.realmManager.anchorExists) {
                    targetMenu = "BEGIN";
                } else {
                    // will be "Intro" and do some logic to determine how built-out the realm is
                    targetMenu = "BEGIN";
                }
                skipAddingToNavHistory = true;
                break;
            case "Exit":
                targetMenu = navigationHistory[navigationHistory.Count - 1].name;
                navigationHistory.RemoveAt(navigationHistory.Count - 1);
                skipAddingToNavHistory = true;
                break;
            case "New":
                targetMenu = "PlacementView";
                break;
            default:
                targetMenu = toMenu;
                break;
        }

        List<string> existingMenus = new List<string>();
        foreach (Transform menuObject in viewManager.transform)
        {
            existingMenus.Add(menuObject.name);
            if (menuObject.name == targetMenu)
            {
                menuObject.gameObject.SetActive(true);
                if (!skipAddingToNavHistory) {
                    navigationHistory.Add(fromMenu);
                }
            }
        }

        if (existingMenus.Contains(targetMenu)) {
            fromMenu.SetActive(false);
        } else
        {
            PresentAlert("Oops!", "This option hasn't finished being created yet.  Check back later.", "OK");
        }
    }

    public void ToggleMenuPanelBackground(Transform menuObject)
    {
        string menuDisplayTypeAsString = menuObject.GetComponent<ViewController>().menuDisplayType.ToString();
        switch (menuDisplayTypeAsString)
        {
            case "MenuPanel":
                MenuPanelObj.SetActive(true);
                MenuPanelTallObj.SetActive(false);
                MenuPanelShortObj.SetActive(false);
                break;
            case "MenuPanelTall":
                MenuPanelObj.SetActive(false);
                MenuPanelTallObj.SetActive(true);
                MenuPanelShortObj.SetActive(false);
                break;
            case "MenuPanelShort":
                MenuPanelObj.SetActive(false);
                MenuPanelTallObj.SetActive(false);
                MenuPanelShortObj.SetActive(true);
                break;
            default:
                Debug.Log("Error finding panel type");
                break;
        }
    }

    public void PresentAlert(string title, string message, string buttonTitle)
    {
        GameObject canvasObj = transform.GetComponentInParent<Canvas>().gameObject;
        GameObject newAlert = (GameObject)Instantiate(AlertPF, canvasObj.transform);
        Alert alert = newAlert.GetComponent<Alert>();
        alert.PresentAlertMessage(canvasObj, title, message, buttonTitle);
    }
}
