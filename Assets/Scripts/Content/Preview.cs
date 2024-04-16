using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Preview : MonoBehaviour
{
    public Properties properties;

    [SerializeField] private TextMeshProUGUI status;
    [SerializeField] private TextMeshProUGUI startDate;
    [SerializeField] private TextMeshProUGUI endDate;
    [SerializeField] private TextMeshProUGUI client;
    [SerializeField] private TextMeshProUGUI description;

    public WorkInPreview workPrefab;
    public Transform workPlace;

    public PartPlate partPrefab;
    public Transform partPlace;

    [SerializeField] private TextMeshProUGUI comment;

    public EditOrder editOrderPrefab;
    public void Init(Properties props)
    {
        ClearAll();
        properties = props;

        status.text = properties.status;
        startDate.text = properties.date;
        endDate.text = properties.endDate;
        client.text = properties.client.name;
        description.text = properties.description;
        comment.text = properties.comment;

        int i = 0;
        foreach(var item in properties.works)
        {
            var obj = Instantiate(workPrefab, workPlace);
            obj.text.text = item;
            obj.number.text = "Work " + i++;
            allSpawnedObjects.Add(obj.gameObject);
        }
        i = 0;
        foreach (var item in properties.parts)
        {
            var obj = Instantiate(partPrefab, partPlace);
            obj.partNameField.text = "Spare part " + i++;
            obj.nameField.ChangeText(item.name);
            obj.costField.ChangeText(item.cost.ToString());
            allSpawnedObjects.Add(obj.gameObject);
        }
        foreach(var item in properties.photoes)
        {
            SpawnImage(item);
        }
    }
    public ImagePicker imagePickerPrefab;
    public Transform imagePickerPlace;
    public void SpawnImage(string path)
    {
        var obj = Instantiate(imagePickerPrefab, imagePickerPlace);
        obj.Init(path);
        allSpawnedObjects.Add(obj.gameObject);

    }
    private List<GameObject> allSpawnedObjects = new List<GameObject>();
    private void ClearAll()
    {
        foreach (var obj in allSpawnedObjects)
            Destroy(obj);
        allSpawnedObjects.Clear();
    }
    public void Delete()
    {
        DataProcessor.Instance.allData.properties.Remove(properties);
        SpawnManager.Instance.SpawnAllPlates();
        Parser.StartSave();
    }
    public void SpawnEditOrder()
    {
        Instantiate(editOrderPrefab,transform).Init(properties,this);
    }
}
