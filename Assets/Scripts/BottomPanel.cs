using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomPanel : MonoBehaviour
{
    [SerializeField]
    public Button[] buttons;
    private void Awake()
    {
        foreach (var obj in buttons)
        {
            obj.onClick.AddListener(() => { OnClick(obj); });
        }
    }
    private void OnClick(Button button)
    {
        foreach(var obj in buttons)
        {
            obj.interactable = true;
        }
        button.interactable = false;
    }
}
