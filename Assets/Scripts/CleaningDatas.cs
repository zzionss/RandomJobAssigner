using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

public class CleaningDatas
{
    private const string settingFileName = "DataSetting.json";
    
    private static CleaningDatas instance;

    public Person[] persons;
    public Job[] jobs;

    public static string SettingFilePath => Path.Combine(Application.dataPath, settingFileName);
    public static CleaningDatas Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }

            if(File.Exists(SettingFilePath))
            {
                instance = new CleaningDatas();
                JsonUtility.FromJsonOverwrite(File.ReadAllText(SettingFilePath), instance);
                return instance;
            }

            instance = new CleaningDatas();
            File.WriteAllText(SettingFilePath, JsonUtility.ToJson(instance, true));
            return instance;
        }
    }
    public int JobCountSum => Array.ConvertAll(jobs, o => o.count).Sum();
    public Person[] ActivePersons => Array.FindAll(persons, o => o.isActive);

    public void Save()
    {
        File.WriteAllText(SettingFilePath, JsonUtility.ToJson(instance, true));
    }

   
}
