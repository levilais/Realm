using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public GameObject textfieldMenuItemPF; // This is our prefab object that will be exposed in the inspector
    public GameObject inputScrollView;

    public int numberToCreate; // number of objects to create. Exposed in inspector

    void Start()
    {
        Populate();
        ResizeScrollView();
    }

    void Populate()
    {
        MenuObject menuObject = transform.GetComponentInParent<MenuObject>();
        GameObject newTextfieldMenuItemObj; // Create GameObject instance
        for (int i = 0; i < menuObject.textfields.Count; i++)
        {
            newTextfieldMenuItemObj = (GameObject)Instantiate(textfieldMenuItemPF, transform);
            newTextfieldMenuItemObj.name = menuObject.textfields[i].ToString().Replace("_"," ");
        }
    }

    private void ResizeScrollView()
    {
        RectTransform parentRect = inputScrollView.GetComponent<RectTransform>();
        GridLayoutGroup gridLayout = gameObject.GetComponent<GridLayoutGroup>();
        gridLayout.cellSize = new Vector2(parentRect.rect.width - 20, 95);
    }
}
