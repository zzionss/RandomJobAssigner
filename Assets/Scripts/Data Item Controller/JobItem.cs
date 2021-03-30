using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class JobItem : DataItem<Job>
{
    [SerializeField]
    private TMP_InputField nameText;
    [SerializeField]
    private TMP_InputField countField;
    [SerializeField]
    private Button increaseButton;
    [SerializeField]
    private Button decreaseButton;

    public override void Initialize()
    {
        nameText.text = Data.name;
        countField.text = Data.count.ToString();
    }

    public void Increase()
    {
        Data.count++;
        countField.text = Data.count.ToString();
        CleaningDatas.Instance.Save();
    }

    public void Decrease()
    {
        if(Data.count > 0)
        {
            Data.count--;
            countField.text = Data.count.ToString();
            CleaningDatas.Instance.Save();
        }
    }

    public void ChangeCount(string count)
    {
        Data.count = int.Parse(count);
        CleaningDatas.Instance.Save();
    }

    public void ChangeName(string name)
    {
        Data.name = name;
        CleaningDatas.Instance.Save();
    }
}
