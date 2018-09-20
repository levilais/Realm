using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewManager : MonoBehaviour {

    [Header("Instance Declaration")]
    public static ViewManager menuManager = null;

    [Space(6)]
    [Header("Singleton Properties (Runtime)")]
    public List<GameObject> menus;
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
        if (menuManager == null)
            //if not, set instance to this
            menuManager = this;
        //If instance already exists and it's not this:
        else if (menuManager != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a MenuManager.
            Destroy(gameObject);
        
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        // Add menu 
        foreach (GameObject menuObject in gameObject.transform) {
            menuManager.menus.Add(menuObject);
        }
    }

    public void performSegue(string toMenu, GameObject fromMenu) {
        
        List<string> skipAddingToNavigationHistory = new List<string>();

        string targetMenu;
        switch (toMenu) {
            case "Back":
                targetMenu = navigationHistory[navigationHistory.Count - 1].name;
                navigationHistory.RemoveAt(navigationHistory.Count - 1);
                skipAddingToNavigationHistory.Add(targetMenu);
                break;
            case "Next":
                // todo: Segue to next menu item in sequence... for now do nothing.
                PresentAlert("Oops!", "This option hasn't finished being created yet.  Check back later.", "OK");
                targetMenu = toMenu;
                skipAddingToNavigationHistory.Add(targetMenu);
                break;
            case "BEGIN":
                if (RealmManager.realmManager.anchorExists) {
                    targetMenu = "BEGIN";
                } else {
                    targetMenu = "Anchor";
                }
                skipAddingToNavigationHistory.Add(targetMenu);
                break;
            case "Exit":
                navigationHistory.Clear();
                targetMenu = "Welcome";
                skipAddingToNavigationHistory.Add(targetMenu);
                break;
            default:
                targetMenu = toMenu;
                break;
        }

        List<string> existingMenus = new List<string>();
        foreach (Transform menuObject in menuManager.transform)
        {
            existingMenus.Add(menuObject.name);
            if (menuObject.name == targetMenu)
            {
                //ToggleMenuPanelBackground(menuObject);
                menuObject.gameObject.SetActive(true);

                if (!skipAddingToNavigationHistory.Contains(targetMenu)) {
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
