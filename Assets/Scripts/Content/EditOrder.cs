using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class EditOrder : MonoBehaviour, ICanChangeStatus, ICanAddNewImage
{
    public Properties order = new Properties();

    public ChangeStatusButton[] statusButtons;
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

    public Preview preview;

    public RectTransform[] SetLastSibling;
    public void Init(Properties props,Preview preview)
    {
        this.preview = preview;
        order = props;

        foreach(var obj in statusButtons)
        {
            if (obj.Status == order.status)
                obj.ChangeStatus();
        }
        orderNameField.ChangeText(order.orderName);
        nameField.ChangeText(order.client.name);
        contactsField.ChangeText(order.client.contacts);

        brandField.ChangeText(order.car.brand);
        modelField.ChangeText(order.car.model);
        manufactureField.ChangeText(order.car.yearManufacture);
        VINField.ChangeText(order.car.code);
        descriptionField.ChangeText(order.description);

        int i = 1;
        foreach(var item in order.works)
        {
            var obj = Instantiate(workPrefab, workPlace);

            obj.nameTMP.text = "Work " + i++;

            obj.ChangeText(item);
            listOfWorks.Add(obj);

            obj.GetComponent<DeleteWork>().DeleteButton.onClick.AddListener(() =>
            {
                listOfWorks.Remove(obj);
                for (int j = 0; j < listOfWorks.Count; j++)
                {
                    listOfWorks[j].nameTMP.text = "Work " + (j + 1);
                }
                Destroy(obj.gameObject);
            });
        }
        i = 1;
        foreach(var item in order.parts)
        {
            var obj = Instantiate(partPrefab, partPlace);
            obj.partNameField.text = "Spare part " + i++;
            obj.costField.ChangeText(item.cost.ToString());
            obj.nameField.ChangeText(item.name);
            partList.Add(obj);

            obj.DeleteButton.onClick.AddListener(() =>
            {
                partList.Remove(obj);
                for (int j = 0; j < partList.Count; j++)
                {
                    partList[j].partNameField.text = "Spare part " + (j + 1);
                }
                Destroy(obj.gameObject);
            });
        }
        commentField.ChangeText(order.comment);

        foreach(var item in order.photoes)
        {
            var obj = Instantiate(imagePickerPrefab, imagePickerPlace);

            obj.Init(item);
        }

        startDateText.text = order.date;
        endDateText.text = order.endDate;

        foreach(var obj in SetLastSibling)
            obj.SetAsLastSibling();
    }
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
        order.car.model = modelField.text;
        order.car.yearManufacture = manufactureField.text;
        order.car.code = VINField.text;
        order.description = descriptionField.text;

        order.works.Clear();
        foreach (var item in listOfWorks)
        {
            order.works.Add(item.text);
        }
        order.parts.Clear();
        foreach (var item in partList)
        {
            order.parts.Add(new Part());
            order.parts[order.parts.Count - 1].name = item.nameField.text;

            float.TryParse(item.costField.text, out float cost);
            order.parts[order.parts.Count - 1].cost = cost;

        }

        order.comment = commentField.text;

        DataProcessor.Instance.allData.properties[order.ID] = order;
        Parser.StartSave();
        preview.Init(order);
        SpawnManager.Instance.SpawnAllPlates();

        Destroy(gameObject);
    }
    public InputFieldChanger workPrefab;
    public Transform workPlace;

    public PartPlate partPrefab;
    public Transform partPlace;

    public void CreateWork()
    {
        var obj = Instantiate(workPrefab, workPlace);
        listOfWorks.Add(obj);
        obj.nameTMP.text = "Work " + listOfWorks.Count;

        obj.GetComponent<DeleteWork>().DeleteButton.onClick.AddListener(() =>
        {
            listOfWorks.Remove(obj);
            for (int i = 0; i < listOfWorks.Count; i++)
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
    }
    [SerializeField] private Calendar endDateCalendar;
    public TextMeshProUGUI endDateText;

    private void ChangeEndDate(Day day)
    {
        order.endDate = day.DateTime.ToString().Remove(10);
        endDateText.text = day.DateTime.ToString().Remove(10);

    }

    public ImagePicker imagePickerPrefab;
    public Transform imagePickerPlace;

    public void GetImage(string path)
    {
        order.photoes.Add(path);
        var obj = Instantiate(imagePickerPrefab, imagePickerPlace);

        obj.Init(path);
    }
}
