using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayImage : MonoBehaviour
{

    public Image image;
    public GameObject TintOverlay;
    public GameObject SelectedOverlay;

    public string photoName;
    public string photoAddress;

    public void InitializePhoto()
    {
        image.sprite = Resources.Load<Sprite>("DisplayImages/" + photoName);
    }

    public void SelectPhoto()
    {
        foreach (Transform imageChild in transform.GetComponentInParent<GridLayoutGroup>().transform)
        {
            DisplayImage displayImage = imageChild.GetComponent<DisplayImage>();
            if (displayImage.photoName != photoName)
            {
                displayImage.ShowDeselect();
            }
        }

        ShowSelect();
        UpdateAndSave();
    }

    private void UpdateAndSave()
    {
        RealmManager.realmManager.activeObject.imageName = photoName;
        RealmManager.realmManager.activeObject.SaveActiveObject();
    }

    public void ShowSelect()
    {
        TintOverlay.SetActive(false);
        SelectedOverlay.SetActive(true);
    }

    public void ShowDeselect()
    {
        TintOverlay.SetActive(true);
        SelectedOverlay.SetActive(false);
    }
}

