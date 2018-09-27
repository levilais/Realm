using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateDisplayView : MonoBehaviour {

    public List<GameObject> subViews;
    public int activeSubView = 0;
    public GameObject confirmImage;
    private int stepInProcess = 0;

    GameObject currentSubView;
    ViewController currentSubViewController;
    GameObject messageTextObj;
    Text messageText;

    private void OnEnable()
    {
        subViews[0].SetActive(true);
        currentSubView = subViews[activeSubView];
        currentSubViewController = currentSubView.GetComponentInChildren<ViewController>();
        messageTextObj = currentSubViewController.messageText;
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
                confirmImage.SetActive(false);
                GoToNextSubView();
                stepInProcess += 1;
                break;
            case 5:
                GoToNextSubView();
                stepInProcess += 1;
                break;
            case 6:
                string toMenu = "Back";
                ViewManager.viewManager.performSegue(toMenu, gameObject);
                exitCreateDisplayView();
                break;
            default:
                Debug.Log("Error finding stepInProcess");
                break;
        }
    }

    public void GoToNextSubView() {
        GameObject oldSubView = currentSubView;
        activeSubView += 1;
        currentSubView = subViews[activeSubView];
        currentSubView.SetActive(true);
        oldSubView.SetActive(false);
        currentSubViewController = currentSubView.GetComponentInChildren<ViewController>();
        messageTextObj = currentSubViewController.messageText;
        messageText = messageTextObj.GetComponent<Text>();
    }

    public void GoToPreviousSubView() {
        switch (activeSubView) {
            case 0:
                exitCreateDisplayView();
                break;
            case 1:
                stepInProcess = 0;
                break;
            default:
                break;
        }

        GameObject oldSubView = currentSubView;
        activeSubView -= 1;
        currentSubView = subViews[activeSubView];
        currentSubView.SetActive(true);
        oldSubView.SetActive(false);
        currentSubViewController = currentSubView.GetComponentInChildren<ViewController>();
        messageTextObj = currentSubViewController.messageText;
        messageText = messageTextObj.GetComponent<Text>();
        ProceedSequentially();
    }

    public void exitCreateDisplayView() {
        foreach (GameObject subView in subViews) {
            subView.SetActive(false);
        }

        confirmImage.SetActive(false);
        activeSubView = 0;
        stepInProcess = 0;
        gameObject.SetActive(false);
    }
}
