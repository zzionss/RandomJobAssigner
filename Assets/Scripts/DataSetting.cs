using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

public class DataSetting
{
#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void OnDomainReload()
    {
        instance = null;
    }
#endif

    private static DataSetting instance;
    
    [SerializeField]
    private List<Person> personList;
    [SerializeField]
    private List<Job> jobList;

    public readonly static string fileName = "data.json";
    public readonly static string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\RandomJobAssigner\Data\";
    public readonly static string fullPath = Path.Combine(filePath, fileName);

    public static DataSetting Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }

            if(File.Exists(fullPath))
            {
                instance = new DataSetting();
                JsonUtility.FromJsonOverwrite(File.ReadAllText(fullPath), instance);
                return instance;
            }

            if(!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            instance = new DataSetting();
            File.WriteAllText(fullPath, JsonUtility.ToJson(instance, true));
            return instance;
        }
    }
    public int JobCountSum => JobList.ConvertAll(o => o.count).Sum();
    public List<Person> ActivePersons => PersonList.FindAll(o => o.isActive);

    public List<Person> PersonList { get => personList; set => personList = value; }
    public List<Job> JobList { get => jobList; set => jobList = value; }

    public DataSetting()
    {
        personList = new List<Person>();
        jobList = new List<Job>();
    }

    public void Save()
    {
        File.WriteAllText(fullPath, JsonUtility.ToJson(instance, true));
    }
}
