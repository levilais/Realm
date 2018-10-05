using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSelectView : MonoBehaviour {

    public GameObject DisplayImagePF;

    private RObject activeObject;
    public DisplayImage imageToChange;

    private void OnEnable()
    {
        activeObject = RealmManager.realmManager.activeObject;
        PopulateHeader();
        PopulateImages();
    }

    private void PopulateHeader()
    {
        ViewController viewController = transform.GetComponent<ViewController>();
        viewController.headerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/PhotosIcon");
        viewController.headerTitle.GetComponent<Text>().text = "Select Image For " + activeObject.name;
    }

    private void PopulateImages()
    {
        // create "current" image
        GameObject currentImageObj = (GameObject)Instantiate(DisplayImagePF, transform);

        DisplayImage currentDisplayImage = currentImageObj.GetComponent<DisplayImage>();
        currentDisplayImage.photoName = RealmManager.realmManager.activeObject.imageName;
        currentDisplayImage.InitializePhoto();
        currentDisplayImage.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;

        imageToChange = currentDisplayImage;
        //Debug.Log("RealmDisplay images at PopulateImages: " + RealmManager.realmManager.realm.displayImages.Count);

        {
            for (int i = 0; i < 3; i++)
            {

                Debug.Log("Attempt " + i);
                string imageNameSuffix = "0" + (i + 1);
                string photoName = "DisplayImage" + imageNameSuffix;

                if (photoName != currentDisplayImage.photoName)
                {
                GameObject newDisplayImage = (GameObject)Instantiate(DisplayImagePF, transform);
                DisplayImage displayImage = newDisplayImage.GetComponent<DisplayImage>();
                displayImage.photoName = photoName;
                displayImage.InitializePhoto();
                newDisplayImage.transform.parent = transform.GetComponentInChildren<GridLayoutGroup>().transform;
                }
            }
        }

        currentDisplayImage.SelectPhoto();
    }

    public void SaveSelectedImage()
    {
        Debug.Log("save selected image called, still need to impliment");
    }

    public void ClearImages() {
        foreach (Transform image in transform.GetComponentInChildren<GridLayoutGroup>().transform) {
            Destroy(image.gameObject);
        }
    }

    private void OnDisable()
    {
        ClearImages();
    }

    public void PickImage()
    {
        int maxSize = 2048;
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);

            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);

                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                Rect rect = new Rect(0, 0, texture.width, texture.height);
                Vector2 vector2 = new Vector2(0.5f, 0.5f);
                Sprite sprite = Sprite.Create(texture, rect, vector2);
                imageToChange.ChangeSprite(sprite);

                //// Assign texture to a temporary quad and destroy it after 5 seconds
                //GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                //quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                //quad.transform.forward = Camera.main.transform.forward;
                //quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

                //Material material = quad.GetComponent<Renderer>().material;
                //if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                //    material.shader = Shader.Find("Legacy Shaders/Diffuse");

                //material.mainTexture = texture;

                //Destroy(quad, 5f);

                //// If a procedural texture is not destroyed manually, 
                //// it will only be freed after a scene change
                //Destroy(texture, 5f);
            }
        }, "Select a PNG image", "image/png", maxSize);

        Debug.Log("Permission result: " + permission);
    }
}
