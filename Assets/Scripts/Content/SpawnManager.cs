using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    private List<BasePlate> AllPlates = new List<BasePlate>();

    [SerializeField] private BasePlate plate;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform spawnPreviewPoint;
    [SerializeField] private GameObject emptyObject;

    private List<string> currentStatuses;
    private void Awake()
    {
        if (calendar != null)
            calendar.Init(ChooseDay, 2);
    }
    private void OnEnable()
    {
        Instance = this;
        currentStatuses = SpawnFilter.currentFilters;
    }
    public void Start()
    {
        if(searchField != null)
            searchField.onValueChanged.AddListener(ChangeFilterText);
        if (DataProcessor.Instance != null)
        SpawnAllPlates();
    }

    public void ChangeFilterText(string str)
    {
        text = str;

        SpawnAllPlates();

    }
    public string text = "";
    public void ChangeStatus(List<string> status)
    {
        currentStatuses = status;
        SpawnAllPlates();
    }
    public void SpawnAllPlates()
    {
        ClearPlates();
        foreach (var item in DataProcessor.Instance.allData.properties)
        {
            if (Filter(item))
            {
                var obj = Instantiate(plate, spawnPoint);
                obj.Init(item, spawnPreviewPoint);
                AllPlates.Add(obj);
            }
        }
        if (AllPlates.Count == 0) emptyObject.SetActive(true); else emptyObject.SetActive(false);
    }
    private void ClearPlates()
    {
        foreach (var plate in AllPlates)
            Destroy(plate.gameObject);
        AllPlates.Clear();
    }
    [SerializeField] private TMP_InputField searchField;
    [SerializeField] private Calendar calendar;

    public List<DateTime> choosedDays = new List<DateTime>();
    private int changedDay = 0;
    private void ChooseDay(Day day)
    {
        if(choosedDays.Count < 2)
        {
            choosedDays.Add(day.DateTime);
        }
        else
        {
            choosedDays.RemoveAt(changedDay++);
            choosedDays.Add(day.DateTime);
            if (choosedDays[0] > choosedDays[1])
                (choosedDays[0], choosedDays[1]) = (choosedDays[1], choosedDays[0]);
        }
        if(changedDay == choosedDays.Count)
        {
            changedDay = 0;
        }
        for(int i =0;i< choosedDays.Count;i++)
        {
            calendar.choosedDays[i] = choosedDays[i];
        }
        calendar.SetDayStates();
        SpawnAllPlates();
    }
    private bool Filter(Properties props)
    {
        if (state == spawnManagerStates.Base)
        {
            if(text != "")
                if (!props.car.brand.Contains(text) &&
                    !props.description.Contains(text) &&
                    !props.car.yearManufacture.Contains(text))
                {
                    return false;
                }
        }
        if (state == spawnManagerStates.Calendar)
        {
            DateTime.TryParse(props.endDate, out DateTime date);
            if(choosedDays.Count == 2)
            {
                if ((date < choosedDays[0]) || (date > choosedDays[1]))
                    return false;
            }

        }
        if(currentStatuses.Count > 0)
        {
            if(!currentStatuses.Contains(props.status)) 
                return false;
        }
            return true;
    }
    public spawnManagerStates state = spawnManagerStates.Base;
    public enum spawnManagerStates
    {
        Base,
        Calendar
    }
}
