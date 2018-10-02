using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSelectView : MonoBehaviour {

    private RObject activeObject;

    private void OnEnable()
    {
        Debug.Log("ActiveObject title on enable: " + RealmManager.realmManager.activeObject.name);
        activeObject = RealmManager.realmManager.activeObject;
        PopulateHeader();
    }

    private void PopulateHeader()
    {
        ViewController viewController = transform.GetComponent<ViewController>();
        viewController.headerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/PhotosIcon");
        viewController.headerTitle.GetComponent<Text>().text = "Select Image For " + activeObject.name;
    }
}
