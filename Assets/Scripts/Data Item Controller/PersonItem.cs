using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class PersonItem : DataItem<Person>
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private Toggle activeToggle;

    public override void Initialize()
    {
        nameText.text = data.name;
        activeToggle.isOn = data.isActive;
    }

    public void ChangeActiveState(bool isActive)
    {
        data.isActive = isActive;
        CleaningDatas.Instance.Save();
    }


}
