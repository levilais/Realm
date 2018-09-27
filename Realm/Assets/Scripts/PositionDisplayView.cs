using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionDisplayView : MonoBehaviour {

    public GameObject confirmImage;
    private int stepInProcess = 0;

    GameObject messageTextObj;
    Text messageText;

    private void OnEnable()
    {
        messageTextObj = transform.GetComponent<ViewController>().messageText;
        messageText = messageTextObj.GetComponent<Text>();
        ProceedSequentially();
    }

    public void ProceedSequentially()
    {
        switch (stepInProcess)
        {
            case 0:
                messageText.text = "Step 1: Scan the nearest Waypost image...";
                stepInProcess += 1;
                break;
            case 1:
                // This will appear for 2 seconds (maybe 3) when Anchor is found
                confirmImage.SetActive(true);
                messageText.text = "Connected. You are now Anchored in.";
                stepInProcess += 1;
                break;
            case 2:
                confirmImage.SetActive(false);
                messageText.text = "Step 2: Locate a plane and tap to select Display position.";
                stepInProcess += 1;
                break;
            case 3:
                confirmImage.SetActive(true);
                messageText.text = "Your Display has been positioned.";
                stepInProcess += 1;
                break;
            case 4:
                string toMenu = "Back";
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
        confirmImage.SetActive(false);
    }
}
