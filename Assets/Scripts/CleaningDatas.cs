using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

public class CleaningDatas
{
    private const string settingFileName = "DataSetting.json";
    
    private static CleaningDatas instance;

    [SerializeField]
    private List<Person> personList;
    [SerializeField]
    private List<Job> jobList;

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
    public int JobCountSum => JobList.ConvertAll(o => o.count).Sum();
    public List<Person> ActivePersons => PersonList.FindAll(o => o.isActive);

    public List<Person> PersonList { get => personList; set => personList = value; }
    public List<Job> JobList { get => jobList; set => jobList = value; }

    public CleaningDatas()
    {
        personList = new List<Person>();
        jobList = new List<Job>();
    }

    public void Save()
    {
        File.WriteAllText(SettingFilePath, JsonUtility.ToJson(instance, true));
    }
}
