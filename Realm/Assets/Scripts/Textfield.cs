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
            Debug.Log("1");
            UpdateText();
            Debug.Log("2");
            ShowOverlay();
            Debug.Log("3");
        }
        if (!inputField.enabled && contentIsAdjustedForKeyboard)
        {
            HideOverlay();
        }

        // MOBILE
#else
        TouchScreenKeyboard keyboard = inputField.touchScreenKeyboard;
        if (inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Visible && !contentIsAdjustedForKeyboard) {
            UpdateText();    
            ShowOverlay();
        }
#endif
    }

    public void UpdateText() {
        Debug.Log("setting currentText from inputField.text: " + inputField.text);
        textfieldOverlay.currentText = inputField.text;
    }

    public void ShowOverlay() {
        initialText = inputField.text;
        //textfieldOverlay.GetComponent<TextfieldOverlay>().textfield = gameObject.GetComponent<Textfield>();
        //textfieldOverlay.currentText = inputField.text;
        textfieldOverlay.gameObject.SetActive(true);
        contentIsAdjustedForKeyboard = true;
    }

    public void HideOverlay() {
        textfieldOverlay.gameObject.SetActive(false);
        contentIsAdjustedForKeyboard = false;
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
                switch (title)
                {
                    case "Display Name":
                        RealmManager.realmManager.activeObject.name = text;
                        break;
                    case "Display ID":
                        RealmManager.realmManager.activeObject.displayID = text;
                        break;
                    default:
                        text = "Default";
                        break;
                }
                RealmManager.realmManager.activeObject.SaveActiveObject();
            }
        }
        else
        {
            Debug.Log("No new text found");
        }
    }
}
