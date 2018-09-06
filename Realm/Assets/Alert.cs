using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour {
    
    public GameObject AlertView;
    public GameObject AlertPopup;
    public GameObject MainPanel;
    public GameObject Button;
    public GameObject AlertTitle;
    public GameObject AlertMessage;

    private string alertTitle;
    private string alertMessage;
    private string alertButtonTitle;

    public void PresentAlertMessage(GameObject presenter, string alertTitle, string alertMessage, string alertButtonTitle) {
        AlertTitle.GetComponent<Text>().text = alertTitle;
        AlertMessage.GetComponent<Text>().text = alertMessage;
        Button.GetComponent<Text>().text = alertButtonTitle;
    }

    public void DismissAlert() {
        Destroy(gameObject);
    }
}
