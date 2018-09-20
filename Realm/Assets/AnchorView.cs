using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnchorView : MonoBehaviour {

    public GameObject leftButtonTitle;

    private GameObject messageTextObj;
    private Text messageText;

    private void OnEnable()
    {
        InitializeMessageText();
    }
    // Use this for initialization
    void Start ()
    {
        
    }

    private void InitializeMessageText()
    {
        messageTextObj = transform.GetComponent<ViewController>().messageText;
        messageText = messageTextObj.GetComponent<Text>();
        messageText.text = "Step 1: Scan your \"Anchor\" image...";
        messageTextObj.SetActive(true);
        //leftButtonTitle.SetActive(false);
    }

    private void OnDisable()
    {
        leftButtonTitle.SetActive(true);
        messageTextObj.SetActive(false);
    }
}
