using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProcessor : MonoBehaviour
{
    public Root allData;
    public static DataProcessor Instance;
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] private GameObject onBoarding;

    public void LoadData(Root data)
    {
        allData = data;
        SpawnManager.Instance.SpawnAllPlates();
        if (allData.onBoarding) onBoarding.SetActive(false);

    }
    public void AddNewPlate(Properties props)
    {
        props.ID = allData.properties.Count;

        allData.properties.Add(props);
        Parser.StartSave();
        SpawnManager.Instance.SpawnAllPlates();
    }
}
