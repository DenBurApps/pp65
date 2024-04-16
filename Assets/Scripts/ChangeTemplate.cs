using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ChangeTemplate : MonoBehaviour
{
    [SerializeField] private Image template;
    [SerializeField] private Image copy;

    void Update()
    {
        copy.sprite = template.sprite;
    }
}
