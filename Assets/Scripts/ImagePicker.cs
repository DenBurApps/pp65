using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagePicker : MonoBehaviour
{
	public Image _image;

    public void Init(string path)
    {
        if (path != "")
        {
            GetImageFromGallery.SetImage(path, _image);
        }
        else
            Debug.Log("Path is empty");
    }
}
