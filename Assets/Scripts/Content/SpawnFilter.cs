using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnFilter : MonoBehaviour
{
    public static List<string> currentFilters = new List<string>();

    public GameObject Choosed;
    public string Status;
    public TextMeshProUGUI text;

    private void Awake()
    {
        text.text = Status;
        GetComponent<Button>().onClick.AddListener(ChangeStatus);
    }

    private void ChangeStatus()
    {
        if (currentFilters.Contains(Status))
        {
            Choosed.SetActive(false);
            currentFilters.Remove(Status);
        }
        else
        {
            Choosed.SetActive(true);
            currentFilters.Add(Status);
        }

        SpawnManager.Instance.ChangeStatus(currentFilters);
    }
}
