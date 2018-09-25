using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeView : MonoBehaviour {

    public GameObject realmImage;

    private void OnEnable()
    {
        realmImage.SetActive(true);
    }

    private void OnDisable()
    {
        realmImage.SetActive(false);
    }
}
