using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour {
    
    // STANDARD MENU ITEMS
    public List<string> MainMenu = new List<string>() 
    { 
        "Profile", 
        "Displays" 
    };

    public List<string> Profile = new List<string>()
    {
        "Back",
        "Info",
    };

    public List<string> Displays = new List<string>()
    {
        "Back",
        "New",
        "Edit",
        "Wayposts",
        "Anchor"
    };

    public List<string> SubMenuItmesForMenuNamed(string currentMenu) {
        List<string> returnValue;
        switch (currentMenu) {
            case "Main Menu":
                returnValue = MainMenu;
                break;
            case "Profile":
                returnValue = Profile;
                break;
            case "Displays":
                returnValue = Displays;
                break;
            default:
                return null;
                break;
        }
        return returnValue;
    }
}
