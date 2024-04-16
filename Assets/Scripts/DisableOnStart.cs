using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    public GameObject[] disable;
    void Start()
    {
        foreach (GameObject obj in disable)
        {
            obj.SetActive(false);
        }
    }
}
