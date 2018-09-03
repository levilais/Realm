using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour {

    List<string> Menu = new List<string>() 
    { 
        "Profile", 
        "Displays" 
    };

    List<string> Profile = new List<string>()
    {
        "Back",
        "Info",
    };

    List<string> Displays = new List<string>()
    {
        "Back",
        "New",
        "Edit",
        "Wayposts",
        "Anchor"
    };






	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
