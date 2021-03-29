using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class CleaningDataWindow : ScriptableWizard
{
    public Person[] persons;
    public Job[] jobs;

    [MenuItem("Window/Cleaning Data")]
    private static void Open()
    {
        CleaningDataWindow window = DisplayWizard<CleaningDataWindow>("Data Setting", "저장", "취소");
        window.maxSize = window.minSize = new Vector2(300, 400);
    }

    private void OnEnable()
    {
        Debug.Log("Cleaning Data Window Enable");
        jobs = CleaningDatas.Instance.jobs;
        persons = CleaningDatas.Instance.persons;
    }

    private void OnWizardCreate()
    {
        CleaningDatas.Instance.jobs = jobs;
        CleaningDatas.Instance.persons = persons;
        CleaningDatas.Instance.Save();
    }

    private void OnWizardOtherButton()
    {
        Close();
    }
}
