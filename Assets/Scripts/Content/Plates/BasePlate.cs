using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasePlate : MonoBehaviour
{
    public Properties properties;

    public TextMeshProUGUI nameTMP;
    public TextMeshProUGUI endDateTMP;
    public TextMeshProUGUI descriptionTMP;

    public Preview previewPrefab;
    public GameObject photoSroll;
    public GameObject descriptionSroll;

    public void Init(Properties props,Transform spawnPointPreview)
    {
        properties = props;
        nameTMP.text = properties.car.brand + " - " + properties.car.yearManufacture;
        endDateTMP.text = properties.endDate;

        GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("spawn");
            var obj = Instantiate(previewPrefab,spawnPointPreview);
            obj.Init(properties);
        });
        descriptionTMP.text = properties.description;
        if (properties.photoes.Count == 0)
        {
            photoSroll.SetActive(false);
            descriptionSroll.SetActive(true);
        }
        else
            foreach (var item in properties.photoes)
            {
                SpawnImage(item);
            }
    }
    public ImagePicker imagePickerPrefab;
    public Transform imagePickerPlace;
    private void SpawnImage(string path)
    {
        var obj = Instantiate(imagePickerPrefab, imagePickerPlace);

        obj.Init(path);
    }
}
