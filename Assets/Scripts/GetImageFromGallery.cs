using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetImageFromGallery : MonoBehaviour
{
    [SerializeField] private GameObject imageGetterGM;
    private ICanAddNewImage imageGetter;
    private void Awake()
    {
        imageGetter = imageGetterGM.GetComponent<ICanAddNewImage>();
    }
    public void PickImagePath()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("path is - " + path);

            if (path != null)
                imageGetter.GetImage(path);
        });
    }
}
