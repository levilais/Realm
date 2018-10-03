using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public GameObject textfieldMenuItemPF; // This is our prefab object that will be exposed in the inspector
    public GameObject inputScrollView;

    void Start()
    {
        InitializeTextfields();
        ResizeScrollView();
    }

    private void OnEnable()
    {
        if (transform.childCount > 0)
        {
            RepopulateExistingTextfields();
        }
    }

    private void RepopulateExistingTextfields()
    {
        ViewController view = transform.GetComponentInParent<ViewController>();
        int i = 0;
        foreach (Transform textfield in transform)
        {
            TextfieldMenuItem textfieldMenuItem = textfield.GetComponent<TextfieldMenuItem>();
            textfieldMenuItem.name = view.textfields[i].ToString().Replace("_", " ");

            string title, placeholder, text;
            SetProperties(view, i, out title, out placeholder, out text);

            GameObject viewManagerObj = transform.GetComponentInParent<ViewManager>().gameObject;
            textfieldMenuItem.viewManager = viewManagerObj;
            textfieldMenuItem.PopulateTextfield(title, text, placeholder);
            i += 1;
        }
    }

    void InitializeTextfields()
    {
        ViewController view = transform.GetComponentInParent<ViewController>();
        GameObject newTextfieldMenuItemObj; // Create GameObject instance

        for (int i = 0; i < view.textfields.Count; i++)
        {
            newTextfieldMenuItemObj = (GameObject)Instantiate(textfieldMenuItemPF, transform);
            newTextfieldMenuItemObj.name = view.textfields[i].ToString().Replace("_", " ");

            string title, placeholder, text;
            SetProperties(view, i, out title, out placeholder, out text);

            GameObject viewManagerObj = transform.GetComponentInParent<ViewManager>().gameObject;
            TextfieldMenuItem textfieldMenuItem = newTextfieldMenuItemObj.GetComponent<TextfieldMenuItem>();
            textfieldMenuItem.viewManager = viewManagerObj;
            textfieldMenuItem.PopulateTextfield(title, text, placeholder);
        }
    }

    private static void SetProperties(ViewController view, int i, out string title, out string placeholder, out string text)
    {
        title = view.textfields[i].ToString().Replace("_", " ");
        placeholder = "Enter " + title + "...";
        text = "Default";
        Realm realm = RealmManager.realmManager.realm;
        ViewController.TextfieldMenuItemTitle textFieldName = view.textfields[i];

        if (view.menuName == ViewController.ViewName.Info)
        {
            switch (textFieldName)
            {
                case ViewController.TextfieldMenuItemTitle.Realm_Name:
                    text = realm.realmName;
                    break;
                case ViewController.TextfieldMenuItemTitle.Account_Manager_Name:
                    text = realm.accountManagerName;
                    break;
                case ViewController.TextfieldMenuItemTitle.Company_Name:
                    text = realm.companyName;
                    break;
                case ViewController.TextfieldMenuItemTitle.Contact_Email:
                    text = realm.contactEmail;
                    break;
                case ViewController.TextfieldMenuItemTitle.Contact_Phone:
                    text = realm.contactPhone;
                    break;
                default:
                    text = "Default";
                    Debug.Log("Error");
                    break;
            }
        }

        if (view.menuName == ViewController.ViewName.Display_Info)
        {
            switch (textFieldName)
            {
                case ViewController.TextfieldMenuItemTitle.Display_Name:
                    text = RealmManager.realmManager.activeObject.name;
                    break;
                case ViewController.TextfieldMenuItemTitle.Display_ID:
                    text = RealmManager.realmManager.activeObject.displayID;
                    break;
                default:
                    text = "Default";
                    break;
            }
        }
    }

    private void ResizeScrollView()
    {
        RectTransform parentRect = inputScrollView.GetComponent<RectTransform>();
        GridLayoutGroup gridLayout = gameObject.GetComponent<GridLayoutGroup>();
        gridLayout.cellSize = new Vector2(parentRect.rect.width - 20, 95);
    }
}
