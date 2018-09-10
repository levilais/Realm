using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    [Header("Instance Declaration")]
    public static MenuManager menuManager = null;

    [Space(6)]
    [Header("Singleton Properties (Runtime)")]
    public List<GameObject> menus;
    public List<GameObject> navigationHistory;

    [Header("Prefabs & Connections")]
    public GameObject AlertPF;
    public GameObject TextfieldSelectedOverlay;

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
        
        string targetMenu;
        switch (toMenu) {
            case "Back":
                targetMenu = navigationHistory[navigationHistory.Count - 1].name;
                navigationHistory.RemoveAt(navigationHistory.Count - 1);
                break;
            case "Next":
                // todo: Segue to next menu item in sequence... for now do nothing.
                PresentAlert("Oops!", "This option hasn't finished being created yet.  Check back later.", "OK");
                targetMenu = toMenu;
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
                //navigationHistory.Add(menuObject.gameObject);
                menuObject.gameObject.SetActive(true);
                if (toMenu != "Back" && toMenu != "Next") {
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

    public void PresentAlert(string title, string message, string buttonTitle)
    {
        GameObject newAlert = (GameObject)Instantiate(AlertPF, transform);
        Alert alert = newAlert.GetComponent<Alert>();
        alert.PresentAlertMessage(gameObject, title, message, buttonTitle);
    }
}
