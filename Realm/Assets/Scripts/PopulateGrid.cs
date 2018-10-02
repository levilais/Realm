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
        Populate();
        //ResizeScrollView();
    }

    void Populate()
    {
        ViewController view = transform.GetComponentInParent<ViewController>();
        GameObject newTextfieldMenuItemObj; // Create GameObject instance

        Debug.Log("Textfields count: " + view.textfields.Count);

        for (int i = 0; i < view.textfields.Count; i++)
        {
            newTextfieldMenuItemObj = (GameObject)Instantiate(textfieldMenuItemPF, transform);
            newTextfieldMenuItemObj.name = view.textfields[i].ToString().Replace("_", " ");

            string title = newTextfieldMenuItemObj.name;
            string placeholder = "Enter " + newTextfieldMenuItemObj.name + "...";

            string text = "Default";
            Realm realm = RealmManager.realmManager.realm;
            ViewController.TextfieldMenuItemTitle textFieldName = view.textfields[i];
            if (view.menuName == ViewController.ViewName.Info)
            {
                Debug.Log("populate grid called - info");   
                switch (textFieldName) {
                    case ViewController.TextfieldMenuItemTitle.Realm_Name:
                        text = realm.realmName;
                        break;
                    case ViewController.TextfieldMenuItemTitle.Account_Manager_Name:
                        //text = realm.accountManagerName;
                        text = "test";
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
                        Debug.Log("Error");
                        break;
                }
            }

            Debug.Log("1");
            GameObject viewManagerObj = transform.GetComponentInParent<ViewManager>().gameObject;
            Debug.Log("2");
            TextfieldMenuItem textfieldMenuItem = newTextfieldMenuItemObj.GetComponent<TextfieldMenuItem>();
            Debug.Log("3");
            textfieldMenuItem.viewManager = viewManagerObj;
            Debug.Log("4");
            textfieldMenuItem.PopulateTextfield(title, text, placeholder);
        }

        //for (int i = 0; i < menuObject.textfields.Count; i++)
        //{
        //    newTextfieldMenuItemObj = (GameObject)Instantiate(textfieldMenuItemPF, transform);
        //    newTextfieldMenuItemObj.name = menuObject.textfields[i].ToString().Replace("_"," ");

        //    GameObject viewManagerObj = transform.GetComponentInParent<ViewManager>().gameObject;
        //    newTextfieldMenuItemObj.GetComponent<TextfieldMenuItem>().viewManager = viewManagerObj;
        //}
        ResizeScrollView();
    }

    private void ResizeScrollView()
    {
        RectTransform parentRect = inputScrollView.GetComponent<RectTransform>();
        GridLayoutGroup gridLayout = gameObject.GetComponent<GridLayoutGroup>();
        gridLayout.cellSize = new Vector2(parentRect.rect.width - 20, 95);
    }
}
