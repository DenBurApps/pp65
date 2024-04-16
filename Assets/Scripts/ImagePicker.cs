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
            Texture2D texture = NativeGallery.LoadImageAtPath(path);
            if (texture != null)
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
                _image.sprite = sprite;

            }
            else
                Debug.Log("texture = null");
        }
        else
            Debug.Log("Path is empty");
    }



}
