using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObj : MonoBehaviour
{
    public GameObject obj;
    public Transform place;
    public void Create()
    {
        Instantiate(obj,place);
    }
}
