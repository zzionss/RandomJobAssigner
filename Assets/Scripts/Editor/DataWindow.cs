using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class DataWindow : ScriptableWizard
{
    public List<Person> personList;
    public List<Job> jobList;

    [MenuItem("Window/Datas")]
    private static void Open()
    {
        DataWindow window = DisplayWizard<DataWindow>("Data Setting", "저장", "취소");
        window.maxSize = window.minSize = new Vector2(300, 400);
    }

    private void OnEnable()
    {
        jobList = DataSetting.Instance.JobList;
        personList = DataSetting.Instance.PersonList;
    }

    private void OnWizardCreate()
    {
        DataSetting.Instance.JobList = jobList;
        DataSetting.Instance.PersonList = personList;
        DataSetting.Instance.Save();
    }

    private void OnWizardOtherButton()
    {
        Close();
    }
}
