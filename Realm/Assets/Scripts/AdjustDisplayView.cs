using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustDisplayView : MonoBehaviour {

    private RObject activeObject;
                                               
	private void OnEnable()
    {
        activeObject = RealmManager.realmManager.activeObject;
        PopulateHeader();
    }
 
	private void PopulateHeader() {
    // this is where we'll set the name of the detail display to the name if this item
    //We'll need to set the activeObject in the DisplaysView when the button is pressed. That's how we'll determine the title, too.
    ViewController viewController = transform.GetComponent<ViewController>();
    viewController.headerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/DisplaysIcon");
        viewController.headerTitle.GetComponent<Text>().text = "Adjust " + activeObject.title;
    }
}
