using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSelectView : MonoBehaviour {

    public GameObject DisplayImagePF;
    private RObject activeObject;

    private void OnEnable()
    {
        Debug.Log("ActiveObject title on enable: " + RealmManager.realmManager.activeObject.name);
        activeObject = RealmManager.realmManager.activeObject;
        PopulateHeader();
        PopulateImages();
    }

    private void PopulateHeader()
    {
        ViewController viewController = transform.GetComponent<ViewController>();
        viewController.headerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/PhotosIcon");
        viewController.headerTitle.GetComponent<Text>().text = "Select Image For " + activeObject.name;
    }

    private void PopulateImages()
    {
        // create "current" image
        GameObject currentImageObj = (GameObject)Instantiate(DisplayImagePF, transform);

        DisplayImage currentDisplayImage = currentImageObj.GetComponent<DisplayImage>();
        currentDisplayImage.photoName = RealmManager.realmManager.activeObject.imageName;
        currentDisplayImage.InitializePhoto();
        currentDisplayImage.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;

        {
            for (int i = 0; i < 3; i++)
            {
                string imageNameSuffix = "0" + (i + 1);
                string photoName = "DisplayImage" + imageNameSuffix;

                if (photoName != currentDisplayImage.photoName)
                {
                GameObject newDisplayImage = (GameObject)Instantiate(DisplayImagePF, transform);
                DisplayImage displayImage = newDisplayImage.GetComponent<DisplayImage>();
                displayImage.photoName = photoName;
                displayImage.InitializePhoto();
                newDisplayImage.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
                }
            }
        }

        currentDisplayImage.SelectPhoto();
    }

    public void SaveSelectedImage()
    {
        Debug.Log("save selected image called, still need to impliment");
    }

    public void ClearImages() {
        foreach (Transform image in transform.GetComponentInChildren<GridLayoutGroup>().transform) {
            Destroy(image.gameObject);
        }
    }

    private void OnDisable()
    {
        ClearImages();
    }
}
