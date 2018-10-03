using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInfoView : MonoBehaviour {

    RObject activeObject;

    private void OnEnable()
    {
        activeObject = RealmManager.realmManager.activeObject;
        PopulateHeader();
    }

    public void PopulateHeader()
    {
        ViewController viewController = transform.GetComponent<ViewController>();
        viewController.headerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/DisplaysIcon");
        viewController.headerTitle.GetComponent<Text>().text = activeObject.name + " Info";
    }

}
