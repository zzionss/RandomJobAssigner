using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class CleaningDataWindow : ScriptableWizard
{
    public List<Person> personList;
    public List<Job> jobList;

    [MenuItem("Window/Cleaning Data")]
    private static void Open()
    {
        CleaningDataWindow window = DisplayWizard<CleaningDataWindow>("Data Setting", "저장", "취소");
        window.maxSize = window.minSize = new Vector2(300, 400);
    }

    private void OnEnable()
    {
        Debug.Log("Cleaning Data Window Enable");
        jobList = CleaningDatas.Instance.JobList;
        personList = CleaningDatas.Instance.PersonList;
    }

    private void OnWizardCreate()
    {
        CleaningDatas.Instance.JobList = jobList;
        CleaningDatas.Instance.PersonList = personList;
        CleaningDatas.Instance.Save();
    }

    private void OnWizardOtherButton()
    {
        Close();
    }
}
