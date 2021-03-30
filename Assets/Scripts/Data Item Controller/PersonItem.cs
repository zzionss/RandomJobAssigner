using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class PersonItem : DataItem<Person>
{
    [SerializeField]
    private TMP_InputField nameText;
    [SerializeField]
    private Toggle activeToggle;

    public override void Initialize()
    {
        nameText.text = Data.name;
        activeToggle.isOn = Data.isActive;
    }

    public void ChangeActiveState(bool isActive)
    {
        Data.isActive = isActive;
        CleaningDatas.Instance.Save();
    }

    public void ChangeName(string name)
    {
        Data.name = name;
        CleaningDatas.Instance.Save();
    }
}
