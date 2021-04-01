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

        // TODO: 청소담당별로 또는 팀별로 색깔을 주면 어떨까 합니다.
    }
}
