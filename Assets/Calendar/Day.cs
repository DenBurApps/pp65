using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Day : MonoBehaviour
{
    private int dayNum;

    private bool disabled;

    public DateTime DateTime;
    private Button button;
    private Action<Day> onDayClicked;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void Init(int dayNum, DateTime dateTime, Action<Day> action)
    {
        this.dayNum = dayNum;
        DateTime = dateTime;

        onDayClicked = action;
    }

    public void DisableDay()
    {
        disabled = true;

        gameObject.GetComponent<Button>().interactable = false;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
        mainImage.color = new Color(0,0,0,0);
    }
    private bool today;
    public void EnableDay(bool today)
    {
        disabled = false;

        this.today = today;
        gameObject.GetComponent<Button>().interactable = true;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = dayNum.ToString();
        if (today)
        {
            mainImage.color = Color.black;
            todayDay.SetActive(true);
        }
        else
        {
            todayDay.SetActive(false);
            mainImage.color = Color.white;
        }
    }

    public void SetDayChoosedState()
    {
        if (!disabled)
        {
            if (today)
            {
                mainImage.color = Color.black;
                todayDay.SetActive(false);
            }
            else
            {
                mainImage.color = Color.black;
                todayDay.SetActive(false);
            }
            gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
    }
    public void SetDayUnchoosedState()
    {
        if (!disabled)
        {
            if (today)
            {
                mainImage.color = Color.black;
                todayDay.SetActive(true);
            }
            else
            {
                mainImage.color = Color.white;
                todayDay.SetActive(false);
            }
            gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;

        }
    }
    private void OnClick()
    {
        onDayClicked.Invoke(this);
        SetDayChoosedState();
    }

    public Image mainImage;
    [SerializeField] private GameObject todayDay;
}
