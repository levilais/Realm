using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypos : MonoBehaviour {

    //public GameObject menuButtonParent;
    public List<WaypoName> waypoNames;
    public List<string> waypoNameStrings;

    public enum WaypoName
    {
        Back,
        New,
        Waypo,
        Anchor
    }

    public void OnEnable()
    {
        foreach (WaypoName waypoNameString in waypoNames) {
            waypoNameStrings.Add(waypoNameString.ToString());
        }

        List<string> subMenuItemNames = transform.GetComponent<ViewController>().subMenuItemNames;
        subMenuItemNames = waypoNameStrings;
    }
}
