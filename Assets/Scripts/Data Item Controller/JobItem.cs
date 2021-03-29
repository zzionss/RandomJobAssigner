using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class JobItem : DataItem<Job>
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TMP_InputField countField;
    [SerializeField]
    private Button increaseButton;
    [SerializeField]
    private Button decreaseButton;

    private void OnEnable()
    {
        increaseButton.onClick.AddListener(() => Increase());
        decreaseButton.onClick.AddListener(() => Decrease());
    }

    public override void Initialize()
    {
        nameText.text = data.name;
        countField.text = data.count.ToString();
    }

    private void Increase()
    {
        data.count++;
        countField.text = data.count.ToString();
        CleaningDatas.Instance.Save();
    }

    private void Decrease()
    {
        if(data.count > 0)
        {
            data.count--;
            countField.text = data.count.ToString();
            CleaningDatas.Instance.Save();
        }
    }
}
