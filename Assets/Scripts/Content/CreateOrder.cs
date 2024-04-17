using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateOrder : MonoBehaviour, ICanChangeStatus, ICanAddNewImage
{
    public Properties order = new Properties();

    public InputFieldChanger orderNameField;

    public InputFieldChanger nameField;
    public InputFieldChanger contactsField;

    public InputFieldChanger brandField;
    public InputFieldChanger modelField;
    public InputFieldChanger manufactureField;
    public InputFieldChanger VINField;
    public InputFieldChanger descriptionField;

    public List<InputFieldChanger> listOfWorks = new List<InputFieldChanger>();
    public List<PartPlate> partList = new List<PartPlate>();

    public InputFieldChanger commentField;

    public void ChangeStatus(string status)
    {
        order.status = status;
    }

    public void Save()
    {
        order.orderName = orderNameField.text;
        order.client.name = nameField.text;
        order.client.contacts = contactsField.text;

        order.car.brand = brandField.text;
        order.car.model =modelField.text;
        order.car.yearManufacture = manufactureField.text;
        order.car.code = VINField.text;
        order.description = descriptionField.text;

        foreach(var item in listOfWorks)
        {
            order.works.Add(item.text);
        }
        foreach(var item in partList)
        {
            order.parts.Add(new Part());
            order.parts[order.parts.Count - 1].name = item.nameField.text;

            float.TryParse(item.costField.text,out float cost);
            order.parts[order.parts.Count - 1].cost = cost;

        }

        order.comment = commentField.text;
        DataProcessor.Instance.AddNewPlate(order);
        Destroy(gameObject);
    }
    public InputFieldChanger workPrefab;
    public Transform workPlace;

    public PartPlate partPrefab;
    public Transform partPlace;

    public void CreateWork()
    {
        var obj = Instantiate(workPrefab,workPlace);
        listOfWorks.Add(obj);
        obj.nameTMP.text = "Work " + listOfWorks.Count;
        obj.GetComponent<DeleteWork>().DeleteButton.onClick.AddListener(() =>
        {
            listOfWorks.Remove(obj);

            for(int i = 0;i < listOfWorks.Count; i++)
            {
                listOfWorks[i].nameTMP.text = "Work " + (i + 1);
            }
            Destroy(obj.gameObject);
        });
    }
    public void CreatePart()
    {
        var obj = Instantiate(partPrefab, partPlace);
        partList.Add(obj);
        obj.partNameField.text = "Spare part " + partList.Count;
        obj.DeleteButton.onClick.AddListener(() =>
        {
            partList.Remove(obj);
            for (int i = 0; i < partList.Count; i++)
            {
                partList[i].partNameField.text = "Spare part " + (i + 1);
            }
            Destroy(obj.gameObject);
        });
    }
    private void Awake()
    {
        startDateCalendar.Init(ChangeStartDate, 1);
        endDateCalendar.Init(ChangeEndDate, 1);

    }
    [SerializeField] private Calendar startDateCalendar;
    public TextMeshProUGUI startDateText;

    private void ChangeStartDate(Day day)
    {
        order.date = day.DateTime.ToString().Remove(10);
        startDateText.text = day.DateTime.ToString().Remove(10);
        startDateCalendar.SetDayStates();
    }
    [SerializeField] private Calendar endDateCalendar;
    public TextMeshProUGUI endDateText;

    private void ChangeEndDate(Day day)
    {
        order.endDate = day.DateTime.ToString().Remove(10);
        endDateText.text = day.DateTime.ToString().Remove(10);
        endDateCalendar.SetDayStates();
    }

    public ImagePicker imagePickerPrefab;
    public Transform imagePickerPlace;
    public void PickImage()
    {
        GetImageFromGallery.PickImage(GetImage);
    }
    public void GetImage(string path)
    {
        order.photoes.Add(path);
        var obj = Instantiate(imagePickerPrefab, imagePickerPlace);

        obj.Init(path);
    }
}
