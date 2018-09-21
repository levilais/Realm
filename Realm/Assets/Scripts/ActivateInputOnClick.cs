using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateInputOnClick : MonoBehaviour {

    public void ActivateInputField() {
        InputField inputField = transform.GetComponentInParent<InputField>();
        inputField.enabled = true;
        inputField.ActivateInputField();
    }

    public void DeactivateInputField() {
        InputField inputField = transform.GetComponentInParent<InputField>();
        inputField.enabled = false;
    }
}
