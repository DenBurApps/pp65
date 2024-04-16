using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStatusButton : MonoBehaviour
{
    public GameObject Choosed;
    public static ChangeStatusButton ChoosedStatus;
    public string Status;
    public ICanChangeStatus creator;
    public GameObject creatorGM;

    public TextMeshProUGUI text;
    private void Awake()
    {
        creator = creatorGM.GetComponent<ICanChangeStatus>();
        text.text = Status;
        GetComponent<Button>().onClick.AddListener(ChangeStatus);
    }

    public void ChangeStatus()
    {
        creator.ChangeStatus(Status);
        if(ChoosedStatus != null)
        {
            ChoosedStatus.Choosed.SetActive(false);
        }
        ChoosedStatus = this;
        Choosed.SetActive(true);
    }
}
