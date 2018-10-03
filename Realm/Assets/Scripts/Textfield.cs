using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textfield : MonoBehaviour
{

    public GameObject viewManager;

    public string textFieldTitle;
    public string textFieldText;
    public string textFieldPlaceholderText;

    TextfieldOverlay textfieldOverlay;
    InputField inputField;

    string textfieldObjectName;
    bool contentIsAdjustedForKeyboard = false;
    bool editModeActive = false;
    private string initialText;

    private void Start()
    {
        InitializeObjects();

    }

    private void OnEnable()
    {
        InitializeObjects();
    }

    private void InitializeObjects()
    {
        textfieldObjectName = transform.parent.name;
        inputField = transform.GetComponent<InputField>();
        textfieldOverlay = transform.GetComponentInParent<ViewManager>().TextfieldOverlay.gameObject.GetComponent<TextfieldOverlay>();

        if (textFieldText != "Default") {
            inputField.text = textFieldText;
        }

        viewManager = transform.GetComponentInParent<ViewManager>().gameObject;
    }



    void Update()
    {
        // EDITOR
#if UNITY_EDITOR
        if (inputField.isFocused && !contentIsAdjustedForKeyboard)
        {
            UpdateText();
            ShowOverlay();
        }

        if (!inputField.enabled && contentIsAdjustedForKeyboard)
        {
            HideOverlay();
        }

        // MOBILE
#else
        TouchScreenKeyboard keyboard = inputField.touchScreenKeyboard;
        if (inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Visible && !contentIsAdjustedForKeyboard) {
        Debug.Log("this shouldn't be called");
            UpdateText();    
            ShowOverlay();
        }
#endif
    }

    public void UpdateText() {
        if (editModeActive)
        {
            textfieldOverlay.currentText = inputField.text;
        }
    }

    public void ShowOverlay() {
        editModeActive = true;
        initialText = inputField.text;
        textfieldOverlay.GetComponent<TextfieldOverlay>().textfield = gameObject.GetComponent<Textfield>();
        textfieldOverlay.currentText = inputField.text;
        if (editModeActive)
        {
            UpdateText();
        }
        textfieldOverlay.gameObject.SetActive(true);
        contentIsAdjustedForKeyboard = true;
    }

    public void HideOverlay() {
        textfieldOverlay.gameObject.SetActive(false);
        contentIsAdjustedForKeyboard = false;
        editModeActive = false;
        SaveUpdatedText();
    }

    public void SaveUpdatedText() {
        ViewController view = transform.GetComponentInParent<ViewController>();

        string title = textFieldTitle;    
        string text = inputField.text;

        if (text != initialText)
        {
            Realm realm = RealmManager.realmManager.realm;
            if (view.menuName == ViewController.ViewName.Info)
            {
                switch (title)
                {
                    case "Realm Name":
                        realm.realmName = text;
                        break;
                    case "Account Manager Name":
                        realm.accountManagerName = text;
                        break;
                    case "Company Name":
                        realm.companyName = text;
                        break;
                    case "Contact Email":
                        realm.contactEmail = text;
                        break;
                    case "Contact Phone":
                        realm.contactPhone = text;
                        break;
                    default:
                        text = "Default";
                        Debug.Log("Error");
                        break;
                }
                DataManager.SaveData();
            }

            if (view.menuName == ViewController.ViewName.Display_Info)
            {
                bool newName = false;
                switch (title)
                {
                    case "Display Name":
                        RealmManager.realmManager.activeObject.name = text;
                        newName = true;
                        break;
                    case "Display ID":
                        RealmManager.realmManager.activeObject.displayID = text;
                        break;
                    default:
                        text = "Default";
                        break;
                }
                RealmManager.realmManager.activeObject.SaveActiveObject();
                if (newName)
                {
                    transform.GetComponentInParent<DisplayInfoView>().PopulateHeader();
                }
            }
        }
        else
        {
            Debug.Log("No new text found");
        }
    }
}
