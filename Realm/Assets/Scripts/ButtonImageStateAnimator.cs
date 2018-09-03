using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageStateAnimator : MonoBehaviour {

    public GameObject buttonImage;
    public Texture2D imageDefault;
    public Texture2D imagePressed;

    private void OnMouseDown()
    {
        //print("on mouse down called");
        //ShowImagePressed();
    }

    private void OnMouseUp()
    {
        print("on mouse up called");
        //ShowImageDefault();
    }

    public void ShowImagePressed() {
        print("ShowImagePressed called");
        Material materialTemp = buttonImage.GetComponent<CanvasRenderer>().GetMaterial();
        materialTemp.SetTexture("_MainTex", imagePressed);
        buttonImage.GetComponent<CanvasRenderer>().SetMaterial(materialTemp, 0);
    }

    //void ShowImageDefault()
    //{
    //    buttonImage.GetComponent<CanvasRenderer>().material.mainTexture = imagePressed;
    //}
}
