﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementView : MonoBehaviour {
    
    private GameObject messageTextObj;
    private Text messageText;
    private int stepInProcess = 0;

    private void OnEnable()
    {
        InitializeMessageText();
    }

    private void InitializeMessageText()
    {
        messageTextObj = transform.GetComponent<ViewController>().messageText;
        messageText = messageTextObj.GetComponent<Text>();
        ProceedSequentially();
        messageTextObj.SetActive(true);
    }

    public void ProceedSequentially()
    {
        switch (stepInProcess) {
            case 0:
                messageText.text = "Step 1: Scan your \"Anchor\" image...";
                stepInProcess += 1;
                break;
            case 1:
                // This will appear for 2 seconds (maybe 3) when Anchor is found
                messageText.text = "You are now connected to your Anchor.";
                stepInProcess += 1;
                break;
            case 2:
                messageText.text = "Step 2: Scan “Waypost [Insert Name]” image";
                stepInProcess += 1;
                break;
            case 3:
                // THIS IS WHERE WE CHANGE THE IMAGE AND "HAS BEEN SET" PROPERTY
                RObject activeObject = RealmManager.realmManager.activeObject;
                activeObject.PlaceObject();
                messageText.text = "Waypost [Insert Name] has been set.";
                stepInProcess += 1;
                break;
            case 4:
                RealmManager.realmManager.anchorExists = true;
                string toMenu;
                if (ViewManager.viewManager.navigationHistory.Count == 0) {
                    toMenu = "BEGIN";
                } else {
                    toMenu = "Back";
                }
                ViewManager.viewManager.performSegue(toMenu, gameObject);
                break;
            default:
                Debug.Log("Error finding stepInProcess");
                break;
        }
    }

    private void OnDisable()
    {
        stepInProcess = 0;
        messageTextObj.SetActive(false);
    }
}
