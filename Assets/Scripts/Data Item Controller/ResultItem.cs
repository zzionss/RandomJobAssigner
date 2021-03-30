using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class ResultItem : DataItem<Result>
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI jobText;
    
    public override void Initialize()
    {
        nameText.text = Data.name;
        jobText.text = Data.job;
    }
}
